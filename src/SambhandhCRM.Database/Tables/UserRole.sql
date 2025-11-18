CREATE TABLE [dbo].[UserRole]
(
    [UserRoleId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [UserId] BIGINT NOT NULL,
    [RoleId] BIGINT NOT NULL,

    -- Assignment Details
    [AssignedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ExpiryDate] DATETIME NULL,
    [IsTemporary] BIT DEFAULT 0,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]([UserId]),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role]([RoleId]),

    -- Unique Constraint (one user can have a role only once actively)
    CONSTRAINT [UK_UserRole] UNIQUE ([UserId], [RoleId]) WHERE [IsDeleted] = 0 AND [IsActive] = 1
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_UserRole_UserId] ON [dbo].[UserRole] ([UserId]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId] ON [dbo].[UserRole] ([RoleId]) WHERE [IsDeleted] = 0;
GO
