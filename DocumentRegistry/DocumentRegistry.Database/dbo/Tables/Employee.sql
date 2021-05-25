CREATE TABLE [dbo].[Employee] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreateUserId] INT            DEFAULT ((1)) NOT NULL,
    [ModifyDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifyUserId] INT            DEFAULT ((1)) NOT NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    [FirstName]    NVARCHAR (150) NOT NULL,
    [LastName]     NVARCHAR (150) NOT NULL,
    [CompanyId]    INT            NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Employee_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([Id]),
    CONSTRAINT [FK_Employee_CreateUser] FOREIGN KEY ([CreateUserId]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Employee_ModifyUser] FOREIGN KEY ([ModifyUserId]) REFERENCES [dbo].[User] ([Id])
);

