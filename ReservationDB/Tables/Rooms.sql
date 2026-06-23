CREATE TABLE [Reservation].[Rooms]
(
    [Id] INT IDENTITY(1,1) NOT NULL,
    [RoomNumber] INT NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_Rooms_IsActive] DEFAULT (1),

    [RoomType_Category] NVARCHAR(50) NOT NULL,
    [RoomType_Description] NVARCHAR(200) NOT NULL,
    [Floor_Number] INT NOT NULL,

    [MaintenanceRequested] BIT NOT NULL CONSTRAINT [DF_Rooms_MaintenanceRequested] DEFAULT (0),
    [MaintenanceReason] NVARCHAR(500) NULL,

    [IsDeleted] BIT NOT NULL CONSTRAINT [DF_Rooms_IsDeleted] DEFAULT (0),
    [HistoryActionId] UNIQUEIDENTIFIER NULL,

    [CreatedById] INT NOT NULL,
    [ModifiedById] INT NOT NULL,

    [CreatedOn] DATETIME NOT NULL CONSTRAINT [DF_Rooms_CreatedOn] DEFAULT (GETUTCDATE()),
    [ModifiedOn] DATETIME NOT NULL CONSTRAINT [DF_Rooms_ModifiedOn] DEFAULT (GETUTCDATE()),

    [Timestamp] ROWVERSION NOT NULL,

    [hValidFrom] DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [hValidTo] DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,

    PERIOD FOR SYSTEM_TIME ([hValidFrom], [hValidTo]),

    CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED ([Id] ASC)
)
WITH
(
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = [Reservation].[Rooms_History]
    )
);
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Rooms_RoomNumber]
ON [Reservation].[Rooms] ([RoomNumber]);
GO