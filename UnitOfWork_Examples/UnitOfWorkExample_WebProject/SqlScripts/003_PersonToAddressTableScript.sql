CREATE TABLE [dbo].[PersonToAddress](
	[ID] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[PersonID] [int] NOT NULL,
	[AddressID] [int] NOT NULL,
)
GO

ALTER TABLE [dbo].[PersonToAddress]  WITH CHECK ADD  CONSTRAINT [FK_PersonToAddress_Person] FOREIGN KEY([PersonID])
REFERENCES [dbo].[Person] ([ID])
GO

ALTER TABLE [dbo].[PersonToAddress] CHECK CONSTRAINT [FK_PersonToAddress_Person]
GO

ALTER TABLE [dbo].[PersonToAddress]  WITH CHECK ADD  CONSTRAINT [FK_PersonToAddress_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([ID])
GO

ALTER TABLE [dbo].[PersonToAddress] CHECK CONSTRAINT [FK_PersonToAddress_Address]
GO