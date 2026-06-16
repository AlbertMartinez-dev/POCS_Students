
CREATE TABLE [Reservation].[Rooms]
(
    -- Part A: core columns
    [Id] UNIQUEIDENTIFIER NOT NULL CONSTRAINT [DF_Rooms_Id] DEFAULT (NEWSEQUENTIALID()),
    [RoomNumber] NVARCHAR(10) NOT NULL,
    [IsActive] BIT NOT NULL CONSTRAINT [DF_Rooms_IsActive] DEFAULT (1),

    -- Part B: owned-type columns (must match HasColumnName in EF config)
    [RoomType_Category] NVARCHAR(50) NOT NULL,
    [RoomType_Description] NVARCHAR(200) NULL,
    [Floor_Number] INT NOT NULL,

    -- Part C: shadow property and audit columns
    [IsDeleted] BIT NOT NULL CONSTRAINT [DF_Rooms_IsDeleted] DEFAULT (0),
    [HistoryActionId] UNIQUEIDENTIFIER NULL,

    [CreatedById] INT NOT NULL,
    [ModifiedById] INT NOT NULL,

    [CreatedOn] DATETIME NOT NULL CONSTRAINT [DF_Rooms_CreatedOn] DEFAULT (GETUTCDATE()),
    [ModifiedOn] DATETIME NOT NULL CONSTRAINT [DF_Rooms_ModifiedOn] DEFAULT (GETUTCDATE()),

    [Timestamp] TIMESTAMP NOT NULL,

    -- Part D: temporal table period columns
    [hValidFrom] DATETIME2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [hValidTo] DATETIME2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,

    PERIOD FOR SYSTEM_TIME ([hValidFrom], [hValidTo]),

    CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED ([Id] ASC)
)
WITH
(
    -- Part D: system versioning
    SYSTEM_VERSIONING = ON
    (
        HISTORY_TABLE = [Reservation].[Rooms_History]
    )
);
GO

-- Part E: index
CREATE UNIQUE NONCLUSTERED INDEX [IX_Rooms_RoomNumber]
ON [Reservation].[Rooms] ([RoomNumber]);
GO

