CREATE TABLE [dbo].[PartyMaster]
(
    [PartyId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Legal Constitution & Classification
    [LegalConstitution] NVARCHAR(50) NOT NULL, -- Company, Society, Trust/NGO, Partnership/LLP, Individual/Proprietor
    [BusinessSegment] NVARCHAR(100) NULL, -- FPO, Agri-Startup, MFI, NBFC, HFC, CSR Partner, PACS, Others
    [BusinessSegmentOther] NVARCHAR(200) NULL, -- If BusinessSegment = 'Others'

    -- Organization Details (for non-individuals)
    [EntityName] NVARCHAR(500) NULL,
    [RegistrationNumber] NVARCHAR(100) NULL, -- CIN/Society Reg/Trust Reg
    [DateOfIncorporation] DATE NULL,
    [PrimaryBusinessActivity] NVARCHAR(500) NULL,
    [AnnualTurnover] DECIMAL(18,2) NULL, -- Last FY
    [EmployeeCountRange] NVARCHAR(50) NULL, -- 0-10, 11-50, 51-200, 201-500, 500+

    -- Individual Details (for individuals/proprietors)
    [FullName] NVARCHAR(200) NULL,
    [DateOfBirth] DATE NULL,
    [Occupation] NVARCHAR(200) NULL,
    [AnnualIncomeRange] NVARCHAR(50) NULL, -- <5L, 5-10L, 10-25L, 25-50L, 50L+

    -- Common Fields
    [PANNumber] NVARCHAR(10) NULL,
    [AadhaarNumber] NVARCHAR(100) NULL, -- Encrypted/Masked
    [GSTNumber] NVARCHAR(15) NULL,

    -- MCA Integration (for Companies)
    [CINNumber] NVARCHAR(21) NULL,
    [IsMCAVerified] BIT DEFAULT 0,
    [MCAVerificationDate] DATETIME NULL,
    [MCAData] NVARCHAR(MAX) NULL, -- JSON data from MCA API

    -- Website & Social Media
    [Website] NVARCHAR(500) NULL,
    [LinkedInProfile] NVARCHAR(500) NULL,
    [TwitterHandle] NVARCHAR(100) NULL,

    -- Customer Status & Classification
    [Status] NVARCHAR(50) NOT NULL DEFAULT 'Prospect', -- Prospect, Active Lead, Customer, Dormant, Archived
    [IsExistingCustomer] BIT DEFAULT 0,
    [CustomerSince] DATE NULL,
    [ExistingProductType] NVARCHAR(200) NULL,
    [OutstandingAmount] DECIMAL(18,2) NULL,

    -- Assignment & Ownership
    [AssignedToUserId] BIGINT NULL, -- FK to User table
    [RegionalManagerId] BIGINT NULL, -- FK to User table

    -- Other Relationships
    [OtherLenders] NVARCHAR(MAX) NULL, -- JSON array of lender names

    -- Data Quality
    [DataQualityScore] INT NULL, -- 0-100 based on completeness
    [IsVerified] BIT DEFAULT 0,
    [VerifiedBy] BIGINT NULL, -- FK to User table
    [VerifiedDate] DATETIME NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DeletedBy] BIGINT NULL,
    [DeletedDate] DATETIME NULL,

    -- Indexes
    CONSTRAINT [UK_PartyMaster_PANNumber] UNIQUE ([PANNumber]) WHERE [PANNumber] IS NOT NULL AND [IsDeleted] = 0,
    CONSTRAINT [UK_PartyMaster_CINNumber] UNIQUE ([CINNumber]) WHERE [CINNumber] IS NOT NULL AND [IsDeleted] = 0,
    CONSTRAINT [UK_PartyMaster_RegistrationNumber] UNIQUE ([RegistrationNumber]) WHERE [RegistrationNumber] IS NOT NULL AND [IsDeleted] = 0,

    -- Constraints
    CONSTRAINT [CK_PartyMaster_LegalConstitution] CHECK ([LegalConstitution] IN ('Company', 'Society', 'Trust/NGO', 'Partnership/LLP', 'Individual/Proprietor')),
    CONSTRAINT [CK_PartyMaster_Status] CHECK ([Status] IN ('Prospect', 'Active Lead', 'Customer', 'Dormant', 'Archived'))
);
GO

-- Create Indexes for Performance
CREATE NONCLUSTERED INDEX [IX_PartyMaster_Status] ON [dbo].[PartyMaster] ([Status]) INCLUDE ([EntityName], [FullName]);
GO

CREATE NONCLUSTERED INDEX [IX_PartyMaster_AssignedToUserId] ON [dbo].[PartyMaster] ([AssignedToUserId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_PartyMaster_LegalConstitution] ON [dbo].[PartyMaster] ([LegalConstitution]) INCLUDE ([BusinessSegment]);
GO

CREATE NONCLUSTERED INDEX [IX_PartyMaster_CreatedDate] ON [dbo].[PartyMaster] ([CreatedDate] DESC);
GO
