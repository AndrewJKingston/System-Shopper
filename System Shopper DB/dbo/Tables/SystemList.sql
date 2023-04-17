CREATE TABLE [dbo].[SystemList] (
    [SystemListID]      INT NOT NULL,
    [ShoppingSessionID] INT NULL,
    CONSTRAINT [PK_SystemList] PRIMARY KEY CLUSTERED ([SystemListID] ASC),
    CONSTRAINT [FK_SystemList_ShoppingSession] FOREIGN KEY ([ShoppingSessionID]) REFERENCES [dbo].[ShoppingSession] ([ShoppingSessionID])
);

