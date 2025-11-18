CREATE TABLE [dbo].[Document]
(
    [DocumentId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Related Entities
    [EntityType] NVARCHAR(50) NOT NULL, -- Party, Individual, Lead, Communication
    [EntityId] BIGINT NOT NULL,

    -- Document Details
    [DocumentType] NVARCHAR(100) NOT NULL, -- KYC, Financial Statement, Business Document, Photo, etc.
    [DocumentCategory] NVARCHAR(100) NULL, -- Sub-category
    [DocumentName] NVARCHAR(500) NOT NULL,
    [DocumentNumber] NVARCHAR(100) NULL, -- For identifiable documents like PAN, Aadhaar
    [IssueDate] DATE NULL,
    [ExpiryDate] DATE NULL,

    -- File Details
    [FileName] NVARCHAR(500) NOT NULL,
    [FileExtension] NVARCHAR(20) NULL,
    [FileSize] BIGINT NULL, -- In bytes
    [FilePath] NVARCHAR(1000) NOT NULL, -- Physical/Cloud storage path
    [FileHash] NVARCHAR(500) NULL, -- For integrity check
    [MimeType] NVARCHAR(100) NULL,

    -- Storage Details
    [StorageType] NVARCHAR(50) NOT NULL DEFAULT 'Local', -- Local, Azure, AWS, etc.
    [StorageContainer] NVARCHAR(200) NULL, -- Bucket/Container name
    [StorageURL] NVARCHAR(1000) NULL, -- Public URL if applicable

    -- Verification
    [IsVerified] BIT DEFAULT 0,
    [VerifiedBy] BIGINT NULL,
    [VerifiedDate] DATETIME NULL,
    [VerificationNotes] NVARCHAR(MAX) NULL,

    -- Security
    [IsConfidential] BIT DEFAULT 0,
    [AccessLevel] NVARCHAR(50) NULL, -- Public, Internal, Confidential, Restricted

    -- Version Control
    [Version] INT DEFAULT 1,
    [ParentDocumentId] BIGINT NULL, -- For version history
    [IsLatestVersion] BIT DEFAULT 1,

    -- Metadata
    [Tags] NVARCHAR(MAX) NULL, -- Comma-separated or JSON
    [Description] NVARCHAR(1000) NULL,
    [Notes] NVARCHAR(MAX) NULL,

    -- Audit Fields
    [UploadedBy] BIGINT NOT NULL,
    [UploadedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DeletedBy] BIGINT NULL,
    [DeletedDate] DATETIME NULL,

    -- Foreign Keys
    CONSTRAINT [FK_Document_ParentDocument] FOREIGN KEY ([ParentDocumentId]) REFERENCES [dbo].[Document]([DocumentId]),

    -- Constraints
    CONSTRAINT [CK_Document_EntityType] CHECK ([EntityType] IN ('Party', 'Individual', 'Lead', 'Communication', 'User'))
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_Document_EntityType_EntityId] ON [dbo].[Document] ([EntityType], [EntityId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_Document_DocumentType] ON [dbo].[Document] ([DocumentType]);
GO

CREATE NONCLUSTERED INDEX [IX_Document_UploadedDate] ON [dbo].[Document] ([UploadedDate] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_Document_ParentDocumentId] ON [dbo].[Document] ([ParentDocumentId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_Document_ExpiryDate] ON [dbo].[Document] ([ExpiryDate]) WHERE [ExpiryDate] IS NOT NULL AND [IsDeleted] = 0;
GO
