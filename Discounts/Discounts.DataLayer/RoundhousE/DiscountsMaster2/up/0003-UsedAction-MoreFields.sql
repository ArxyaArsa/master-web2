IF COL_LENGTH('dbo.UsedAction', 'DateCreated') IS NULL
BEGIN
	ALTER TABLE dbo.UsedAction
	ADD DateCreated DATETIME NULL;
END ;

IF COL_LENGTH('dbo.UsedAction', 'OriginalValue') IS NULL
BEGIN
	ALTER TABLE dbo.UsedAction
	ADD OriginalValue DECIMAL(19,2) NOT NULL DEFAULT(0);
END ;