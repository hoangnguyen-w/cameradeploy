IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE TABLE [carManagements] (
        [CarManagementID] int NOT NULL IDENTITY,
        [CarName] nvarchar(100) NULL,
        [CarColor] nvarchar(100) NULL,
        [LicensePlates] nvarchar(100) NULL,
        [CarBrand] nvarchar(100) NULL,
        CONSTRAINT [PK_carManagements] PRIMARY KEY ([CarManagementID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE TABLE [Roles] (
        [RoleID] int NOT NULL IDENTITY,
        [RoleName] nvarchar(30) NOT NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleID])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE TABLE [Carlocators] (
        [CarLocatorID] int NOT NULL IDENTITY,
        [location] nvarchar(max) NULL,
        [CarManagementID] int NOT NULL,
        CONSTRAINT [PK_Carlocators] PRIMARY KEY ([CarLocatorID]),
        CONSTRAINT [FK_Carlocators_carManagements_CarManagementID] FOREIGN KEY ([CarManagementID]) REFERENCES [carManagements] ([CarManagementID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE TABLE [Accounts] (
        [AccountID] int NOT NULL IDENTITY,
        [AccounName] nvarchar(100) NOT NULL,
        [password] nvarchar(100) NOT NULL,
        [AccountEmail] nvarchar(200) NULL,
        [Phone] nvarchar(20) NULL,
        [FullName] nvarchar(100) NULL,
        [Image] nvarchar(max) NULL,
        [RoleID] int NOT NULL,
        [RefreshToken] nvarchar(max) NULL,
        [TokenCreated] datetime2 NOT NULL,
        [TokenExpires] datetime2 NOT NULL,
        CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountID]),
        CONSTRAINT [FK_Accounts_Roles_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [Roles] ([RoleID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE TABLE [NotifiHistories] (
        [HistoryID] int NOT NULL IDENTITY,
        [HistoryName] nvarchar(100) NULL,
        [HistoryDate] datetime2 NOT NULL,
        [HistoryStatus] bit NOT NULL,
        [AccountID] int NOT NULL,
        [CarManagementID] int NOT NULL,
        CONSTRAINT [PK_NotifiHistories] PRIMARY KEY ([HistoryID]),
        CONSTRAINT [FK_NotifiHistories_Accounts_AccountID] FOREIGN KEY ([AccountID]) REFERENCES [Accounts] ([AccountID]) ON DELETE CASCADE,
        CONSTRAINT [FK_NotifiHistories_carManagements_CarManagementID] FOREIGN KEY ([CarManagementID]) REFERENCES [carManagements] ([CarManagementID]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleID', N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] ON;
    EXEC(N'INSERT INTO [Roles] ([RoleID], [RoleName])
    VALUES (1, N''Admin''),
    (2, N''Customer''),
    (3, N''Owner'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'RoleID', N'RoleName') AND [object_id] = OBJECT_ID(N'[Roles]'))
        SET IDENTITY_INSERT [Roles] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountID', N'AccounName', N'AccountEmail', N'FullName', N'Image', N'Phone', N'RefreshToken', N'RoleID', N'TokenCreated', N'TokenExpires', N'password') AND [object_id] = OBJECT_ID(N'[Accounts]'))
        SET IDENTITY_INSERT [Accounts] ON;
    EXEC(N'INSERT INTO [Accounts] ([AccountID], [AccounName], [AccountEmail], [FullName], [Image], [Phone], [RefreshToken], [RoleID], [TokenCreated], [TokenExpires], [password])
    VALUES (1, N''admin'', N''Admin@gmail.com'', N''ADMIN'', NULL, NULL, NULL, 1, ''0001-01-01T00:00:00.0000000'', ''0001-01-01T00:00:00.0000000'', N''123'')');
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountID', N'AccounName', N'AccountEmail', N'FullName', N'Image', N'Phone', N'RefreshToken', N'RoleID', N'TokenCreated', N'TokenExpires', N'password') AND [object_id] = OBJECT_ID(N'[Accounts]'))
        SET IDENTITY_INSERT [Accounts] OFF;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE INDEX [IX_Accounts_RoleID] ON [Accounts] ([RoleID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE INDEX [IX_Carlocators_CarManagementID] ON [Carlocators] ([CarManagementID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE INDEX [IX_NotifiHistories_AccountID] ON [NotifiHistories] ([AccountID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    CREATE INDEX [IX_NotifiHistories_CarManagementID] ON [NotifiHistories] ([CarManagementID]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230409183306_addDatabase')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230409183306_addDatabase', N'7.0.3');
END;
GO

COMMIT;
GO

