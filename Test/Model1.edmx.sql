
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/26/2017 15:16:10
-- Generated from EDMX file: E:\Test\Test\Test\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [HealthCareFinal];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_FileConsultation]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ConsultationSets] DROP CONSTRAINT [FK_FileConsultation];
GO
IF OBJECT_ID(N'[dbo].[FK_RDVPatient]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[RDVSets] DROP CONSTRAINT [FK_RDVPatient];
GO
IF OBJECT_ID(N'[dbo].[FK_CitySetPatientSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PatientSets] DROP CONSTRAINT [FK_CitySetPatientSet];
GO
IF OBJECT_ID(N'[dbo].[FK_FileSetPatientSet]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FileSets] DROP CONSTRAINT [FK_FileSetPatientSet];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[CitySets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CitySets];
GO
IF OBJECT_ID(N'[dbo].[ConsultationSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ConsultationSets];
GO
IF OBJECT_ID(N'[dbo].[FileSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FileSets];
GO
IF OBJECT_ID(N'[dbo].[RDVSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[RDVSets];
GO
IF OBJECT_ID(N'[dbo].[UserSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserSets];
GO
IF OBJECT_ID(N'[dbo].[PatientSets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PatientSets];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'CitySets'
CREATE TABLE [dbo].[CitySets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [name] nvarchar(max)  NOT NULL,
    [postalCode] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'ConsultationSets'
CREATE TABLE [dbo].[ConsultationSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [date] datetime  NOT NULL,
    [actNature] nvarchar(max)  NOT NULL,
    [cost] float  NOT NULL,
    [FileId] int  NOT NULL
);
GO

-- Creating table 'FileSets'
CREATE TABLE [dbo].[FileSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CreationDate] datetime  NOT NULL,
    [PatientSet_Id] int  NOT NULL
);
GO

-- Creating table 'RDVSets'
CREATE TABLE [dbo].[RDVSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [date] datetime  NOT NULL,
    [state] bit  NOT NULL,
    [Patient_Id] int  NOT NULL
);
GO

-- Creating table 'UserSets'
CREATE TABLE [dbo].[UserSets] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [login] nvarchar(max)  NOT NULL,
    [password] nvarchar(max)  NOT NULL,
    [profile] int  NOT NULL,
    [IsSuperUser] bit  NOT NULL,
    [SUperMdp] nvarchar(max)  NULL
);
GO

-- Creating table 'PatientSets'
CREATE TABLE [dbo].[PatientSets] (
    [LastName] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [BirthDate] datetime  NOT NULL,
    [Occupation] nvarchar(max)  NULL,
    [LastVisit] datetime  NULL,
    [CellPhone] nvarchar(max)  NULL,
    [Address] nvarchar(max)  NULL,
    [Id] int IDENTITY(1,1) NOT NULL,
    [CitySetId] int  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'CitySets'
ALTER TABLE [dbo].[CitySets]
ADD CONSTRAINT [PK_CitySets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConsultationSets'
ALTER TABLE [dbo].[ConsultationSets]
ADD CONSTRAINT [PK_ConsultationSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileSets'
ALTER TABLE [dbo].[FileSets]
ADD CONSTRAINT [PK_FileSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'RDVSets'
ALTER TABLE [dbo].[RDVSets]
ADD CONSTRAINT [PK_RDVSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserSets'
ALTER TABLE [dbo].[UserSets]
ADD CONSTRAINT [PK_UserSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PatientSets'
ALTER TABLE [dbo].[PatientSets]
ADD CONSTRAINT [PK_PatientSets]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [FileId] in table 'ConsultationSets'
ALTER TABLE [dbo].[ConsultationSets]
ADD CONSTRAINT [FK_FileConsultation]
    FOREIGN KEY ([FileId])
    REFERENCES [dbo].[FileSets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileConsultation'
CREATE INDEX [IX_FK_FileConsultation]
ON [dbo].[ConsultationSets]
    ([FileId]);
GO

-- Creating foreign key on [Patient_Id] in table 'RDVSets'
ALTER TABLE [dbo].[RDVSets]
ADD CONSTRAINT [FK_RDVPatient]
    FOREIGN KEY ([Patient_Id])
    REFERENCES [dbo].[PatientSets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_RDVPatient'
CREATE INDEX [IX_FK_RDVPatient]
ON [dbo].[RDVSets]
    ([Patient_Id]);
GO

-- Creating foreign key on [CitySetId] in table 'PatientSets'
ALTER TABLE [dbo].[PatientSets]
ADD CONSTRAINT [FK_CitySetPatientSet]
    FOREIGN KEY ([CitySetId])
    REFERENCES [dbo].[CitySets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CitySetPatientSet'
CREATE INDEX [IX_FK_CitySetPatientSet]
ON [dbo].[PatientSets]
    ([CitySetId]);
GO

-- Creating foreign key on [PatientSet_Id] in table 'FileSets'
ALTER TABLE [dbo].[FileSets]
ADD CONSTRAINT [FK_FileSetPatientSet]
    FOREIGN KEY ([PatientSet_Id])
    REFERENCES [dbo].[PatientSets]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileSetPatientSet'
CREATE INDEX [IX_FK_FileSetPatientSet]
ON [dbo].[FileSets]
    ([PatientSet_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------