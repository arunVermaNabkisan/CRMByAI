-- Sambandh CRM Database Schema
-- SQL Server Database Setup Script

-- Create Database (run this separately if needed)
-- CREATE DATABASE SambhandhCRM;
-- GO
-- USE SambhandhCRM;
-- GO

-- =============================================
-- Table: Customers (Party Master)
-- =============================================
CREATE TABLE Customers (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),

    -- Basic Information
    LegalConstitution INT NOT NULL,
    EntityName NVARCHAR(500) NOT NULL,
    RegistrationNumber NVARCHAR(100),
    CIN NVARCHAR(21),
    PANNumber NVARCHAR(10),
    AadhaarNumber NVARCHAR(12),
    DateOfIncorporation DATE,
    DateOfBirth DATE,
    PrimaryBusinessActivity NVARCHAR(500),
    Occupation NVARCHAR(200),
    AnnualTurnover DECIMAL(18, 2),
    EmployeeCountRange NVARCHAR(50),
    AnnualIncomeRange NVARCHAR(50),

    -- Contact Information
    RegisteredAddress NVARCHAR(1000) NOT NULL,
    OfficeAddress NVARCHAR(1000),
    CorrespondenceAddress NVARCHAR(1000),
    RegisteredPinCode NVARCHAR(10) NOT NULL,
    OfficePinCode NVARCHAR(10),
    CorrespondencePinCode NVARCHAR(10),
    PrimaryPhone NVARCHAR(20) NOT NULL,
    MobileNumber NVARCHAR(20),
    AlternativePhone NVARCHAR(20),
    PrimaryEmail NVARCHAR(200),
    SecondaryEmail NVARCHAR(200),
    Website NVARCHAR(500),
    LinkedInProfile NVARCHAR(500),
    TwitterProfile NVARCHAR(500),

    -- Banking & Financial
    PrimaryBankName NVARCHAR(200),
    BankingSinceYear INT,
    IsExistingNABKISANCustomer BIT DEFAULT 0,
    ExistingProductType NVARCHAR(200),
    OutstandingAmount DECIMAL(18, 2),
    OtherLenderRelationships NVARCHAR(MAX),

    -- Status and Classification
    Status INT NOT NULL DEFAULT 0,
    AssignedToUserId NVARCHAR(450),

    -- MCA Integration Data
    AuthorizedCapital NVARCHAR(100),
    PaidUpCapital NVARCHAR(100),
    MCALastFetchedAt DATETIME2,

    -- Audit Fields
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(200) NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(200),
    IsDeleted BIT NOT NULL DEFAULT 0,

    INDEX IX_Customers_PANNumber (PANNumber),
    INDEX IX_Customers_Status (Status),
    INDEX IX_Customers_IsDeleted (IsDeleted)
);

-- =============================================
-- Table: BusinessSegments
-- =============================================
CREATE TABLE BusinessSegments (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    Name NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    IsActive BIT NOT NULL DEFAULT 1,
    DisplayOrder INT NOT NULL DEFAULT 0,

    -- Audit Fields
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(200) NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(200),
    IsDeleted BIT NOT NULL DEFAULT 0
);

-- =============================================
-- Table: CustomerBusinessSegments (Junction Table)
-- =============================================
CREATE TABLE CustomerBusinessSegments (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    BusinessSegmentId UNIQUEIDENTIFIER NOT NULL,

    -- Audit Fields
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(200) NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(200),
    IsDeleted BIT NOT NULL DEFAULT 0,

    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (BusinessSegmentId) REFERENCES BusinessSegments(Id),
    INDEX IX_CustomerBusinessSegments_CustomerId (CustomerId),
    INDEX IX_CustomerBusinessSegments_BusinessSegmentId (BusinessSegmentId)
);

-- =============================================
-- Table: ContactPersons
-- =============================================
CREATE TABLE ContactPersons (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    FullName NVARCHAR(200) NOT NULL,
    MobileNumber NVARCHAR(20) NOT NULL,
    Email NVARCHAR(200),
    PANNumber NVARCHAR(10),
    DIN NVARCHAR(8),
    LinkedInProfile NVARCHAR(500),

    -- Audit Fields
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(200) NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(200),
    IsDeleted BIT NOT NULL DEFAULT 0,

    INDEX IX_ContactPersons_MobileNumber (MobileNumber)
);

-- =============================================
-- Table: CustomerContactPersons (Junction Table)
-- =============================================
CREATE TABLE CustomerContactPersons (
    CustomerId UNIQUEIDENTIFIER NOT NULL,
    ContactPersonId UNIQUEIDENTIFIER NOT NULL,
    RoleInOrganization NVARCHAR(200) NOT NULL,
    RoleStartDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    IsStillActive BIT NOT NULL DEFAULT 1,
    IsDecisionMaker BIT NOT NULL DEFAULT 0,
    IsPreferredContact BIT NOT NULL DEFAULT 0,
    CustomRoleDescription NVARCHAR(500),

    PRIMARY KEY (CustomerId, ContactPersonId),
    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (ContactPersonId) REFERENCES ContactPersons(Id)
);

-- =============================================
-- Table: Leads
-- =============================================
CREATE TABLE Leads (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    LeadNumber NVARCHAR(50) NOT NULL UNIQUE,
    CustomerId UNIQUEIDENTIFIER NOT NULL,

    -- Lead Details
    LeadSource NVARCHAR(200) NOT NULL,
    ReferralSource NVARCHAR(200),
    ProductInterest NVARCHAR(200) NOT NULL,
    LoanAmountRange NVARCHAR(100),
    Priority INT NOT NULL DEFAULT 1,
    Status INT NOT NULL DEFAULT 0,

    -- Assignment
    AssignedToUserId NVARCHAR(450) NOT NULL,
    AssignedAt DATETIME2,

    -- Activity Tracking
    LastContactDate DATETIME2,
    NextFollowUpDate DATETIME2,
    Notes NVARCHAR(MAX),

    -- Document Checklist
    HasKYCDocuments BIT NOT NULL DEFAULT 0,
    HasFinancialStatements BIT NOT NULL DEFAULT 0,
    HasBusinessDocuments BIT NOT NULL DEFAULT 0,
    HasOtherDocuments BIT NOT NULL DEFAULT 0,

    -- Conversion
    ConvertedAt DATETIME2,
    DropReason NVARCHAR(500),

    -- Audit Fields
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(200) NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(200),
    IsDeleted BIT NOT NULL DEFAULT 0,

    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    INDEX IX_Leads_CustomerId (CustomerId),
    INDEX IX_Leads_Status (Status),
    INDEX IX_Leads_AssignedToUserId (AssignedToUserId)
);

-- =============================================
-- Table: LeadStatusHistory
-- =============================================
CREATE TABLE LeadStatusHistory (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    LeadId UNIQUEIDENTIFIER NOT NULL,
    FromStatus INT NOT NULL,
    ToStatus INT NOT NULL,
    ChangedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    ChangedBy NVARCHAR(200) NOT NULL,
    Notes NVARCHAR(MAX),

    FOREIGN KEY (LeadId) REFERENCES Leads(Id),
    INDEX IX_LeadStatusHistory_LeadId (LeadId)
);

-- =============================================
-- Table: CommunicationLogs
-- =============================================
CREATE TABLE CommunicationLogs (
    Id UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    CustomerId UNIQUEIDENTIFIER,
    LeadId UNIQUEIDENTIFIER,

    CommunicationDate DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CommunicationType INT NOT NULL,
    Direction INT NOT NULL,
    Subject NVARCHAR(500) NOT NULL,
    Summary NVARCHAR(MAX),
    NextActionRequired NVARCHAR(MAX),
    LoggedByUserId NVARCHAR(450) NOT NULL,

    -- For bulk communications
    IsBulkCommunication BIT NOT NULL DEFAULT 0,
    CampaignId UNIQUEIDENTIFIER,
    DeliveryStatus NVARCHAR(100),

    -- Audit Fields
    CreatedAt DATETIME2 NOT NULL DEFAULT GETUTCDATE(),
    CreatedBy NVARCHAR(200) NOT NULL,
    UpdatedAt DATETIME2,
    UpdatedBy NVARCHAR(200),
    IsDeleted BIT NOT NULL DEFAULT 0,

    FOREIGN KEY (CustomerId) REFERENCES Customers(Id),
    FOREIGN KEY (LeadId) REFERENCES Leads(Id),
    INDEX IX_CommunicationLogs_CustomerId (CustomerId),
    INDEX IX_CommunicationLogs_LeadId (LeadId),
    INDEX IX_CommunicationLogs_CommunicationDate (CommunicationDate)
);

-- =============================================
-- Insert Sample Business Segments
-- =============================================
INSERT INTO BusinessSegments (Id, Name, Description, DisplayOrder, CreatedBy)
VALUES
    (NEWID(), 'FPO', 'Farmer Producer Organizations', 1, 'System'),
    (NEWID(), 'Agri-Startup', 'Agricultural Startups', 2, 'System'),
    (NEWID(), 'MFI', 'Microfinance Institutions', 3, 'System'),
    (NEWID(), 'SHG', 'Self Help Groups', 4, 'System'),
    (NEWID(), 'Agri-SME', 'Agricultural SMEs', 5, 'System'),
    (NEWID(), 'Dairy', 'Dairy Cooperatives', 6, 'System'),
    (NEWID(), 'Input Dealer', 'Input Dealers and Distributors', 7, 'System'),
    (NEWID(), 'Others', 'Other Business Segments', 99, 'System');

PRINT 'Database schema created successfully!';
PRINT 'Sample business segments inserted.';
PRINT 'Please update the connection string in appsettings.json to connect to this database.';
