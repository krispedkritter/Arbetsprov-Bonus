using Arbetsprov_Bonus.Entities;

namespace Arbetsprov_Bonus.Data;

public class ConsultantRepository : IConsultantRepository
{
    private readonly GisysDbContext _gisysDbContext;

    public ConsultantRepository(
        GisysDbContext gisysDbContext
    )
    {
        _gisysDbContext = gisysDbContext;
    }

    /// <inheritdoc/>
    public Consultant Add(Consultant consultant)
    {
        _gisysDbContext.Consultants.Add(consultant);
        _gisysDbContext.SaveChanges();
        return _gisysDbContext.Consultants.Single(c => c.FirstName == consultant.FirstName && c.LastName == consultant.LastName);
    }

    /// <inheritdoc/>
    public IEnumerable<Consultant> Get()
    {
        return _gisysDbContext.Consultants.ToList();
    }

    /// <inheritdoc/>
    public Consultant GetById(int id)
    {
        return _gisysDbContext.Consultants.Single(c => c.Id == id);
    }

    /// <inheritdoc/>
    public void Remove(int id)
    {
        Consultant c = _gisysDbContext.Consultants.Single(c => c.Id == id);
        if(c != null)
        {
            _gisysDbContext.Consultants.Remove(c);
            _gisysDbContext.SaveChanges();
        }
    }

    /// <inheritdoc/>
    public void Update(Consultant consultant)
    {
        Consultant c = _gisysDbContext.Consultants.Single(c => c.Id == consultant.Id);
        if(c != null)
        {
            c.FirstName = consultant.FirstName;
            c.LastName = consultant.LastName;
            c.StartDate = consultant.StartDate;
            _gisysDbContext.SaveChanges();
        }
    }

    /// <inheritdoc/>
    public bool CheckExists(int id)
    {
        return _gisysDbContext.Consultants.Any(c => c.Id == id);
    }

    /// <inheritdoc/>
    public bool CheckExists(string firstName, string lastName)
    {
        return _gisysDbContext.Consultants.Any(c => c.FirstName == firstName && c.LastName == lastName);
    }
}
