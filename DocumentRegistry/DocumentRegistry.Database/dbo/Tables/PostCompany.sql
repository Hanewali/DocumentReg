CREATE TABLE [dbo].[PostCompany] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreateUserId]   INT            DEFAULT ((1)) NOT NULL,
    [ModifyDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifyUserId]   INT            DEFAULT ((1)) NOT NULL,
    [IsActive]       BIT            DEFAULT ((1)) NOT NULL,
    [Name]           NVARCHAR (150) NULL,
    [Code]           NVARCHAR (150) NULL,
    [City]           NVARCHAR (150) NULL,
    [Street]         NVARCHAR (150) NULL,
    [ContractNumber] NVARCHAR (150) NULL,
    [ContractDate]   DATE           NULL,
    [PostOffice]     NVARCHAR (150) NULL,
    CONSTRAINT [PK_PostCompany] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_PostCompany_CreateUserId] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_PostCompany_ModifyUser] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[User] ([Id])
);

