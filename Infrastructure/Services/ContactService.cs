using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services
{
    public class ContactService(ContactRepository contactRepository, CountryRepository countryRepository, SalaryRepository salaryRepository, AddressRepository addressRepository, OccupationRepository occupationRepository)
    {
        private readonly ContactRepository _contactRepository = contactRepository;
        private readonly CountryRepository _countryRepository = countryRepository;
        private readonly AddressRepository _addressRepository = addressRepository;
        private readonly SalaryRepository _salaryRepository = salaryRepository;
        private readonly OccupationRepository _occupationRepository = occupationRepository;

        public bool CreateContact(ContactDto contact)
        {
            try
            {
                if (!_contactRepository.Exists(x => x.Email == contact.Email))
                {
                    var countryEntity = _countryRepository.GetOne(x => x.Country == contact.Country);
                    countryEntity ??= _countryRepository.Create(new CountryEntity { Id = Guid.NewGuid().ToString(), Country = contact.Country, Continent = contact.Continent });

                    var addressEntity = _addressRepository.GetOne(x => x.StreetName == contact.StreetName && x.PostalCode == contact.PostalCode);
                    addressEntity ??= _addressRepository.Create(new AddressEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        City = contact.City,
                        PostalCode = contact.PostalCode,
                        StreetName = contact.StreetName,
                        Country = countryEntity,
                    });

                    var salaryEntity = _salaryRepository.Create(new SalaryEntity { Id = Guid.NewGuid().ToString(), Salary = contact.Salary });


                    var occupationEntity = _occupationRepository.GetOne(x => x.Occupation == contact.Occupation);
                    occupationEntity ??= _occupationRepository.Create(new OccupationEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        Occupation = contact.Occupation,
                        Description = contact.Description,
                        Salary = salaryEntity
                    });

                    var contactEntity = new ContactEntity
                    {
                        Id = Guid.NewGuid().ToString(),
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        Address = addressEntity,
                        Occupation = occupationEntity
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
        public (ContactDto, ContactEntity) GetOneContact(string email)
        {
            try
            {
                var contactEntity = _contactRepository.GetOne(x => x.Email.ToLower() == email.ToLower());
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

                    return (contactDto, contactEntity);
                }

            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

            return (null!, null!) ;
        }
        public ContactDto UpdateContact(ContactEntity entity, string entityId)
        {
            try
            {
                var contactEntity = _contactRepository.Update(entity, entityId);
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
        public bool RemoveContact(string email)
        {
            try
            {
                return _contactRepository.Delete(x => x.Email.ToLower() == email.ToLower());
            }
            catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
            return false;
        }
    }
}
