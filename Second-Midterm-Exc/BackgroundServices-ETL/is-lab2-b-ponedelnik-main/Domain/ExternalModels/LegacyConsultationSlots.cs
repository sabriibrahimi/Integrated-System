namespace Domain.ExternalModels;

public class LegacyConsultationSlots
{
    public int SlotId { get; set; }
    public DateTime SlotStart { get; set; }
    public DateTime SlotEnd { get; set; }
    public int RoomCode { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
}

// CREATE TABLE ConsultationSlots (
//     SlotId    INT       IDENTITY(1,1) NOT NULL,
//     SlotStart DATETIME2 NOT NULL,
//     SlotEnd   DATETIME2 NOT NULL,
//     RoomCode  INT       NOT NULL,
//     CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
//     UpdatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
//     CONSTRAINT PK_ConsultationSlots PRIMARY KEY (SlotId),
//     CONSTRAINT FK_ConsultationSlot_Room FOREIGN KEY (RoomCode)
// REFERENCES RoomDirectory (RoomCode)
//     );