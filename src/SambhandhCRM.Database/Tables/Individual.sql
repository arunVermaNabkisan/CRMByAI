CREATE TABLE [dbo].[Individual]
(
    [IndividualId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Personal Details
    [FullName] NVARCHAR(200) NOT NULL,
    [FirstName] NVARCHAR(100) NULL,
    [MiddleName] NVARCHAR(100) NULL,
    [LastName] NVARCHAR(100) NULL,
    [Gender] NVARCHAR(20) NULL, -- Male, Female, Other
    [DateOfBirth] DATE NULL,

    -- Contact Details
    [MobileNumber] NVARCHAR(15) NULL,
    [AlternateMobileNumber] NVARCHAR(15) NULL,
    [EmailAddress] NVARCHAR(200) NULL,
    [AlternateEmailAddress] NVARCHAR(200) NULL,

    -- Identification
    [PANNumber] NVARCHAR(10) NULL,
    [AadhaarNumber] NVARCHAR(100) NULL, -- Encrypted/Masked
    [DINNumber] NVARCHAR(20) NULL, -- Director Identification Number
    [PassportNumber] NVARCHAR(50) NULL,

    -- Professional Details
    [Designation] NVARCHAR(200) NULL,
    [Department] NVARCHAR(200) NULL,
    [Qualification] NVARCHAR(200) NULL,
    [Experience] INT NULL, -- Years of experience

    -- Social Media
    [LinkedInProfile] NVARCHAR(500) NULL,
    [TwitterHandle] NVARCHAR(100) NULL,

    -- Address (Primary)
    [AddressLine1] NVARCHAR(500) NULL,
    [AddressLine2] NVARCHAR(500) NULL,
    [City] NVARCHAR(100) NULL,
    [State] NVARCHAR(100) NULL,
    [PINCode] NVARCHAR(10) NULL,

    -- Flags
    [IsKeyDecisionMaker] BIT DEFAULT 0,
    [IsVerified] BIT DEFAULT 0,
    [VerifiedBy] BIGINT NULL,
    [VerifiedDate] DATETIME NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_Individual_FullName] ON [dbo].[Individual] ([FullName]);
GO

CREATE NONCLUSTERED INDEX [IX_Individual_EmailAddress] ON [dbo].[Individual] ([EmailAddress]);
GO

CREATE NONCLUSTERED INDEX [IX_Individual_CreatedDate] ON [dbo].[Individual] ([CreatedDate] DESC);
GO
