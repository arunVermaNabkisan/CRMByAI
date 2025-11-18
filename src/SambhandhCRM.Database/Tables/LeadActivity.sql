CREATE TABLE [dbo].[LeadActivity]
(
    [ActivityId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [LeadId] BIGINT NOT NULL,

    -- Activity Details
    [ActivityType] NVARCHAR(100) NOT NULL, -- Status Change, Follow-up, Document Received, Note Added, Assignment Changed
    [ActivityDescription] NVARCHAR(MAX) NULL,

    -- Status Change Details
    [OldStatus] NVARCHAR(50) NULL,
    [NewStatus] NVARCHAR(50) NULL,

    -- Follow-up Details
    [FollowUpType] NVARCHAR(50) NULL, -- Call, Email, Meeting, Site Visit
    [FollowUpOutcome] NVARCHAR(500) NULL,

    -- Next Action
    [NextAction] NVARCHAR(500) NULL,
    [NextActionDate] DATETIME NULL,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [IsActive] BIT NOT NULL DEFAULT 1,

    -- Foreign Keys
    CONSTRAINT [FK_LeadActivity_Lead] FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead]([LeadId])
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_LeadActivity_LeadId] ON [dbo].[LeadActivity] ([LeadId], [CreatedDate] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_LeadActivity_ActivityType] ON [dbo].[LeadActivity] ([ActivityType]);
GO
