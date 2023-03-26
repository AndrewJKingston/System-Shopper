CREATE TABLE [dbo].[Manufacturer] (
    [ManufacturerID]   INT            IDENTITY (1, 1) NOT NULL,
    [ManufacturerName] VARCHAR (100)  NOT NULL,
    [ManufacturerBio]  VARCHAR (1000) NOT NULL,
    [ManufacturerLogo] VARCHAR (300)  NULL,
    CONSTRAINT [PK_Manufacturer] PRIMARY KEY CLUSTERED ([ManufacturerID] ASC)
);











