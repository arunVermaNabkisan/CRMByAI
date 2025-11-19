CREATE TABLE [dbo].[User]
(
    [UserId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Login Credentials
    [UserName] NVARCHAR(100) NOT NULL,
    [Email] NVARCHAR(200) NOT NULL,
    [PasswordHash] NVARCHAR(500) NOT NULL,
    [PasswordSalt] NVARCHAR(500) NOT NULL,

    -- Personal Details
    [FirstName] NVARCHAR(100) NOT NULL,
    [LastName] NVARCHAR(100) NOT NULL,
    [FullName] AS ([FirstName] + ' ' + [LastName]) PERSISTED,
    [MobileNumber] NVARCHAR(15) NULL,

    -- Employee Details
    [EmployeeCode] NVARCHAR(50) NULL,
    [Designation] NVARCHAR(100) NULL,
    [Department] NVARCHAR(100) NULL,
    [BranchCode] NVARCHAR(50) NULL,
    [RegionCode] NVARCHAR(50) NULL,
    [ZoneCode] NVARCHAR(50) NULL,

    -- Reporting Structure
    [ReportingManagerId] BIGINT NULL, -- FK to User table
    [RegionalManagerId] BIGINT NULL, -- FK to User table

    -- User Type & Status
    [UserType] NVARCHAR(50) NOT NULL DEFAULT 'Employee', -- Employee, Admin, System
    [Status] NVARCHAR(20) NOT NULL DEFAULT 'Active', -- Active, Inactive, Locked, Suspended

    -- Security
    [LastLoginDate] DATETIME NULL,
    [LastLoginIP] NVARCHAR(50) NULL,
    [FailedLoginAttempts] INT DEFAULT 0,
    [LockedOutDate] DATETIME NULL,
    [PasswordChangedDate] DATETIME NULL,
    [MustChangePassword] BIT DEFAULT 0,

    -- Session Management
    [RefreshToken] NVARCHAR(500) NULL,
    [RefreshTokenExpiry] DATETIME NULL,

    -- Preferences
    [ProfilePicture] NVARCHAR(500) NULL,
    [Timezone] NVARCHAR(50) NULL,
    [Language] NVARCHAR(10) DEFAULT 'en',
    [Theme] NVARCHAR(20) DEFAULT 'Light',

    -- Notifications
    [EmailNotifications] BIT DEFAULT 1,
    [SMSNotifications] BIT DEFAULT 1,
    [PushNotifications] BIT DEFAULT 1,

    -- Audit Fields
    [CreatedBy] BIGINT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,
    [DeletedBy] BIGINT NULL,
    [DeletedDate] DATETIME NULL,

    -- Foreign Keys
    CONSTRAINT [FK_User_ReportingManager] FOREIGN KEY ([ReportingManagerId]) REFERENCES [dbo].[User]([UserId]),
    CONSTRAINT [FK_User_RegionalManager] FOREIGN KEY ([RegionalManagerId]) REFERENCES [dbo].[User]([UserId]),



    -- Check Constraints
    CONSTRAINT [CK_User_Status] CHECK ([Status] IN ('Active', 'Inactive', 'Locked', 'Suspended'))
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_User_Email] ON [dbo].[User] ([Email]);
GO

CREATE NONCLUSTERED INDEX [IX_User_EmployeeCode] ON [dbo].[User] ([EmployeeCode]);
GO

CREATE NONCLUSTERED INDEX [IX_User_Status] ON [dbo].[User] ([Status]) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_User_ReportingManagerId] ON [dbo].[User] ([ReportingManagerId]) WHERE [IsDeleted] = 0;
GO
