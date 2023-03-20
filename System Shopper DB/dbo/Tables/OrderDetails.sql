CREATE TABLE [dbo].[OrderDetails] (
    [OrderDetailsID]   INT          NOT NULL,
    [UserID]           INT          NOT NULL,
    [Total]            DECIMAL (18) NOT NULL,
    [PaymentDetailsID] INT          NOT NULL,
    [CreatedAt]        DATETIME     NOT NULL,
    CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED ([OrderDetailsID] ASC),
    CONSTRAINT [FK_OrderDetails_PaymentDetails] FOREIGN KEY ([OrderDetailsID]) REFERENCES [dbo].[PaymentDetails] ([PaymentDetailsID]),
    CONSTRAINT [FK_OrderDetails_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([UserID])
);

