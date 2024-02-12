using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository, CountryRepository countryRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly CountryRepository _countryRepository = countryRepository;

    public AddressDto CreateAdress(AddressDto address)
    {
        try
        {
            if (!_addressRepository.Exists(x => x.StreetName == address.StreetName && x.City == address.City))
            {
                var countryEntity = _countryRepository.GetOne(x => x.Country == address.Country);
                countryEntity ??= _countryRepository.Create(new CountryEntity { Country = address.Country, Continent = address.Continent });

                var addressEntity = new AddressEntity
                {
                    City = address.City,
                    PostalCode = address.PostalCode,
                    StreetName = address.StreetName,
                    CountryId = countryEntity.Id,
                };

                var result = _addressRepository.Create(addressEntity);
                if (result != null)
                    return address;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
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

    public AddressDto GetOneAddress(string streetName, string postalCode)
    {
        try
        {
            var addressEntity = _addressRepository.GetOne(x => x.StreetName == streetName && x.PostalCode == postalCode);
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

    public bool RemoveAdress(string addressId)
    {
        try
        {
            return _addressRepository.Delete(x => x.Id == addressId);
        }
        catch(Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
}
