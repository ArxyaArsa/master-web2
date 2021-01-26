IF (OBJECT_ID('PartnerType', 'U') IS NULL)
BEGIN
    CREATE TABLE [PartnerType] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [Name] NVARCHAR(250) NULL,
        CONSTRAINT [PK_PartnerType] PRIMARY KEY CLUSTERED ([Id])
    );
END;

IF (OBJECT_ID('Partner', 'U') IS NULL)
BEGIN
    CREATE TABLE [Partner] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [PartnerTypeId] INT NOT NULL,
        [UserId] INT NOT NULL,
        [Name] NVARCHAR(250) NULL,
        [StartDate] DATETIME NULL,
        [EndDate] DATETIME NULL,
        CONSTRAINT [PK_Partner] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_Partner_CompanyType_CompanyTypeId] FOREIGN KEY ([PartnerTypeId]) REFERENCES [PartnerType] ([Id]),
        CONSTRAINT [FK_Partner_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;

IF (OBJECT_ID('Action', 'U') IS NULL)
BEGIN
    CREATE TABLE [Action] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [Name] NVARCHAR(250) NOT NULL,
        [Description] NVARCHAR(1000) NULL,
        [CashValue] DECIMAL(19,2) NULL,
        [PercentValue] DECIMAL(19,4) NULL,
        [CreatedDate] DATETIME NULL,
        [StartDate] DATETIME NULL,
        [EndDate] DATETIME NULL,
        [IsCanceled] BIT NULL,
        [CancelDate] DATETIME NULL,
        [CancelReason] NVARCHAR(1000) NULL,
        CONSTRAINT [PK_Action] PRIMARY KEY CLUSTERED ([Id])
    );
END;

IF (OBJECT_ID('PartnerActionMap', 'U') IS NULL)
BEGIN
    CREATE TABLE [PartnerActionMap] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [PartnerId] INT NOT NULL,
        [ActionId] INT NOT NULL,
        [CreatedDate] DATETIME NULL,
        CONSTRAINT [PK_PartnerActionMap] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_PartnerActionMap_Partner_PartnerId] FOREIGN KEY ([PartnerId]) REFERENCES [Partner] ([Id]),
        CONSTRAINT [FK_PartnerActionMap_Action_ActionId] FOREIGN KEY ([ActionId]) REFERENCES [Action] ([Id])
    );
END;

IF (OBJECT_ID('UsedAction', 'U') IS NULL)
BEGIN
    CREATE TABLE [UsedAction] (
        [Id] INT NOT NULL IDENTITY(1,1),
        [UserId] INT NOT NULL,
        [ActionId] INT NOT NULL,
        [PartnerId] INT NOT NULL,
        [ActionValue] DECIMAL(19,2) NOT NULL,
        CONSTRAINT [PK_UsedAction] PRIMARY KEY CLUSTERED ([Id]),
        CONSTRAINT [FK_UsedAction_Partner_PartnerId] FOREIGN KEY ([PartnerId]) REFERENCES [Partner] ([Id]),
        CONSTRAINT [FK_UsedAction_Action_ActionId] FOREIGN KEY ([ActionId]) REFERENCES [Action] ([Id]),
        CONSTRAINT [FK_UsedAction_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id])
    );
END;

IF (COL_LENGTH ('AspNetUsers', 'PartnerId') IS NULL)
BEGIN
    ALTER TABLE [AspNetUsers]
    ADD [PartnerId] INT NULL;

    ALTER TABLE [AspNetUsers]
    ADD CONSTRAINT [FK_AspNetUsers_Partner_PartnerId] FOREIGN KEY ([PartnerId]) REFERENCES [Partner] ([Id]);
END;

-- ALTER TABLE [AspNetUsers]
-- DROP CONSTRAINT [FK_AspNetUsers_Partner_PartnerId];
-- 
-- ALTER TABLE [AspNetUsers]
-- DROP [PartnerId];
-- 
-- DROP TABLE IF EXISTS [UsedAction];
-- DROP TABLE IF EXISTS [PartnerActionMap];
-- DROP TABLE IF EXISTS [Action];
-- DROP TABLE IF EXISTS [Partner];
-- DROP TABLE IF EXISTS [PartnerType];
