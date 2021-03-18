IF (OBJECT_ID('Report', 'U') IS NULL)
BEGIN
    CREATE TABLE [Report] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [DiscountsUserId] INT NULL,
        [PartnerId] INT NULL,
        [Name] NVARCHAR(200) NOT NULL,
        [CreatedDate] DATETIME NOT NULL,
        [PathToFile] NVARCHAR(1000) NOT NULL,
        [FilterJson] NVARCHAR(MAX) NOT NULL,
        CONSTRAINT [PK_Report] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_Report_Partner_PartnerId] FOREIGN KEY ([PartnerId]) REFERENCES [Partner] ([Id]),
        CONSTRAINT [FK_Report_AspNetUsers_DiscountsUserId] FOREIGN KEY ([DiscountsUserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;