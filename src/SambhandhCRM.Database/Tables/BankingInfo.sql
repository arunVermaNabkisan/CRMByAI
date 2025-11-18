CREATE TABLE [dbo].[BankingInfo]
(
    [BankingInfoId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PartyId] BIGINT NOT NULL,

    -- Bank Details
    [BankName] NVARCHAR(200) NOT NULL,
    [BranchName] NVARCHAR(200) NULL,
    [IFSCCode] NVARCHAR(20) NULL,
    [AccountNumber] NVARCHAR(100) NULL, -- Encrypted
    [AccountType] NVARCHAR(50) NULL, -- Savings, Current, CC, OD

    -- Relationship Details
    [IsPrimaryBank] BIT DEFAULT 0,
    [BankingSinceYear] INT NULL,
    [RelationshipType] NVARCHAR(100) NULL, -- Depositor, Borrower, Both

    -- Facility Details (if borrower)
    [FacilityType] NVARCHAR(200) NULL, -- Term Loan, CC, OD, etc.
    [SanctionedAmount] DECIMAL(18,2) NULL,
    [OutstandingAmount] DECIMAL(18,2) NULL,
    [AsOnDate] DATE NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_BankingInfo_PartyMaster] FOREIGN KEY ([PartyId]) REFERENCES [dbo].[PartyMaster]([PartyId])
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_BankingInfo_PartyId] ON [dbo].[BankingInfo] ([PartyId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_BankingInfo_BankName] ON [dbo].[BankingInfo] ([BankName]);
GO
