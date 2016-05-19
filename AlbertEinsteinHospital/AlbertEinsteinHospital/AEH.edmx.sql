
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/19/2016 19:34:30
-- Generated from EDMX file: D:\Joao\Escola\IS\2º Ano\2º Semestre\ESGPGS\Projeto\projetoESGPS\AlbertEinsteinHospital\AlbertEinsteinHospital\AEH.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AEH_BD];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Utilizador_inherits_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PessoaSet_Utilizador] DROP CONSTRAINT [FK_Utilizador_inherits_Pessoa];
GO
IF OBJECT_ID(N'[dbo].[FK_Paciente_inherits_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PessoaSet_Paciente] DROP CONSTRAINT [FK_Paciente_inherits_Pessoa];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PessoaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet];
GO
IF OBJECT_ID(N'[dbo].[PessoaSet_Utilizador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet_Utilizador];
GO
IF OBJECT_ID(N'[dbo].[PessoaSet_Paciente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet_Paciente];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'PessoaSet'
CREATE TABLE [dbo].[PessoaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [DataNascimento] datetime  NOT NULL,
    [NumSns] int  NOT NULL,
    [Genero] nvarchar(max)  NOT NULL,
    [Morada] nvarchar(max)  NOT NULL,
    [Telefone] int  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'PessoaSet_Utilizador'
CREATE TABLE [dbo].[PessoaSet_Utilizador] (
    [NomeUtilizador] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [TipoUtilizador] nvarchar(max)  NOT NULL,
    [Ativo] bit  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'PessoaSet_Paciente'
CREATE TABLE [dbo].[PessoaSet_Paciente] (
    [Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PessoaSet'
ALTER TABLE [dbo].[PessoaSet]
ADD CONSTRAINT [PK_PessoaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PessoaSet_Utilizador'
ALTER TABLE [dbo].[PessoaSet_Utilizador]
ADD CONSTRAINT [PK_PessoaSet_Utilizador]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PessoaSet_Paciente'
ALTER TABLE [dbo].[PessoaSet_Paciente]
ADD CONSTRAINT [PK_PessoaSet_Paciente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id] in table 'PessoaSet_Utilizador'
ALTER TABLE [dbo].[PessoaSet_Utilizador]
ADD CONSTRAINT [FK_Utilizador_inherits_Pessoa]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PessoaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PessoaSet_Paciente'
ALTER TABLE [dbo].[PessoaSet_Paciente]
ADD CONSTRAINT [FK_Paciente_inherits_Pessoa]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PessoaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------