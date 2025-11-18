CREATE TABLE [dbo].[PartyAddress]
(
    [AddressId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PartyId] BIGINT NOT NULL,

    -- Address Type
    [AddressType] NVARCHAR(50) NOT NULL, -- Registered, Office, Correspondence, Residence

    -- Address Details
    [AddressLine1] NVARCHAR(500) NOT NULL,
    [AddressLine2] NVARCHAR(500) NULL,
    [AddressLine3] NVARCHAR(500) NULL,
    [City] NVARCHAR(100) NOT NULL,
    [District] NVARCHAR(100) NULL,
    [State] NVARCHAR(100) NOT NULL,
    [Country] NVARCHAR(100) NOT NULL DEFAULT 'India',
    [PINCode] NVARCHAR(10) NOT NULL,

    -- Geolocation (optional)
    [Latitude] DECIMAL(10,8) NULL,
    [Longitude] DECIMAL(11,8) NULL,

    -- Flags
    [IsPrimary] BIT DEFAULT 0,
    [IsVerified] BIT DEFAULT 0,
    [VerifiedDate] DATETIME NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_PartyAddress_PartyMaster] FOREIGN KEY ([PartyId]) REFERENCES [dbo].[PartyMaster]([PartyId]),

    -- Constraints
    CONSTRAINT [CK_PartyAddress_AddressType] CHECK ([AddressType] IN ('Registered', 'Office', 'Correspondence', 'Residence'))
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_PartyAddress_PartyId] ON [dbo].[PartyAddress] ([PartyId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_PartyAddress_PINCode] ON [dbo].[PartyAddress] ([PINCode]);
GO

CREATE NONCLUSTERED INDEX [IX_PartyAddress_State] ON [dbo].[PartyAddress] ([State]);
GO
