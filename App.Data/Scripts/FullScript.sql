CREATE TABLE [Browser] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Browser] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Plataform] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Plataform] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [Url] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [ShortUrl] nvarchar(5) NOT NULL,
    [OriginalUrl] nvarchar(500) NOT NULL,
    [DateCreated] datetime2 NOT NULL,
    CONSTRAINT [PK_Url] PRIMARY KEY ([Id])
);
GO


CREATE TABLE [UrlMetric] (
    [Id] int NOT NULL IDENTITY,
    [DateClicked] datetime2 NOT NULL,
    [UrlId] uniqueidentifier NOT NULL,
    [BrowserId] int NOT NULL,
    [PlataformId] int NOT NULL,
    CONSTRAINT [PK_UrlMetric] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_UrlMetric_Browser_BrowserId] FOREIGN KEY ([BrowserId]) REFERENCES [Browser] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UrlMetric_Plataform_PlataformId] FOREIGN KEY ([PlataformId]) REFERENCES [Plataform] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UrlMetric_Url_UrlId] FOREIGN KEY ([UrlId]) REFERENCES [Url] ([Id]) ON DELETE CASCADE
);
GO


CREATE UNIQUE INDEX [IX_Browser_Name] ON [Browser] ([Name]);
GO


CREATE UNIQUE INDEX [IX_Plataform_Name] ON [Plataform] ([Name]);
GO


CREATE UNIQUE INDEX [IX_Url_OriginalUrl] ON [Url] ([OriginalUrl]);
GO


CREATE UNIQUE INDEX [IX_Url_ShortUrl] ON [Url] ([ShortUrl]);
GO


CREATE INDEX [IX_UrlMetric_BrowserId] ON [UrlMetric] ([BrowserId]);
GO


CREATE INDEX [IX_UrlMetric_PlataformId] ON [UrlMetric] ([PlataformId]);
GO


CREATE INDEX [IX_UrlMetric_UrlId] ON [UrlMetric] ([UrlId]);
GO


