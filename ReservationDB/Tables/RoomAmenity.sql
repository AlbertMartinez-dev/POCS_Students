CREATE TABLE [Reservation].[RoomAmenities]
(
    [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_RoomAmenities_Id] DEFAULT (NEWSEQUENTIALID()),
    [RoomId] INT NOT NULL,
    [Name] NVARCHAR(100) NOT NULL,

    CONSTRAINT [PK_RoomAmenities] PRIMARY KEY CLUSTERED ([Id] ASC),

    CONSTRAINT [FK_RoomAmenities_Rooms_RoomId]
        FOREIGN KEY ([RoomId])
        REFERENCES [Reservation].[Rooms] ([Id])
        ON DELETE CASCADE
);
GO

CREATE NONCLUSTERED INDEX [IX_RoomAmenities_RoomId]
ON [Reservation].[RoomAmenities] ([RoomId]);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_RoomAmenities_RoomId_Name]
ON [Reservation].[RoomAmenities] ([RoomId], [Name]);
GO