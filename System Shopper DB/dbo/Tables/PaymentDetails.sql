CREATE TABLE [dbo].[PaymentDetails] (
    [PaymentDetailsID] INT           NOT NULL,
    [OrderID]          INT           NOT NULL,
    [Amount]           DECIMAL (18)  NOT NULL,
    [Provider]         VARCHAR (100) NOT NULL,
    [Status]           VARCHAR (50)  NOT NULL,
    [CreatedAt]        DATETIME      NOT NULL,
    CONSTRAINT [PK_PaymentDetails] PRIMARY KEY CLUSTERED ([PaymentDetailsID] ASC)
);

