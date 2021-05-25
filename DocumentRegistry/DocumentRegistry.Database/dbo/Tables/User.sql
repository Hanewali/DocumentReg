CREATE TABLE [dbo].[User] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [CreateDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [CreateUserId] INT            DEFAULT ((1)) NOT NULL,
    [ModifyDate]   DATETIME       DEFAULT (getdate()) NOT NULL,
    [ModifyUserId] INT            DEFAULT ((1)) NOT NULL,
    [IsActive]     BIT            DEFAULT ((1)) NOT NULL,
    [FirstName]    NVARCHAR (150) NULL,
    [LastName]     NVARCHAR (150) NULL,
    [Login]        NVARCHAR (150) NULL,
    [Email]        NVARCHAR (150) NOT NULL,
    [PasswordHash] NVARCHAR (500) NOT NULL,
    [PasswordSalt] NVARCHAR (150) NOT NULL,
    [IsAdmin]      BIT            NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
);

