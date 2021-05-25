CREATE TABLE [dbo].[Company] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreateUserId]   INT            DEFAULT ((1)) NOT NULL,
    [ModifyDate]     DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifyUserId]   INT            DEFAULT ((1)) NOT NULL,
    [IsActive]       BIT            DEFAULT ((1)) NOT NULL,
    [Name]           NVARCHAR (150) NULL,
    [Street]         NVARCHAR (150) NULL,
    [City]           NVARCHAR (150) NULL,
    [Branch]         NVARCHAR (150) NULL,
    [PostalCode]     NVARCHAR (10)  NULL,
    [PostName]       NVARCHAR (150) NULL,
    [PostStreet]     NVARCHAR (150) NULL,
    [PostCity]       NVARCHAR (150) NULL,
    [PostPostalCode] NVARCHAR (10)  NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Company_CreateUser] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Company_ModifyUser] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[User] ([Id])
);

