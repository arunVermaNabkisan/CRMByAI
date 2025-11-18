CREATE TABLE [dbo].[CommunicationLog]
(
    [CommunicationId] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- Related Entities
    [PartyId] BIGINT NULL,
    [IndividualId] BIGINT NULL,
    [LeadId] BIGINT NULL,

    -- Communication Details
    [CommunicationType] NVARCHAR(50) NOT NULL, -- Phone Call, Email, Meeting, Site Visit, WhatsApp, SMS
    [Direction] NVARCHAR(20) NOT NULL, -- Inbound, Outbound
    [CommunicationDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [Duration] INT NULL, -- In minutes (for calls/meetings)

    -- Contact Details
    [ContactPerson] NVARCHAR(200) NULL,
    [ContactNumber] NVARCHAR(20) NULL,
    [ContactEmail] NVARCHAR(200) NULL,

    -- Content
    [Subject] NVARCHAR(500) NULL,
    [Summary] NVARCHAR(MAX) NULL,
    [DetailedNotes] NVARCHAR(MAX) NULL,

    -- Outcome & Follow-up
    [Outcome] NVARCHAR(500) NULL,
    [NextAction] NVARCHAR(500) NULL,
    [NextFollowUpDate] DATETIME NULL,

    -- Campaign Details (for bulk communication)
    [CampaignId] BIGINT NULL,
    [TemplateId] BIGINT NULL,
    [IsBulkCommunication] BIT DEFAULT 0,

    -- Delivery Status (for SMS/Email)
    [DeliveryStatus] NVARCHAR(50) NULL, -- Sent, Delivered, Failed, Bounced, Opened, Clicked
    [DeliveryStatusDate] DATETIME NULL,
    [DeliveryStatusDetails] NVARCHAR(MAX) NULL,

    -- Attachments
    [HasAttachments] BIT DEFAULT 0,
    [AttachmentCount] INT DEFAULT 0,

    -- Audit Fields
    [CreatedBy] BIGINT NOT NULL,
    [CreatedDate] DATETIME NOT NULL DEFAULT GETUTCDATE(),
    [ModifiedBy] BIGINT NULL,
    [ModifiedDate] DATETIME NULL,
    [IsActive] BIT NOT NULL DEFAULT 1,
    [IsDeleted] BIT NOT NULL DEFAULT 0,

    -- Foreign Keys
    CONSTRAINT [FK_CommunicationLog_PartyMaster] FOREIGN KEY ([PartyId]) REFERENCES [dbo].[PartyMaster]([PartyId]),
    CONSTRAINT [FK_CommunicationLog_Individual] FOREIGN KEY ([IndividualId]) REFERENCES [dbo].[Individual]([IndividualId]),
    CONSTRAINT [FK_CommunicationLog_Lead] FOREIGN KEY ([LeadId]) REFERENCES [dbo].[Lead]([LeadId]),

    -- Constraints
    CONSTRAINT [CK_CommunicationLog_Type] CHECK ([CommunicationType] IN ('Phone Call', 'Email', 'Meeting', 'Site Visit', 'WhatsApp', 'SMS')),
    CONSTRAINT [CK_CommunicationLog_Direction] CHECK ([Direction] IN ('Inbound', 'Outbound'))
);
GO

-- Create Indexes
CREATE NONCLUSTERED INDEX [IX_CommunicationLog_PartyId] ON [dbo].[CommunicationLog] ([PartyId], [CommunicationDate] DESC) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_CommunicationLog_IndividualId] ON [dbo].[CommunicationLog] ([IndividualId], [CommunicationDate] DESC) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_CommunicationLog_LeadId] ON [dbo].[CommunicationLog] ([LeadId], [CommunicationDate] DESC) WHERE [IsDeleted] = 0;
GO

CREATE NONCLUSTERED INDEX [IX_CommunicationLog_Type] ON [dbo].[CommunicationLog] ([CommunicationType], [CommunicationDate] DESC);
GO

CREATE NONCLUSTERED INDEX [IX_CommunicationLog_NextFollowUpDate] ON [dbo].[CommunicationLog] ([NextFollowUpDate]) WHERE [IsDeleted] = 0 AND [NextFollowUpDate] IS NOT NULL;
GO

CREATE NONCLUSTERED INDEX [IX_CommunicationLog_CreatedBy] ON [dbo].[CommunicationLog] ([CreatedBy], [CommunicationDate] DESC);
GO
