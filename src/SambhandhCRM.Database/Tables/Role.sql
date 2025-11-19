CREATE TABLE [dbo].[Role]
(
    [RoleId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Role Details
    [RoleName] NVARCHAR(100) NOT NULL,
    [RoleCode] NVARCHAR(50) NOT NULL,
    [Description] NVARCHAR(500) NULL,

    -- Role Type
    [RoleType] NVARCHAR(50) NOT NULL DEFAULT 'User', -- System, Admin, User

    -- Permissions (can be expanded to detailed permission table)
    [Permissions] NVARCHAR(MAX) NULL, -- JSON format for permissions

    -- Hierarchy
    [RoleLevel] INT NULL, -- For hierarchy: 1=Admin, 2=Regional Manager, 3=Relationship Manager
    [ParentRoleId] BIGINT NULL, -- FK to Role table for hierarchy

    -- Audit Fields
    [CreatedBy] BIGINT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_Role_ParentRole] FOREIGN KEY ([ParentRoleId]) REFERENCES [dbo].[Role]([RoleId]),


);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_Role_RoleCode] ON [dbo].[Role] ([RoleCode]);
GO

CREATE NONCLUSTERED INDEX [IX_Role_RoleType] ON [dbo].[Role] ([RoleType]) WHERE [IsDeleted] = 0;
GO
