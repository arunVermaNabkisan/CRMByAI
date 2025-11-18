CREATE TABLE [dbo].[Lead]
(
    [LeadId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [LeadNumber] NVARCHAR(50) NOT NULL, -- Format: LEAD-YYYYMM-9999
    [PartyId] BIGINT NOT NULL,

    -- Lead Source & Classification
    [LeadSource] NVARCHAR(100) NOT NULL, -- Direct Walk-in, Website, Referral - Customer, Referral - Employee, Campaign, Others
    [LeadSourceOther] NVARCHAR(200) NULL,
    [ReferralPartyId] BIGINT NULL, -- If referred by customer
    [ReferralEmployeeId] BIGINT NULL, -- If referred by employee
    [CampaignId] BIGINT NULL, -- If from campaign

    -- Product & Amount
    [ProductInterest] NVARCHAR(100) NOT NULL, -- Term Loan, Working Capital, Guarantee, Others
    [ProductInterestOther] NVARCHAR(200) NULL,
    [LoanAmountRange] NVARCHAR(50) NULL, -- <10L, 10-25L, 25-50L, 50-100L, 1-5Cr, >5Cr
    [EstimatedLoanAmount] DECIMAL(18,2) NULL,
    [RequiredTenureMonths] INT NULL,

    -- Priority & Status
    [Priority] NVARCHAR(20) NOT NULL DEFAULT 'Medium', -- High, Medium, Low
    [Status] NVARCHAR(50) NOT NULL DEFAULT 'New', -- New, In-Progress, Documentation, Submitted to Credit, Dropped, Converted
    [SubStatus] NVARCHAR(100) NULL, -- For additional detail

    -- Assignment
    [AssignedToUserId] BIGINT NOT NULL,
    [RegionalManagerId] BIGINT NULL,
    [AssignedDate] DATETIME NOT NULL,

    -- Follow-up & Activity
    [LastContactDate] DATETIME NULL,
    [NextFollowUpDate] DATETIME NULL,
    [ExpectedClosureDate] DATE NULL,

    -- Conversion
    [IsConverted] BIT DEFAULT 0,
    [ConvertedDate] DATETIME NULL,
    [ConversionValue] DECIMAL(18,2) NULL, -- Actual loan amount sanctioned

    -- Dropped Details
    [IsDropped] BIT DEFAULT 0,
    [DroppedDate] DATETIME NULL,
    [DropReason] NVARCHAR(500) NULL,
    [DropReasonCategory] NVARCHAR(100) NULL, -- Not Interested, High Rate, Better Offer, Documentation Issue, etc.

    -- Notes
    [Notes] NVARCHAR(MAX) NULL,
    [InternalNotes] NVARCHAR(MAX) NULL, -- Not visible to all users

    -- Document Checklist
    [IsKYCReceived] BIT DEFAULT 0,
    [IsFinancialStatementsReceived] BIT DEFAULT 0,
    [IsBusinessDocumentsReceived] BIT DEFAULT 0,
    [IsOtherDocumentsReceived] BIT DEFAULT 0,

    -- Aging
    [DaysInPipeline] AS DATEDIFF(DAY, [CreatedDate], ISNULL([ConvertedDate], ISNULL([DroppedDate], GETUTCDATE()))),

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_Lead_PartyMaster] FOREIGN KEY ([PartyId]) REFERENCES [dbo].[PartyMaster]([PartyId]),
    CONSTRAINT [FK_Lead_ReferralParty] FOREIGN KEY ([ReferralPartyId]) REFERENCES [dbo].[PartyMaster]([PartyId]),

    -- Constraints
    CONSTRAINT [CK_Lead_Priority] CHECK ([Priority] IN ('High', 'Medium', 'Low')),
    CONSTRAINT [CK_Lead_Status] CHECK ([Status] IN ('New', 'In-Progress', 'Documentation', 'Submitted to Credit', 'Dropped', 'Converted')),

    -- Unique Constraint
    CONSTRAINT [UK_Lead_LeadNumber] UNIQUE ([LeadNumber])
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_Lead_PartyId] ON [dbo].[Lead] ([PartyId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_Lead_Status] ON [dbo].[Lead] ([Status]) INCLUDE ([Priority], [AssignedToUserId]);
GO

CREATE NONCLUSTERED INDEX [IX_Lead_AssignedToUserId] ON [dbo].[Lead] ([AssignedToUserId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_Lead_NextFollowUpDate] ON [dbo].[Lead] ([NextFollowUpDate]) WHERE [IsDeleted] = 0 AND [NextFollowUpDate] IS NOT NULL;
GO

CREATE NONCLUSTERED INDEX [IX_Lead_CreatedDate] ON [dbo].[Lead] ([CreatedDate] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_Lead_LeadNumber] ON [dbo].[Lead] ([LeadNumber]);
GO
