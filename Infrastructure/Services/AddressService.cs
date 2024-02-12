using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class AddressService(AddressRepository addressRepository, CountryRepository countryRepository)
{
    private readonly AddressRepository _addressRepository = addressRepository;
    private readonly CountryRepository _countryRepository = countryRepository;

    public bool CreateAdress(string city, string postalCode, string streetName, string country, string continent)
    {
        try
        {
            if (!_addressRepository.Exists(x => x.StreetName == streetName && x.City == city))
            {
                var countryEntity = _countryRepository.GetOne(x => x.Country == country);
                countryEntity ??= _countryRepository.Create(new CountryEntity { Country = country, Continent = continent });

                var addressEntity = new AddressEntity
                {
                    City = city,
                    PostalCode = postalCode,
                    StreetName = streetName,
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
}
