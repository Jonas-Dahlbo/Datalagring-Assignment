using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class ContactService(ContactRepository contactRepository, CountryRepository countryRepository, SalaryRepository salaryRepository)
    {
        private readonly ContactRepository _contactRepository = contactRepository;
        private readonly CountryRepository _countryRepository = countryRepository;
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

        public IEnumerable<ContactDto> GetAllContacts()
        {
            var contacts = new List<ContactDto>();
            try
            {
                var result = _contactRepository.GetAll();
                foreach (var contact in result)
                    contacts.Add(new ContactDto
                    {
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        StreetName = contact.Address.StreetName,
                        City = contact.Address.City,
                        PostalCode = contact.Address.PostalCode,
                        Country = contact.Address.Country.Country,
                        Continent = contact.Address.Country.Continent,
                        Occupation = contact.Occupation.Occupation,
                        Description = contact.Occupation.Description,
                        Salary = contact.Occupation.Salary.Salary
                    });
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

            return contacts;
        }
        public ContactDto GetOneContact(string email)
        {
            try
            {
                var contactEntity = _contactRepository.GetOne(x => x.Email == email);
                if (contactEntity != null)
                {
                    var contactDto = new ContactDto
                    {
                        FirstName = contactEntity.FirstName,
                        LastName = contactEntity.LastName,
                        Email = contactEntity.Email,
                        StreetName = contactEntity.Address.StreetName,
                        City = contactEntity.Address.City,
                        PostalCode = contactEntity.Address.PostalCode,
                        Country = contactEntity.Address.Country.Country,
                        Continent = contactEntity.Address.Country.Continent,
                        Occupation = contactEntity.Occupation.Occupation,
                        Description = contactEntity.Occupation.Description,
                        Salary = contactEntity.Occupation.Salary.Salary
                    };

                    return contactDto;
                }

            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

            return null!;
        }
        public ContactDto UpdateAddress(ContactEntity entity)
        {
            try
            {
                var contactEntity = _contactRepository.Update(entity);
                if (contactEntity != null)
                {
                    var contactDto = new ContactDto
                    {
                        FirstName = contactEntity.FirstName,
                        LastName = contactEntity.LastName,
                        Email = contactEntity.Email,
                        StreetName = contactEntity.Address.StreetName,
                        City = contactEntity.Address.City,
                        PostalCode = contactEntity.Address.PostalCode,
                        Country = contactEntity.Address.Country.Country,
                        Continent = contactEntity.Address.Country.Continent,
                        Occupation = contactEntity.Occupation.Occupation,
                        Description = contactEntity.Occupation.Description,
                        Salary = contactEntity.Occupation.Salary.Salary
                    };
                    return contactDto;
                }
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
            return null!;
        }
        public bool RemoveContact(string contactId)
        {
            try
            {
                return _contactRepository.Delete(x => x.Id == contactId);
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
            return false;
        }
    }
}
