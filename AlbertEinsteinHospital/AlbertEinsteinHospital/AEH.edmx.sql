
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/02/2016 17:35:29
-- Generated from EDMX file: D:\Joao\Desktop\Andre\ProjetoESGPSAlbertEinstein\AlbertEinsteinHospital\AlbertEinsteinHospital\AEH.edmx
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

IF OBJECT_ID(N'[dbo].[FK_PacienteExame]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ExameSet] DROP CONSTRAINT [FK_PacienteExame];
GO
IF OBJECT_ID(N'[dbo].[FK_PacienteMedicacao]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MedicacaoSet] DROP CONSTRAINT [FK_PacienteMedicacao];
GO
IF OBJECT_ID(N'[dbo].[FK_PacienteSintoma]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SintomaSet] DROP CONSTRAINT [FK_PacienteSintoma];
GO
IF OBJECT_ID(N'[dbo].[FK_Paciente_inherits_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PessoaSet_Paciente] DROP CONSTRAINT [FK_Paciente_inherits_Pessoa];
GO
IF OBJECT_ID(N'[dbo].[FK_Utilizador_inherits_Pessoa]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PessoaSet_Utilizador] DROP CONSTRAINT [FK_Utilizador_inherits_Pessoa];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[PessoaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet];
GO
IF OBJECT_ID(N'[dbo].[MedicacaoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MedicacaoSet];
GO
IF OBJECT_ID(N'[dbo].[ExameSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ExameSet];
GO
IF OBJECT_ID(N'[dbo].[SintomaSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SintomaSet];
GO
IF OBJECT_ID(N'[dbo].[PessoaSet_Paciente]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet_Paciente];
GO
IF OBJECT_ID(N'[dbo].[PessoaSet_Utilizador]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PessoaSet_Utilizador];
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

-- Creating table 'MedicacaoSet'
CREATE TABLE [dbo].[MedicacaoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [DataInicio] datetime  NOT NULL,
    [DataFim] datetime  NOT NULL,
    [Medicamento] nvarchar(max)  NOT NULL,
    [Dosagem] nvarchar(max)  NOT NULL,
    [Paciente_Id] int  NULL
);
GO

-- Creating table 'ExameSet'
CREATE TABLE [dbo].[ExameSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Imagem] varbinary(max)  NOT NULL,
    [Notas] nvarchar(max)  NOT NULL,
    [Paciente_Id] int  NULL
);
GO

-- Creating table 'SintomaSet'
CREATE TABLE [dbo].[SintomaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CompromissoViaAerea] bit  NOT NULL,
    [RespiracaoIneficaz] bit  NOT NULL,
    [CriancaNaoReativa] bit  NOT NULL,
    [Choque] bit  NOT NULL,
    [IncapacidadeArticular] bit  NOT NULL,
    [TaquicardiaAcentuada] bit  NOT NULL,
    [PEFRmb] bit  NOT NULL,
    [SAO2mb] bit  NOT NULL,
    [AlteracaoConsciencia] bit  NOT NULL,
    [PEFRb] bit  NOT NULL,
    [SAO2b] bit  NOT NULL,
    [HistoriaAsma] bit  NOT NULL,
    [Asma] bit  NOT NULL,
    [Broncospasmo] bit  NOT NULL,
    [ProvavelInfecaoRespiratoria] bit  NOT NULL,
    [ProblemaRecente] bit  NOT NULL,
    [Outro] bit  NOT NULL,
    [OutroDescricao] nvarchar(max)  NULL,
    [Notas] nvarchar(max)  NULL,
    [Paciente_Id] int  NULL,
    [Consulta_Id] int  NULL
);
GO

-- Creating table 'ConsultaSet'
CREATE TABLE [dbo].[ConsultaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Diagnostico] nvarchar(max)  NOT NULL,
    [Paciente_Id] int  NULL
);
GO

-- Creating table 'PessoaSet_Paciente'
CREATE TABLE [dbo].[PessoaSet_Paciente] (
    [Id] int  NOT NULL
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

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'PessoaSet'
ALTER TABLE [dbo].[PessoaSet]
ADD CONSTRAINT [PK_PessoaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'MedicacaoSet'
ALTER TABLE [dbo].[MedicacaoSet]
ADD CONSTRAINT [PK_MedicacaoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ExameSet'
ALTER TABLE [dbo].[ExameSet]
ADD CONSTRAINT [PK_ExameSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'SintomaSet'
ALTER TABLE [dbo].[SintomaSet]
ADD CONSTRAINT [PK_SintomaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ConsultaSet'
ALTER TABLE [dbo].[ConsultaSet]
ADD CONSTRAINT [PK_ConsultaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PessoaSet_Paciente'
ALTER TABLE [dbo].[PessoaSet_Paciente]
ADD CONSTRAINT [PK_PessoaSet_Paciente]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PessoaSet_Utilizador'
ALTER TABLE [dbo].[PessoaSet_Utilizador]
ADD CONSTRAINT [PK_PessoaSet_Utilizador]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Paciente_Id] in table 'ExameSet'
ALTER TABLE [dbo].[ExameSet]
ADD CONSTRAINT [FK_PacienteExame]
    FOREIGN KEY ([Paciente_Id])
    REFERENCES [dbo].[PessoaSet_Paciente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PacienteExame'
CREATE INDEX [IX_FK_PacienteExame]
ON [dbo].[ExameSet]
    ([Paciente_Id]);
GO

-- Creating foreign key on [Paciente_Id] in table 'MedicacaoSet'
ALTER TABLE [dbo].[MedicacaoSet]
ADD CONSTRAINT [FK_PacienteMedicacao]
    FOREIGN KEY ([Paciente_Id])
    REFERENCES [dbo].[PessoaSet_Paciente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PacienteMedicacao'
CREATE INDEX [IX_FK_PacienteMedicacao]
ON [dbo].[MedicacaoSet]
    ([Paciente_Id]);
GO

-- Creating foreign key on [Paciente_Id] in table 'SintomaSet'
ALTER TABLE [dbo].[SintomaSet]
ADD CONSTRAINT [FK_PacienteSintoma]
    FOREIGN KEY ([Paciente_Id])
    REFERENCES [dbo].[PessoaSet_Paciente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PacienteSintoma'
CREATE INDEX [IX_FK_PacienteSintoma]
ON [dbo].[SintomaSet]
    ([Paciente_Id]);
GO

-- Creating foreign key on [Paciente_Id] in table 'ConsultaSet'
ALTER TABLE [dbo].[ConsultaSet]
ADD CONSTRAINT [FK_PacienteConsulta]
    FOREIGN KEY ([Paciente_Id])
    REFERENCES [dbo].[PessoaSet_Paciente]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PacienteConsulta'
CREATE INDEX [IX_FK_PacienteConsulta]
ON [dbo].[ConsultaSet]
    ([Paciente_Id]);
GO

-- Creating foreign key on [Consulta_Id] in table 'SintomaSet'
ALTER TABLE [dbo].[SintomaSet]
ADD CONSTRAINT [FK_ConsultaSintoma]
    FOREIGN KEY ([Consulta_Id])
    REFERENCES [dbo].[ConsultaSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ConsultaSintoma'
CREATE INDEX [IX_FK_ConsultaSintoma]
ON [dbo].[SintomaSet]
    ([Consulta_Id]);
GO

-- Creating foreign key on [Id] in table 'PessoaSet_Paciente'
ALTER TABLE [dbo].[PessoaSet_Paciente]
ADD CONSTRAINT [FK_Paciente_inherits_Pessoa]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PessoaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'PessoaSet_Utilizador'
ALTER TABLE [dbo].[PessoaSet_Utilizador]
ADD CONSTRAINT [FK_Utilizador_inherits_Pessoa]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[PessoaSet]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------