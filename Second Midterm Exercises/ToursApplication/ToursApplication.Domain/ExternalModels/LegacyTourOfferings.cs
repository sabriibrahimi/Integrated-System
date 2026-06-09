namespace ToursApplication.Domain.ExternalModels;

public class LegacyTourOfferings
{
   public string AgencyName { get; set; } = null!;
   public string TourName { get; set; } = null!;
}

// CREATE TABLE TourOfferings (
//     AgencyName VARCHAR(255) NOT NULL,
//     TourName VARCHAR(255) NOT NULL,
//     PRIMARY KEY (AgencyName, TourName),
//     FOREIGN KEY (TourName) REFERENCES ToursDirectory(Name)