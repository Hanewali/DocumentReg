CREATE TABLE [dbo].[DocumentType] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreateUserId]   INT            DEFAULT ((1)) NOT NULL,
    [ModifyDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifyUserId]   INT            DEFAULT ((1)) NOT NULL,
    [IsActive]       BIT            DEFAULT ((1)) NOT NULL,
    [Name] NVARCHAR (150) NOT NULL,
    CONSTRAINT [PK_DocumentType] PRIMARY KEY CLUSTERED ([Id] ASC)
);

