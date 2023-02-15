using Arbetsprov_Bonus.Entities;

namespace Arbetsprov_Bonus.Data;

public interface IConsultantRepository
{
    /// <summary>
    /// Retrieves all the Consultants from the InMemory database.
    /// </summary>
    /// <returns>An enumerable collection of 'Consultant' objects.</returns>
    IEnumerable<Consultant> Get();

    /// <summary>
    /// Retrieves a single Consultant by the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Consultant.</param>
    /// <returns>A 'Consultant' object, if found, or 'null' otherwise.</returns>
    Consultant GetById(int id);

    /// <summary>
    /// Removes a single Consultant with the specified ID.
    /// </summary>
    /// <param name="id">The unique identifier of the Consultant.</param>
    void Remove(int id);

    /// <summary>
    /// Adds a new Consultant to the database with the specified first name, last name, and start date.
    /// </summary>
    /// <param name="consultant">The 'Consultant' object to add.</param>
    /// <returns>The newly added 'Consultant' object.</returns>
    Consultant Add(Consultant consultant);

    /// <summary>
    /// Updates a single Consultant by the specified ID with the provided first name and last name.
    /// </summary>
    /// <param name="consultant">The 'Consultant' object with the updated information.</param>
    void Update(Consultant consultant);

    /// <summary>
    /// Checks if a Consultant with the specified ID exists in the database.
    /// </summary>
    /// <param name="id">The unique identifier of the Consultant.</param>
    /// <returns>'true' if the Consultant exists, 'false' otherwise.</returns>
    bool CheckExists(int id);

    /// <summary>
    /// Checks if a Consultant with the specified first and last name exists in the database.
    /// </summary>
    /// <param name="firstName">The first name of the Consultant.</param>
    /// <param name="lastName">The last name of the Consultant.</param>
    /// <returns>'true' if the Consultant exists, 'false' otherwise.</returns>
    bool CheckExists(string firstName, string lastName);
}