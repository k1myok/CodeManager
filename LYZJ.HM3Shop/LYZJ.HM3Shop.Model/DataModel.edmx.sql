
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 12/06/2012 16:22:25
-- Generated from EDMX file: F:\heima\practice\LYZJ.HM3Shop\LYZJ.HM3Shop.Model\DataModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HM3Data];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_Role] DROP CONSTRAINT [FK_UserInfoR_UserInfo_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_RoleR_UserInfo_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_Role] DROP CONSTRAINT [FK_RoleR_UserInfo_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoRole_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoRole] DROP CONSTRAINT [FK_ActionInfoRole_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoRole_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoRole] DROP CONSTRAINT [FK_ActionInfoRole_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoR_UserInfo_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[R_UserInfo_ActionInfo] DROP CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoActionGroup_ActionInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoActionGroup] DROP CONSTRAINT [FK_ActionInfoActionGroup_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionInfoActionGroup_ActionGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionInfoActionGroup] DROP CONSTRAINT [FK_ActionInfoActionGroup_ActionGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoActionGroup_UserInfo]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoActionGroup] DROP CONSTRAINT [FK_UserInfoActionGroup_UserInfo];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfoActionGroup_ActionGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserInfoActionGroup] DROP CONSTRAINT [FK_UserInfoActionGroup_ActionGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionGroupRole_ActionGroup]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionGroupRole] DROP CONSTRAINT [FK_ActionGroupRole_ActionGroup];
GO
IF OBJECT_ID(N'[dbo].[FK_ActionGroupRole_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ActionGroupRole] DROP CONSTRAINT [FK_ActionGroupRole_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_UserInfo实体1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[实体1集] DROP CONSTRAINT [FK_UserInfo实体1];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[UserInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfo];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_Role];
GO
IF OBJECT_ID(N'[dbo].[ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[R_UserInfo_ActionInfo]', 'U') IS NOT NULL
    DROP TABLE [dbo].[R_UserInfo_ActionInfo];
GO
IF OBJECT_ID(N'[dbo].[ActionGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionGroup];
GO
IF OBJECT_ID(N'[dbo].[实体1集]', 'U') IS NOT NULL
    DROP TABLE [dbo].[实体1集];
GO
IF OBJECT_ID(N'[dbo].[ActionInfoRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfoRole];
GO
IF OBJECT_ID(N'[dbo].[ActionInfoActionGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionInfoActionGroup];
GO
IF OBJECT_ID(N'[dbo].[UserInfoActionGroup]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserInfoActionGroup];
GO
IF OBJECT_ID(N'[dbo].[ActionGroupRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ActionGroupRole];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserInfo'
CREATE TABLE [dbo].[UserInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UName] nvarchar(32)  NOT NULL,
    [Pwd] nvarchar(32)  NOT NULL,
    [Phone] varchar(16)  NULL,
    [Mail] nvarchar(32)  NULL,
    [SubTime] datetime  NOT NULL,
    [LastModifiedOn] datetime  NOT NULL,
    [DelFlag] smallint  NOT NULL
);
GO

-- Creating table 'Role'
CREATE TABLE [dbo].[Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(32)  NOT NULL,
    [RoleType] smallint  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'R_UserInfo_Role'
CREATE TABLE [dbo].[R_UserInfo_Role] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [RoleID] int  NOT NULL,
    [SubTime] datetime  NOT NULL
);
GO

-- Creating table 'ActionInfo'
CREATE TABLE [dbo].[ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [RequestUrl] varchar(256)  NOT NULL,
    [RequestHttpType] varchar(16)  NOT NULL,
    [ActionName] nvarchar(16)  NOT NULL,
    [SubTime] datetime  NOT NULL,
    [ActionType] smallint  NOT NULL
);
GO

-- Creating table 'R_UserInfo_ActionInfo'
CREATE TABLE [dbo].[R_UserInfo_ActionInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL,
    [ActionInfoID] int  NOT NULL,
    [HasPermation] bit  NOT NULL
);
GO

-- Creating table 'ActionGroup'
CREATE TABLE [dbo].[ActionGroup] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GroupName] nvarchar(32)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [GroupType] smallint  NOT NULL
);
GO

-- Creating table '实体1集'
CREATE TABLE [dbo].[实体1集] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [UserInfoID] int  NOT NULL
);
GO

-- Creating table 'Category'
CREATE TABLE [dbo].[Category] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CatName] nvarchar(max)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [ParentID] int  NOT NULL,
    [TreePath] nvarchar(64)  NOT NULL,
    [Level] int  NOT NULL,
    [IsLeaf] smallint  NOT NULL
);
GO

-- Creating table 'GoodInfo'
CREATE TABLE [dbo].[GoodInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [GoodName] nvarchar(max)  NOT NULL,
    [GoodNo] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(max)  NOT NULL,
    [GoodStatus] nvarchar(max)  NOT NULL,
    [Subtime] datetime  NOT NULL,
    [OnShelfTime] datetime  NOT NULL,
    [OffLineTime] datetime  NOT NULL,
    [GoodMark] smallint  NOT NULL,
    [MainInageId] int  NOT NULL
);
GO

-- Creating table 'Property'
CREATE TABLE [dbo].[Property] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PropName] nvarchar(max)  NOT NULL,
    [ShowType] smallint  NOT NULL,
    [PropOptions] nvarchar(256)  NOT NULL
);
GO

-- Creating table 'PropOption'
CREATE TABLE [dbo].[PropOption] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [OptionName] nvarchar(max)  NOT NULL,
    [ShowName] nvarchar(32)  NOT NULL,
    [PropertyID] int  NOT NULL
);
GO

-- Creating table 'GoodsPropValue'
CREATE TABLE [dbo].[GoodsPropValue] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [PropID] int  NOT NULL,
    [OptionID] int  NOT NULL,
    [GoodInfoID] int  NOT NULL,
    [GoodInfo_ID] int  NOT NULL
);
GO

-- Creating table 'GoodSKU'
CREATE TABLE [dbo].[GoodSKU] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [SkuCode] nvarchar(64)  NOT NULL,
    [Remark] nvarchar(max)  NOT NULL,
    [SKUOptions] nvarchar(128)  NOT NULL,
    [GoodInfoID] int  NOT NULL,
    [StoreCount] decimal(18,0)  NOT NULL,
    [GoodInfo_ID] int  NOT NULL
);
GO

-- Creating table 'ImageInfo'
CREATE TABLE [dbo].[ImageInfo] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [URL] varchar(256)  NOT NULL,
    [Alt] nvarchar(32)  NOT NULL,
    [ImageSize] nvarchar(max)  NOT NULL,
    [DelFlag] smallint  NOT NULL,
    [GoodInfoID] int  NOT NULL
);
GO

-- Creating table 'Shop'
CREATE TABLE [dbo].[Shop] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ShopName] nvarchar(max)  NOT NULL,
    [Property1] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ActionInfoRole'
CREATE TABLE [dbo].[ActionInfoRole] (
    [ActionInfo_ID] int  NOT NULL,
    [Role_ID] int  NOT NULL
);
GO

-- Creating table 'ActionInfoActionGroup'
CREATE TABLE [dbo].[ActionInfoActionGroup] (
    [ActionInfo_ID] int  NOT NULL,
    [ActionGroup_ID] int  NOT NULL
);
GO

-- Creating table 'UserInfoActionGroup'
CREATE TABLE [dbo].[UserInfoActionGroup] (
    [UserInfo_ID] int  NOT NULL,
    [ActionGroup_ID] int  NOT NULL
);
GO

-- Creating table 'ActionGroupRole'
CREATE TABLE [dbo].[ActionGroupRole] (
    [ActionGroup_ID] int  NOT NULL,
    [Role_ID] int  NOT NULL
);
GO

-- Creating table 'CategoryProperty'
CREATE TABLE [dbo].[CategoryProperty] (
    [Category_ID] int  NOT NULL,
    [Property_ID] int  NOT NULL
);
GO

-- Creating table 'PropertyGoodInfo'
CREATE TABLE [dbo].[PropertyGoodInfo] (
    [Property_ID] int  NOT NULL,
    [GoodInfo_ID] int  NOT NULL
);
GO

-- Creating table 'GoodSKUGoodsPropValue'
CREATE TABLE [dbo].[GoodSKUGoodsPropValue] (
    [GoodSKU_ID] int  NOT NULL,
    [GoodsPropValue_ID] int  NOT NULL
);
GO

-- Creating table 'CategoryGoodInfo'
CREATE TABLE [dbo].[CategoryGoodInfo] (
    [Category_ID] int  NOT NULL,
    [GoodInfo_ID] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'UserInfo'
ALTER TABLE [dbo].[UserInfo]
ADD CONSTRAINT [PK_UserInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Role'
ALTER TABLE [dbo].[Role]
ADD CONSTRAINT [PK_Role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'R_UserInfo_Role'
ALTER TABLE [dbo].[R_UserInfo_Role]
ADD CONSTRAINT [PK_R_UserInfo_Role]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionInfo'
ALTER TABLE [dbo].[ActionInfo]
ADD CONSTRAINT [PK_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [PK_R_UserInfo_ActionInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ActionGroup'
ALTER TABLE [dbo].[ActionGroup]
ADD CONSTRAINT [PK_ActionGroup]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table '实体1集'
ALTER TABLE [dbo].[实体1集]
ADD CONSTRAINT [PK_实体1集]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Category'
ALTER TABLE [dbo].[Category]
ADD CONSTRAINT [PK_Category]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodInfo'
ALTER TABLE [dbo].[GoodInfo]
ADD CONSTRAINT [PK_GoodInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Property'
ALTER TABLE [dbo].[Property]
ADD CONSTRAINT [PK_Property]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'PropOption'
ALTER TABLE [dbo].[PropOption]
ADD CONSTRAINT [PK_PropOption]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodsPropValue'
ALTER TABLE [dbo].[GoodsPropValue]
ADD CONSTRAINT [PK_GoodsPropValue]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'GoodSKU'
ALTER TABLE [dbo].[GoodSKU]
ADD CONSTRAINT [PK_GoodSKU]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'ImageInfo'
ALTER TABLE [dbo].[ImageInfo]
ADD CONSTRAINT [PK_ImageInfo]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Shop'
ALTER TABLE [dbo].[Shop]
ADD CONSTRAINT [PK_Shop]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ActionInfo_ID], [Role_ID] in table 'ActionInfoRole'
ALTER TABLE [dbo].[ActionInfoRole]
ADD CONSTRAINT [PK_ActionInfoRole]
    PRIMARY KEY NONCLUSTERED ([ActionInfo_ID], [Role_ID] ASC);
GO

-- Creating primary key on [ActionInfo_ID], [ActionGroup_ID] in table 'ActionInfoActionGroup'
ALTER TABLE [dbo].[ActionInfoActionGroup]
ADD CONSTRAINT [PK_ActionInfoActionGroup]
    PRIMARY KEY NONCLUSTERED ([ActionInfo_ID], [ActionGroup_ID] ASC);
GO

-- Creating primary key on [UserInfo_ID], [ActionGroup_ID] in table 'UserInfoActionGroup'
ALTER TABLE [dbo].[UserInfoActionGroup]
ADD CONSTRAINT [PK_UserInfoActionGroup]
    PRIMARY KEY NONCLUSTERED ([UserInfo_ID], [ActionGroup_ID] ASC);
GO

-- Creating primary key on [ActionGroup_ID], [Role_ID] in table 'ActionGroupRole'
ALTER TABLE [dbo].[ActionGroupRole]
ADD CONSTRAINT [PK_ActionGroupRole]
    PRIMARY KEY NONCLUSTERED ([ActionGroup_ID], [Role_ID] ASC);
GO

-- Creating primary key on [Category_ID], [Property_ID] in table 'CategoryProperty'
ALTER TABLE [dbo].[CategoryProperty]
ADD CONSTRAINT [PK_CategoryProperty]
    PRIMARY KEY NONCLUSTERED ([Category_ID], [Property_ID] ASC);
GO

-- Creating primary key on [Property_ID], [GoodInfo_ID] in table 'PropertyGoodInfo'
ALTER TABLE [dbo].[PropertyGoodInfo]
ADD CONSTRAINT [PK_PropertyGoodInfo]
    PRIMARY KEY NONCLUSTERED ([Property_ID], [GoodInfo_ID] ASC);
GO

-- Creating primary key on [GoodSKU_ID], [GoodsPropValue_ID] in table 'GoodSKUGoodsPropValue'
ALTER TABLE [dbo].[GoodSKUGoodsPropValue]
ADD CONSTRAINT [PK_GoodSKUGoodsPropValue]
    PRIMARY KEY NONCLUSTERED ([GoodSKU_ID], [GoodsPropValue_ID] ASC);
GO

-- Creating primary key on [Category_ID], [GoodInfo_ID] in table 'CategoryGoodInfo'
ALTER TABLE [dbo].[CategoryGoodInfo]
ADD CONSTRAINT [PK_CategoryGoodInfo]
    PRIMARY KEY NONCLUSTERED ([Category_ID], [GoodInfo_ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserInfoID] in table 'R_UserInfo_Role'
ALTER TABLE [dbo].[R_UserInfo_Role]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_Role]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_Role'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_Role]
ON [dbo].[R_UserInfo_Role]
    ([UserInfoID]);
GO

-- Creating foreign key on [RoleID] in table 'R_UserInfo_Role'
ALTER TABLE [dbo].[R_UserInfo_Role]
ADD CONSTRAINT [FK_RoleR_UserInfo_Role]
    FOREIGN KEY ([RoleID])
    REFERENCES [dbo].[Role]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_RoleR_UserInfo_Role'
CREATE INDEX [IX_FK_RoleR_UserInfo_Role]
ON [dbo].[R_UserInfo_Role]
    ([RoleID]);
GO

-- Creating foreign key on [ActionInfo_ID] in table 'ActionInfoRole'
ALTER TABLE [dbo].[ActionInfoRole]
ADD CONSTRAINT [FK_ActionInfoRole_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Role_ID] in table 'ActionInfoRole'
ALTER TABLE [dbo].[ActionInfoRole]
ADD CONSTRAINT [FK_ActionInfoRole_Role]
    FOREIGN KEY ([Role_ID])
    REFERENCES [dbo].[Role]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoRole_Role'
CREATE INDEX [IX_FK_ActionInfoRole_Role]
ON [dbo].[ActionInfoRole]
    ([Role_ID]);
GO

-- Creating foreign key on [UserInfoID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_UserInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_UserInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([UserInfoID]);
GO

-- Creating foreign key on [ActionInfoID] in table 'R_UserInfo_ActionInfo'
ALTER TABLE [dbo].[R_UserInfo_ActionInfo]
ADD CONSTRAINT [FK_ActionInfoR_UserInfo_ActionInfo]
    FOREIGN KEY ([ActionInfoID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoR_UserInfo_ActionInfo'
CREATE INDEX [IX_FK_ActionInfoR_UserInfo_ActionInfo]
ON [dbo].[R_UserInfo_ActionInfo]
    ([ActionInfoID]);
GO

-- Creating foreign key on [ActionInfo_ID] in table 'ActionInfoActionGroup'
ALTER TABLE [dbo].[ActionInfoActionGroup]
ADD CONSTRAINT [FK_ActionInfoActionGroup_ActionInfo]
    FOREIGN KEY ([ActionInfo_ID])
    REFERENCES [dbo].[ActionInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionGroup_ID] in table 'ActionInfoActionGroup'
ALTER TABLE [dbo].[ActionInfoActionGroup]
ADD CONSTRAINT [FK_ActionInfoActionGroup_ActionGroup]
    FOREIGN KEY ([ActionGroup_ID])
    REFERENCES [dbo].[ActionGroup]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionInfoActionGroup_ActionGroup'
CREATE INDEX [IX_FK_ActionInfoActionGroup_ActionGroup]
ON [dbo].[ActionInfoActionGroup]
    ([ActionGroup_ID]);
GO

-- Creating foreign key on [UserInfo_ID] in table 'UserInfoActionGroup'
ALTER TABLE [dbo].[UserInfoActionGroup]
ADD CONSTRAINT [FK_UserInfoActionGroup_UserInfo]
    FOREIGN KEY ([UserInfo_ID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ActionGroup_ID] in table 'UserInfoActionGroup'
ALTER TABLE [dbo].[UserInfoActionGroup]
ADD CONSTRAINT [FK_UserInfoActionGroup_ActionGroup]
    FOREIGN KEY ([ActionGroup_ID])
    REFERENCES [dbo].[ActionGroup]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfoActionGroup_ActionGroup'
CREATE INDEX [IX_FK_UserInfoActionGroup_ActionGroup]
ON [dbo].[UserInfoActionGroup]
    ([ActionGroup_ID]);
GO

-- Creating foreign key on [ActionGroup_ID] in table 'ActionGroupRole'
ALTER TABLE [dbo].[ActionGroupRole]
ADD CONSTRAINT [FK_ActionGroupRole_ActionGroup]
    FOREIGN KEY ([ActionGroup_ID])
    REFERENCES [dbo].[ActionGroup]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Role_ID] in table 'ActionGroupRole'
ALTER TABLE [dbo].[ActionGroupRole]
ADD CONSTRAINT [FK_ActionGroupRole_Role]
    FOREIGN KEY ([Role_ID])
    REFERENCES [dbo].[Role]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_ActionGroupRole_Role'
CREATE INDEX [IX_FK_ActionGroupRole_Role]
ON [dbo].[ActionGroupRole]
    ([Role_ID]);
GO

-- Creating foreign key on [UserInfoID] in table '实体1集'
ALTER TABLE [dbo].[实体1集]
ADD CONSTRAINT [FK_UserInfo实体1]
    FOREIGN KEY ([UserInfoID])
    REFERENCES [dbo].[UserInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_UserInfo实体1'
CREATE INDEX [IX_FK_UserInfo实体1]
ON [dbo].[实体1集]
    ([UserInfoID]);
GO

-- Creating foreign key on [PropertyID] in table 'PropOption'
ALTER TABLE [dbo].[PropOption]
ADD CONSTRAINT [FK_PropertyPropOption]
    FOREIGN KEY ([PropertyID])
    REFERENCES [dbo].[Property]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PropertyPropOption'
CREATE INDEX [IX_FK_PropertyPropOption]
ON [dbo].[PropOption]
    ([PropertyID]);
GO

-- Creating foreign key on [Category_ID] in table 'CategoryProperty'
ALTER TABLE [dbo].[CategoryProperty]
ADD CONSTRAINT [FK_CategoryProperty_Category]
    FOREIGN KEY ([Category_ID])
    REFERENCES [dbo].[Category]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Property_ID] in table 'CategoryProperty'
ALTER TABLE [dbo].[CategoryProperty]
ADD CONSTRAINT [FK_CategoryProperty_Property]
    FOREIGN KEY ([Property_ID])
    REFERENCES [dbo].[Property]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryProperty_Property'
CREATE INDEX [IX_FK_CategoryProperty_Property]
ON [dbo].[CategoryProperty]
    ([Property_ID]);
GO

-- Creating foreign key on [Property_ID] in table 'PropertyGoodInfo'
ALTER TABLE [dbo].[PropertyGoodInfo]
ADD CONSTRAINT [FK_PropertyGoodInfo_Property]
    FOREIGN KEY ([Property_ID])
    REFERENCES [dbo].[Property]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GoodInfo_ID] in table 'PropertyGoodInfo'
ALTER TABLE [dbo].[PropertyGoodInfo]
ADD CONSTRAINT [FK_PropertyGoodInfo_GoodInfo]
    FOREIGN KEY ([GoodInfo_ID])
    REFERENCES [dbo].[GoodInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PropertyGoodInfo_GoodInfo'
CREATE INDEX [IX_FK_PropertyGoodInfo_GoodInfo]
ON [dbo].[PropertyGoodInfo]
    ([GoodInfo_ID]);
GO

-- Creating foreign key on [GoodInfo_ID] in table 'GoodSKU'
ALTER TABLE [dbo].[GoodSKU]
ADD CONSTRAINT [FK_GoodSKUGoodInfo]
    FOREIGN KEY ([GoodInfo_ID])
    REFERENCES [dbo].[GoodInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodSKUGoodInfo'
CREATE INDEX [IX_FK_GoodSKUGoodInfo]
ON [dbo].[GoodSKU]
    ([GoodInfo_ID]);
GO

-- Creating foreign key on [GoodSKU_ID] in table 'GoodSKUGoodsPropValue'
ALTER TABLE [dbo].[GoodSKUGoodsPropValue]
ADD CONSTRAINT [FK_GoodSKUGoodsPropValue_GoodSKU]
    FOREIGN KEY ([GoodSKU_ID])
    REFERENCES [dbo].[GoodSKU]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GoodsPropValue_ID] in table 'GoodSKUGoodsPropValue'
ALTER TABLE [dbo].[GoodSKUGoodsPropValue]
ADD CONSTRAINT [FK_GoodSKUGoodsPropValue_GoodsPropValue]
    FOREIGN KEY ([GoodsPropValue_ID])
    REFERENCES [dbo].[GoodsPropValue]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodSKUGoodsPropValue_GoodsPropValue'
CREATE INDEX [IX_FK_GoodSKUGoodsPropValue_GoodsPropValue]
ON [dbo].[GoodSKUGoodsPropValue]
    ([GoodsPropValue_ID]);
GO

-- Creating foreign key on [GoodInfo_ID] in table 'GoodsPropValue'
ALTER TABLE [dbo].[GoodsPropValue]
ADD CONSTRAINT [FK_GoodsPropValueGoodInfo]
    FOREIGN KEY ([GoodInfo_ID])
    REFERENCES [dbo].[GoodInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodsPropValueGoodInfo'
CREATE INDEX [IX_FK_GoodsPropValueGoodInfo]
ON [dbo].[GoodsPropValue]
    ([GoodInfo_ID]);
GO

-- Creating foreign key on [Category_ID] in table 'CategoryGoodInfo'
ALTER TABLE [dbo].[CategoryGoodInfo]
ADD CONSTRAINT [FK_CategoryGoodInfo_Category]
    FOREIGN KEY ([Category_ID])
    REFERENCES [dbo].[Category]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [GoodInfo_ID] in table 'CategoryGoodInfo'
ALTER TABLE [dbo].[CategoryGoodInfo]
ADD CONSTRAINT [FK_CategoryGoodInfo_GoodInfo]
    FOREIGN KEY ([GoodInfo_ID])
    REFERENCES [dbo].[GoodInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_CategoryGoodInfo_GoodInfo'
CREATE INDEX [IX_FK_CategoryGoodInfo_GoodInfo]
ON [dbo].[CategoryGoodInfo]
    ([GoodInfo_ID]);
GO

-- Creating foreign key on [GoodInfoID] in table 'ImageInfo'
ALTER TABLE [dbo].[ImageInfo]
ADD CONSTRAINT [FK_GoodInfoImageInfo]
    FOREIGN KEY ([GoodInfoID])
    REFERENCES [dbo].[GoodInfo]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_GoodInfoImageInfo'
CREATE INDEX [IX_FK_GoodInfoImageInfo]
ON [dbo].[ImageInfo]
    ([GoodInfoID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------