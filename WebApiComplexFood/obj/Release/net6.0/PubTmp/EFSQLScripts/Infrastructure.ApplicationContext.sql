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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [Admins] (
        [Id] int NOT NULL IDENTITY,
        [Email] nvarchar(255) NOT NULL,
        [Password] nvarchar(255) NOT NULL,
        [FirstName] nvarchar(255) NOT NULL,
        [LastName] nvarchar(255) NOT NULL,
        CONSTRAINT [PK_Admins] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [Buyers] (
        [Id] int NOT NULL IDENTITY,
        [Email] nvarchar(255) NOT NULL,
        [Password] nvarchar(max) NOT NULL,
        [FirstName] nvarchar(255) NOT NULL,
        [LastName] nvarchar(255) NOT NULL,
        [Phone] nvarchar(14) NOT NULL,
        [Gender] nvarchar(max) NOT NULL,
        [Confirmed] bit NOT NULL DEFAULT CAST(0 AS bit),
        [Balance] real NOT NULL DEFAULT CAST(0 AS real),
        CONSTRAINT [PK_Buyers] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [Products] (
        [Id] int NOT NULL IDENTITY,
        [Category] int NOT NULL,
        [Title] nvarchar(255) NOT NULL,
        [Description] nvarchar(255) NOT NULL,
        [Price] real NOT NULL,
        [Image] nvarchar(max) NULL,
        [DateCreated] datetime2 NOT NULL,
        [DateUpdated] datetime2 NOT NULL,
        [IsInStock] bit NOT NULL,
        CONSTRAINT [PK_Products] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [Coupons] (
        [Id] int NOT NULL IDENTITY,
        [Type] int NOT NULL,
        [DateCreated] datetime2 NOT NULL,
        [Code] nvarchar(6) NOT NULL,
        [BuyerId] int NOT NULL,
        CONSTRAINT [PK_Coupons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Coupons_Buyers_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [Buyers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [Orders] (
        [Id] int NOT NULL IDENTITY,
        [TotalPrice] real NOT NULL,
        [DatePlaced] datetime2 NOT NULL,
        [Status] int NOT NULL,
        [Discount] int NOT NULL,
        [Code] nvarchar(6) NOT NULL,
        [BuyerId] int NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Orders_Buyers_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [Buyers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [ShoppingCarts] (
        [Id] int NOT NULL IDENTITY,
        [TotalPrice] real NOT NULL,
        [DatePlaced] datetime2 NOT NULL,
        [Discount] int NOT NULL,
        [Code] nvarchar(450) NOT NULL,
        [BuyerId] int NOT NULL,
        CONSTRAINT [PK_ShoppingCarts] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_ShoppingCarts_Buyers_BuyerId] FOREIGN KEY ([BuyerId]) REFERENCES [Buyers] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [OrderItems] (
        [Id] int NOT NULL IDENTITY,
        [Amount] int NOT NULL,
        [Category] int NOT NULL,
        [Title] nvarchar(max) NOT NULL,
        [Description] nvarchar(max) NOT NULL,
        [Price] real NOT NULL,
        [OrderId] int NULL,
        CONSTRAINT [PK_OrderItems] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE TABLE [ShoppingItems] (
        [ShoppingCartId] int NOT NULL,
        [ProductId] int NOT NULL,
        CONSTRAINT [PK_ShoppingItems] PRIMARY KEY ([ProductId], [ShoppingCartId]),
        CONSTRAINT [FK_ShoppingItems_Products_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ShoppingItems_ShoppingCarts_ShoppingCartId] FOREIGN KEY ([ShoppingCartId]) REFERENCES [ShoppingCarts] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_Admins_Email] ON [Admins] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_Buyers_Email] ON [Buyers] ([Email]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE INDEX [IX_Coupons_BuyerId] ON [Coupons] ([BuyerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_Coupons_Code] ON [Coupons] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE INDEX [IX_Orders_BuyerId] ON [Orders] ([BuyerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_Orders_Code] ON [Orders] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_ShoppingCarts_BuyerId] ON [ShoppingCarts] ([BuyerId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_ShoppingCarts_Code] ON [ShoppingCarts] ([Code]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE UNIQUE INDEX [IX_ShoppingItems_ProductId] ON [ShoppingItems] ([ProductId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    CREATE INDEX [IX_ShoppingItems_ShoppingCartId] ON [ShoppingItems] ([ShoppingCartId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428141515_InitialModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220428141515_InitialModel', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428142252_ConfigureShoppingCartItem')
BEGIN
    DROP INDEX [IX_ShoppingItems_ProductId] ON [ShoppingItems];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428142252_ConfigureShoppingCartItem')
BEGIN
    ALTER TABLE [ShoppingItems] ADD [Amount] int NOT NULL DEFAULT 0;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220428142252_ConfigureShoppingCartItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220428142252_ConfigureShoppingCartItem', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220510110120_ModifyColumnNameShoppingCartItem')
BEGIN
    EXEC sp_rename N'[ShoppingItems].[Amount]', N'Cantity', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220510110120_ModifyColumnNameShoppingCartItem')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220510110120_ModifyColumnNameShoppingCartItem', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220511084532_UpdateProductsTable')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220511084532_UpdateProductsTable', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514103349_AddingConfirmationCodeBuyers2')
BEGIN
    ALTER TABLE [Buyers] ADD [ConfirmCode] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220514103349_AddingConfirmationCodeBuyers2')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220514103349_AddingConfirmationCodeBuyers2', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    EXEC sp_rename N'[OrderItems].[Amount]', N'Cantity', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShoppingCarts]') AND [c].[name] = N'TotalPrice');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [ShoppingCarts] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [ShoppingCarts] ALTER COLUMN [TotalPrice] float NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Products]') AND [c].[name] = N'Price');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Products] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Products] ALTER COLUMN [Price] float NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Orders]') AND [c].[name] = N'TotalPrice');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Orders] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Orders] ALTER COLUMN [TotalPrice] float NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderItems]') AND [c].[name] = N'Price');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [OrderItems] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [OrderItems] ALTER COLUMN [Price] float NOT NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Buyers]') AND [c].[name] = N'Balance');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Buyers] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Buyers] ALTER COLUMN [Balance] float NOT NULL;
    ALTER TABLE [Buyers] ADD DEFAULT 0.0E0 FOR [Balance];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220519081419_UpdateOrderItemsColumnName')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220519081419_UpdateOrderItemsColumnName', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524095245_AddNewColumnImageInOrderItems')
BEGIN
    ALTER TABLE [OrderItems] DROP CONSTRAINT [FK_OrderItems_Orders_OrderId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524095245_AddNewColumnImageInOrderItems')
BEGIN
    DROP INDEX [IX_OrderItems_OrderId] ON [OrderItems];
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[OrderItems]') AND [c].[name] = N'OrderId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [OrderItems] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [OrderItems] ALTER COLUMN [OrderId] int NOT NULL;
    ALTER TABLE [OrderItems] ADD DEFAULT 0 FOR [OrderId];
    CREATE INDEX [IX_OrderItems_OrderId] ON [OrderItems] ([OrderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524095245_AddNewColumnImageInOrderItems')
BEGIN
    ALTER TABLE [OrderItems] ADD [Image] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524095245_AddNewColumnImageInOrderItems')
BEGIN
    ALTER TABLE [OrderItems] ADD CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Orders] ([Id]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524095245_AddNewColumnImageInOrderItems')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220524095245_AddNewColumnImageInOrderItems', N'6.0.4');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531143025_ADdPrecisionBalanceBuyers')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Buyers]') AND [c].[name] = N'Balance');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Buyers] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Buyers] ALTER COLUMN [Balance] float(18) NOT NULL;
    ALTER TABLE [Buyers] ADD DEFAULT 0.0E0 FOR [Balance];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220531143025_ADdPrecisionBalanceBuyers')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220531143025_ADdPrecisionBalanceBuyers', N'6.0.4');
END;
GO

COMMIT;
GO

