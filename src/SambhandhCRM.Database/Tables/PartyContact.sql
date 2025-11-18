CREATE TABLE [dbo].[PartyContact]
(
    [ContactId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [PartyId] BIGINT NOT NULL,

    -- Contact Type
    [ContactType] NVARCHAR(50) NOT NULL, -- Phone, Mobile, Email, WhatsApp

    -- Contact Details
    [ContactValue] NVARCHAR(200) NOT NULL,
    [ContactLabel] NVARCHAR(100) NULL, -- Office, Mobile, Primary, Secondary, etc.

    -- Flags
    [IsPrimary] BIT DEFAULT 0,
    [IsVerified] BIT DEFAULT 0,
    [VerifiedDate] DATETIME NULL,

    -- Verification Details (for OTP etc.)
    [VerificationCode] NVARCHAR(20) NULL,
    [VerificationCodeExpiry] DATETIME NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_PartyContact_PartyMaster] FOREIGN KEY ([PartyId]) REFERENCES [dbo].[PartyMaster]([PartyId]),

    -- Constraints
    CONSTRAINT [CK_PartyContact_ContactType] CHECK ([ContactType] IN ('Phone', 'Mobile', 'Email', 'WhatsApp'))
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_PartyContact_PartyId] ON [dbo].[PartyContact] ([PartyId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_PartyContact_ContactValue] ON [dbo].[PartyContact] ([ContactValue]);
GO

CREATE NONCLUSTERED INDEX [IX_PartyContact_ContactType] ON [dbo].[PartyContact] ([ContactType]);
GO
