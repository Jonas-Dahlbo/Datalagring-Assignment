using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;
using System.Net;

namespace Infrastructure.Services
{
    public class ContactService(ContactRepository contactRepository, AddressRepository addressRepository, CountryRepository countryRepository, OccupationRepository occupationRepository, SalaryRepository salaryRepository)
    {
        private readonly ContactRepository _contactRepository = contactRepository;
        private readonly AddressRepository _addressRepository = addressRepository;
        private readonly CountryRepository _countryRepository = countryRepository;
        private readonly OccupationRepository _occupationRepository = occupationRepository;
        private readonly SalaryRepository _salaryRepository = salaryRepository;

        public bool CreateContact(ContactDto contact)
        {
            try
            {
                if (!_contactRepository.Exists(x => x.Email == contact.Email))
                {
                    var countryEntity = _countryRepository.GetOne(x => x.Country == contact.Country);
                    countryEntity ??= _countryRepository.Create(new CountryEntity { Country = contact.Country, Continent = contact.Continent });

                    var addressEntity = new AddressEntity
                    {
                        City = contact.City,
                        PostalCode = contact.PostalCode,
                        StreetName = contact.StreetName,
                        CountryId = countryEntity.Id,
                    };

                    var salaryEntity = _salaryRepository.GetOne(x => x.Salary == contact.Salary);
                    salaryEntity ??= _salaryRepository.Create(new SalaryEntity { Salary = contact.Salary });

                    var occupationEntity = new OccupationEntity
                    {
                        Occupation = contact.Occupation,
                        Description = contact.Description,
                        SalaryId = salaryEntity.Id
                    };

                    var contactEntity = new ContactEntity
                    {
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        AddressId = addressEntity.Id,
                        OccupationId = occupationEntity.Id
                    };

                    var result = _contactRepository.Create(contactEntity);
                    if (result != null)
                        return true;
                }
            }
            catch (Exception ex) { Debug.WriteLine(ex.Message); }
            return false;
        }
    }
}
