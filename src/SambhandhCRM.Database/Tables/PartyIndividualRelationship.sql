CREATE TABLE [dbo].[PartyIndividualRelationship]
(
    [RelationshipId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PartyId] BIGINT NOT NULL,
    [IndividualId] BIGINT NOT NULL,

    -- Relationship Details
    [RoleInOrganization] NVARCHAR(100) NOT NULL, -- Chairman, CEO/MD, Director, CFO, Authorized Signatory, Primary Contact, Other
    [RoleOther] NVARCHAR(200) NULL, -- If RoleInOrganization = 'Other'
    [Department] NVARCHAR(100) NULL,

    -- Dates
    [RoleStartDate] DATE NULL,
    [RoleEndDate] DATE NULL,
    [IsCurrentRole] BIT DEFAULT 1,

    -- Flags
    [IsDecisionMaker] BIT DEFAULT 0,
    [IsAuthorizedSignatory] BIT DEFAULT 0,
    [IsPrimaryContact] BIT DEFAULT 0,
    [IsPreferredContact] BIT DEFAULT 0,

    -- Contact Preferences
    [PreferredContactMethod] NVARCHAR(50) NULL, -- Phone, Email, WhatsApp, Meeting
    [PreferredContactTime] NVARCHAR(100) NULL, -- Morning, Afternoon, Evening

    -- Notes
    [Notes] NVARCHAR(MAX) NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_PartyIndividualRelationship_PartyMaster] FOREIGN KEY ([PartyId]) REFERENCES [dbo].[PartyMaster]([PartyId]),
    CONSTRAINT [FK_PartyIndividualRelationship_Individual] FOREIGN KEY ([IndividualId]) REFERENCES [dbo].[Individual]([IndividualId]),

    -- Unique Constraint (one person can have only one active role in an organization)
   
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_PartyIndividualRelationship_PartyId] ON [dbo].[PartyIndividualRelationship] ([PartyId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_PartyIndividualRelationship_IndividualId] ON [dbo].[PartyIndividualRelationship] ([IndividualId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_PartyIndividualRelationship_Role] ON [dbo].[PartyIndividualRelationship] ([RoleInOrganization]) INCLUDE ([PartyId], [IndividualId]);
GO
