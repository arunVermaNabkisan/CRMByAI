CREATE TABLE [dbo].[AuditLog]
(
    [AuditId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Entity Information
    [EntityType] NVARCHAR(100) NOT NULL, -- Table name: PartyMaster, Lead, User, etc.
    [EntityId] BIGINT NOT NULL,
    [EntityIdentifier] NVARCHAR(500) NULL, -- Friendly identifier like Party Name, Lead Number

    -- Action Details
    [Action] NVARCHAR(50) NOT NULL, -- Insert, Update, Delete, Login, Logout, etc.
    [ActionDescription] NVARCHAR(500) NULL,

    -- Change Details
    [OldValues] NVARCHAR(MAX) NULL, -- JSON format
    [NewValues] NVARCHAR(MAX) NULL, -- JSON format
    [ChangedFields] NVARCHAR(MAX) NULL, -- Comma-separated list or JSON

    -- User & Session Information
    [UserId] BIGINT NULL,
    [UserName] NVARCHAR(100) NULL,
    [SessionId] NVARCHAR(200) NULL,
    [IPAddress] NVARCHAR(50) NULL,
    [UserAgent] NVARCHAR(500) NULL,

    -- Request Details
    [RequestURL] NVARCHAR(1000) NULL,
    [RequestMethod] NVARCHAR(10) NULL, -- GET, POST, PUT, DELETE
    [RequestData] NVARCHAR(MAX) NULL,

    -- Timestamp
    [Timestamp] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ExecutionTimeMs] INT NULL, -- For performance tracking

    -- Status
    [Status] NVARCHAR(20) NULL, -- Success, Failed, Warning
    [ErrorMessage] NVARCHAR(MAX) NULL,

    -- Additional Context
    [Module] NVARCHAR(100) NULL, -- Party Management, Lead Management, etc.
    [Feature] NVARCHAR(100) NULL, -- Create Party, Update Lead, etc.
    [Tags] NVARCHAR(500) NULL, -- For categorization
);
GO

-- Create Indexes for Performance
CREATE NONCLUSTERED INDEX [IX_AuditLog_EntityType_EntityId] ON [dbo].[AuditLog] ([EntityType], [EntityId], [Timestamp] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_AuditLog_UserId] ON [dbo].[AuditLog] ([UserId], [Timestamp] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_AuditLog_Timestamp] ON [dbo].[AuditLog] ([Timestamp] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_AuditLog_Action] ON [dbo].[AuditLog] ([Action], [Timestamp] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_AuditLog_Module] ON [dbo].[AuditLog] ([Module], [Timestamp] DESC);
GO
