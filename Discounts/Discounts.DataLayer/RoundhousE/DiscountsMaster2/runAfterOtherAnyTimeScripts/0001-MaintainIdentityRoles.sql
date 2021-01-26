
MERGE [AspNetRoles] T
USING (
    SELECT [Name]
    FROM ( 
        VALUES 
            ('admin'),
            ('partner'),
            ('user')
    ) Tmp ([Name])
) S ON T.[Name] = S.[Name]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([Name], [NormalizedName])
    VALUES (S.[Name], UPPER(S.[Name]));

MERGE [AspNetUsers] T
USING (
    SELECT [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]
    FROM ( 
        VALUES
            ('admin@admin.com', 'ADMIN@ADMIN.COM', 'admin@admin.com', 'ADMIN@ADMIN.COM', 0, 'AQAAAAEAACcQAAAAEDFnZyKtIi5LOyH4U3kPEyCgB6OOGCGHVZshos/8PCmqpsOBxvgJBAzgJP+Lcw9NDQ==', 'G2UEXBY6C3OGMNLP4C7YRVU2Z2RFJ3LK', NULL, NULL, 0, 0, NULL, 1, 0)
    ) Tmp ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
) S ON T.[Email] = S.[Email]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
    VALUES (S.[UserName], S.[NormalizedUserName], S.[Email], S.[NormalizedEmail], S.[EmailConfirmed], S.[PasswordHash], S.[SecurityStamp], S.[ConcurrencyStamp], S.[PhoneNumber], S.[PhoneNumberConfirmed], S.[TwoFactorEnabled], S.[LockoutEnd], S.[LockoutEnabled], S.[AccessFailedCount])
--WHEN MATCHED THEN
--    UPDATE SET
--        T.[Username] = S.[Username],
--        T.[NormalizedUserName] = S.[NormalizedUserName],
--        --T.[Email] = S.[Email],
--        T.[NormalizedEmail] = S.[NormalizedEmail],
--        T.[EmailConfirmed] = S.[EmailConfirmed],
--        T.[PasswordHash] = S.[PasswordHash],
--        T.[SecurityStamp] = S.[SecurityStamp],
--        T.[ConcurrencyStamp] = S.[ConcurrencyStamp],
--        T.[PhoneNumber] = S.[PhoneNumber],
--        T.[PhoneNumberConfirmed] = S.[PhoneNumberConfirmed],
--        T.[TwoFactorEnabled] = S.[TwoFactorEnabled],
--        T.[LockoutEnd] = S.[LockoutEnd],
--        T.[LockoutEnabled] = S.[LockoutEnabled],
--        T.[AccessFailedCount] = S.[AccessFailedCount]
;

DECLARE @userId INT = (SELECT [Id] FROM [AspNetUsers] WHERE [Email] = 'admin@admin.com');
DECLARE @roleId INT = (SELECT [Id] FROM [AspNetRoles] WHERE [Name] = 'admin');

MERGE [AspNetUserRoles] T
USING (
    SELECT [UserId], [RoleId]
    FROM (
        VALUES 
            (@userId, @roleId)
    ) Tmp ([UserId], [RoleId])
) S ON T.[UserId] = S.[UserId] AND T.[RoleId] = S.[RoleId]
WHEN NOT MATCHED BY TARGET THEN
    INSERT ([UserId], [RoleId])
    VALUES (S.[UserId], S.[RoleId])
WHEn NOT MATCHED BY SOURCE AND T.[UserId] = @userId THEN
    DELETE;
