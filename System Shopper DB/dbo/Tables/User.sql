CREATE TABLE [dbo].[User] (
    [UserID]         INT           NOT NULL,
    [Email]          VARCHAR (100) NOT NULL,
    [FirstName]      VARCHAR (50)  NOT NULL,
    [LastName]       VARCHAR (50)  NOT NULL,
    [UserProfileURL] VARCHAR (150) NULL,
    [DateJoined]     DATETIME      NULL,
    [PasswordHash]   VARCHAR (200) NULL,
    [Salt]           VARCHAR (15)  NULL,
    [LastLoginTime]  DATETIME      NULL,
    [CartID]         INT           NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_User_Cart] FOREIGN KEY ([CartID]) REFERENCES [dbo].[Cart] ([CartID])
);







