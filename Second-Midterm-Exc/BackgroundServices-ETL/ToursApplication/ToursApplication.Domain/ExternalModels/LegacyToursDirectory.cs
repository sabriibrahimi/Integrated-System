namespace ToursApplication.Domain.ExternalModels;

public class LegacyToursDirectory
{
    public string Name { get; set; } = null!;
    public int Capacity { get; set; }
}

// CREATE TABLE ToursDirectory (
//     Name VARCHAR(255) PRIMARY KEY,
//     Capacity INT NOT NULL
// );