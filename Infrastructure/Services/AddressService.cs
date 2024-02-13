using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository, CountryRepository countryRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly CountryRepository _countryRepository = countryRepository;

    public bool CreateAdress(AddressDto address)
    {
        try
        {
            if (!_addressRepository.Exists(x => x.StreetName == address.StreetName && x.City == address.City))
            {
                var countryEntity = _countryRepository.GetOne(x => x.Country == address.Country);
                countryEntity ??= _countryRepository.Create(new CountryEntity { Id = Guid.NewGuid().ToString(),Country = address.Country, Continent = address.Continent });

                var addressEntity = new AddressEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    City = address.City,
                    PostalCode = address.PostalCode,
                    StreetName = address.StreetName,
                    CountryId = countryEntity.Id,
                };

                var result = _addressRepository.Create(addressEntity);
                if (result != null)
                    return true;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return false;
    }

    public IEnumerable<AddressDto> GetAllAddresses()
    {
        var addresses = new List<AddressDto>();
        try
        {
            var result = _addressRepository.GetAll();
            foreach (var address in result)
                addresses.Add(new AddressDto
                {
                    StreetName = address.StreetName,
                    City = address.City,
                    PostalCode = address.PostalCode,
                    Country = address.Country.Country,
                    Continent = address.Country.Continent
                });
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        
        return addresses;
    }

    public (AddressDto, AddressEntity) GetOneAddress(string streetName, string postalCode)
    {
        try
        {
            var addressEntity = _addressRepository.GetOne(x => x.StreetName == streetName && x.PostalCode == postalCode);
            if (addressEntity != null)
            {
                AddressDto addressDto = new AddressDto
                {
                    StreetName = addressEntity.StreetName,
                    City = addressEntity.City,
                    PostalCode = addressEntity.PostalCode,
                    Country = addressEntity.Country.Country,
                    Continent = addressEntity.Country.Continent
                };
                
                return (addressDto, addressEntity);
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return (null!, null!);
    }
    public AddressEntity GetOneAddressByID(string addressId)
    {
        try
        {
            var addressEntity = _addressRepository.GetOne(x => x.Id == addressId);
            if (addressEntity != null)
            {
                return addressEntity;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }
    public AddressDto UpdateAddress(AddressEntity entity)
    {
        try
        {
            var addressEntity = _addressRepository.Update(entity);
            if (addressEntity != null)
            {
                var addressDto = new AddressDto
                {
                    StreetName = addressEntity.StreetName,
                    City = addressEntity.City,
                    PostalCode = addressEntity.PostalCode,
                    Country = addressEntity.Country.Country,
                    Continent = addressEntity.Country.Continent
                };
                return addressDto;
            }
        }
        catch(Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }


    public CountryEntity GetCountryByID(string countryId)
    {
        try
        {
            var countryEntity = _countryRepository.GetOne(x => x.Id == countryId);
            if (countryEntity != null)
            {
                return countryEntity;
            }
        }
        catch { }
        return null!;
    }

    public bool RemoveAdress(string addressId)
    {
        try
        {
            AddressEntity addressEntity = GetOneAddressByID(addressId); 
            CountryEntity countryEntity = GetCountryByID(addressEntity.Country.Id);
            
            var countryId = addressEntity.Country.Id;
           
            var result = _addressRepository.Delete(x => x.Id == addressId);

            if (!countryEntity.Address.Any())
                _countryRepository.Delete(x => x.Id == countryId);

            return result;
        }
        catch(Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
}
