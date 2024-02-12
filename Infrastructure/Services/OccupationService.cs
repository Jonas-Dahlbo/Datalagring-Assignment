using Infrastructure.Dtos;
using Infrastructure.Entities;
using Infrastructure.Repositories;
using System.Diagnostics;

namespace Infrastructure.Services;

public class OccupationService(OccupationRepository occupationRepository, SalaryRepository salaryRepository)
{
    private readonly OccupationRepository _occupationRepository = occupationRepository;
    private readonly SalaryRepository _salaryRepository = salaryRepository;

    public OccupationDto CreateOccupation(OccupationDto job)
    {
        try
        {
            if (!_occupationRepository.Exists(x => x.Occupation == job.Occupation && x.Salary.Salary == job.Salary))
            {
                var salaryEntity = _salaryRepository.GetOne(x => x.Salary == job.Salary);
                salaryEntity ??= _salaryRepository.Create(new SalaryEntity { Salary = job.Salary });

                var occupationEntity = new OccupationEntity
                {
                    Occupation = job.Occupation,
                    Description = job.Description,
                    SalaryId = salaryEntity.Id,
                };

                var result = _occupationRepository.Create(occupationEntity);
                if (result != null)
                    return job;
            }
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }
        return null!;
    }

    public IEnumerable<OccupationDto> GetAllOccupations()
    {
        var occupations = new List<OccupationDto>();
        try
        {
            var result = _occupationRepository.GetAll();
            foreach (var job in result)
                occupations.Add(new OccupationDto
                {
                    Occupation = job.Occupation,
                    Description = job.Description,
                    Salary = job.Salary.Salary
                });
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return occupations;
    }

    public OccupationDto GetOneOccupation(string job)
    {
        try
        {
            var occupationEntity = _occupationRepository.GetOne(x => x.Occupation == job);
            if (occupationEntity != null)
            {
                var occupationDto = new OccupationDto
                {
                    Occupation = occupationEntity.Occupation,
                    Description = occupationEntity.Description,
                    Salary = occupationEntity.Salary.Salary,
                };

                return occupationDto;
            }

        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }

        return null!;
    }

    public OccupationDto UpdateOccupation(OccupationEntity entity)
    {
        try
        {
            var occupationEntity = _occupationRepository.Update(entity);
            if (occupationEntity != null)
            {
                var occupationDto = new OccupationDto
                {
                    Occupation = occupationEntity.Occupation,
                    Description = occupationEntity.Description,
                    Salary = occupationEntity.Salary.Salary
                };
                return occupationDto;
            }
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return null!;
    }
    public bool RemoveOccupation(string occupationId)
    {
        try
        {
            return _occupationRepository.Delete(x => x.Id == occupationId);
        }
        catch (Exception ex) { Debug.WriteLine("ERROR :: " + ex.Message); }
        return false;
    }
}
