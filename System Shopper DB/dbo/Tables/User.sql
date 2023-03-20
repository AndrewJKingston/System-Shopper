CREATE TABLE [dbo].[User] (
    [UserID]         INT           NOT NULL,
    [Email]          VARCHAR (100) NOT NULL,
    [FirstName]      VARCHAR (50)  NOT NULL,
    [LastName]       VARCHAR (50)  NOT NULL,
    [UserProfileURL] VARCHAR (150) NOT NULL,
    [DateJoined]     DATETIME      NOT NULL,
    [PasswordHash]   VARCHAR (200) NOT NULL,
    [Salt]           VARCHAR (15)  NOT NULL,
    [LastLoginTime]  DATETIME      NOT NULL,
    [CartID]         INT           NULL,
    CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([UserID] ASC),
    CONSTRAINT [FK_User_Cart] FOREIGN KEY ([UserID]) REFERENCES [dbo].[Cart] ([CartID])
);

