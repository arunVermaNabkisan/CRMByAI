CREATE TABLE [dbo].[MasterDataCategory]
(
    [CategoryId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Category Details
    [CategoryCode] NVARCHAR(100) NOT NULL, -- e.g., BUSINESS_SEGMENT, LEAD_SOURCE, PRODUCT_TYPE
    [CategoryName] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(500) NULL,

    -- Configuration
    [AllowUserToAdd] BIT DEFAULT 0, -- Can users add new values
    [IsMandatory] BIT DEFAULT 0, -- Is this a mandatory field
    [DisplayOrder] INT NULL,
    [ParentCategoryId] BIGINT NULL, -- For hierarchical categories

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_MasterDataCategory_ParentCategory] FOREIGN KEY ([ParentCategoryId]) REFERENCES [dbo].[MasterDataCategory]([CategoryId]),

    -- Unique Constraints
    CONSTRAINT [UK_MasterDataCategory_Code] UNIQUE ([CategoryCode]) WHERE [IsDeleted] = 0
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_MasterDataCategory_CategoryCode] ON [dbo].[MasterDataCategory] ([CategoryCode]);
GO

CREATE NONCLUSTERED INDEX [IX_MasterDataCategory_ParentCategoryId] ON [dbo].[MasterDataCategory] ([ParentCategoryId]) WHERE [IsDeleted] = 0;
GO
