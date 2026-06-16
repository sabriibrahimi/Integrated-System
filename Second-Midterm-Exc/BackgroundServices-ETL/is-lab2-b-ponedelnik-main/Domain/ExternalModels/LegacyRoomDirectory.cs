namespace Domain.ExternalModels;

public class LegacyRoomDirectory
{
   public int RoomCode { get; set; }
   public string RoomName { get; set; } = null!;
   public int MaxCapacity { get; set; }
   public bool IsActive { get; set; }
   public DateTime CreatedAt { get; set; }
   public DateTime UpdatedAt { get; set; }
}

// CREATE TABLE RoomDirectory (
//     RoomCode    INT           IDENTITY(1,1) NOT NULL,
//     RoomName    NVARCHAR(200) NOT NULL,
//     MaxCapacity INT           NOT NULL DEFAULT 30,
//     IsActive    BIT           NOT NULL DEFAULT 1,
//     CreatedAt   DATETIME2     NOT NULL DEFAULT GETUTCDATE(),
//     UpdatedAt   DATETIME2     NOT NULL DEFAULT GETUTCDATE(),
//     CONSTRAINT PK_RoomDirectory PRIMARY KEY (RoomCode)
// );