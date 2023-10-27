CREATE TABLE [dbo].[Table1]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Vendor] NVARCHAR(50) NOT NULL, 
    [InvoiceNumber] NVARCHAR(10) NOT NULL, 
    [InvoiceDate] DATE NOT NULL, 
    [InvoiceTotal] MONEY NOT NULL, 
    [Paymenttotal] MONEY NOT NULL, 
    [InvoiceDueDate] DATE NOT NULL, 
    [PaymentDate] DATE NULL
)
