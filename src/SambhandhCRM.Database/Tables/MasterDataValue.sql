CREATE TABLE [dbo].[MasterDataValue]
(
    [ValueId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CategoryId] BIGINT NOT NULL,

    -- Value Details
    [ValueCode] NVARCHAR(100) NOT NULL,
    [ValueText] NVARCHAR(500) NOT NULL,
    [ShortText] NVARCHAR(100) NULL,
    [Description] NVARCHAR(1000) NULL,

    -- Configuration
    [DisplayOrder] INT NULL,
    [ColorCode] NVARCHAR(20) NULL, -- For UI display
    [IconClass] NVARCHAR(100) NULL, -- For UI icons

    -- Parent-Child Relationship (for dependent dropdowns)
    [ParentValueId] BIGINT NULL,

    -- Additional Attributes (JSON for flexibility)
    [AdditionalAttributes] NVARCHAR(MAX) NULL,

    -- Flags
    [IsDefault] BIT DEFAULT 0,
    [IsSystemDefined] BIT DEFAULT 0, -- Cannot be deleted if true

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_MasterDataValue_Category] FOREIGN KEY ([CategoryId]) REFERENCES [dbo].[MasterDataCategory]([CategoryId]),
    CONSTRAINT [FK_MasterDataValue_ParentValue] FOREIGN KEY ([ParentValueId]) REFERENCES [dbo].[MasterDataValue]([ValueId]),

    -- Unique Constraints
    CONSTRAINT [UK_MasterDataValue_Code] UNIQUE ([CategoryId], [ValueCode]) WHERE [IsDeleted] = 0
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_MasterDataValue_CategoryId] ON [dbo].[MasterDataValue] ([CategoryId], [DisplayOrder]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_MasterDataValue_ValueCode] ON [dbo].[MasterDataValue] ([ValueCode]);
GO

CREATE NONCLUSTERED INDEX [IX_MasterDataValue_ParentValueId] ON [dbo].[MasterDataValue] ([ParentValueId]) WHERE [IsDeleted] = 0;
GO
