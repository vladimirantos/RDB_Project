CREATE TABLE [dbo].[Dblog] (
    [id_log]     INT           IDENTITY (1, 1) NOT NULL,
    [action]     VARCHAR (10)  NOT NULL,
	[table]	varchar(12) not null,
    [condition]  VARCHAR (255) NULL,
    [count_rows] INT           NOT NULL,
	[time] DATETIME DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT [pk_id_log] PRIMARY KEY CLUSTERED ([id_log] ASC),
    CONSTRAINT [chk_action] CHECK ([action]='export' OR [action]='delete' OR [action]='update' OR [action]='select' OR [action]='insert')
);

CREATE TABLE [dbo].[Devices] (
    [serial_number] VARCHAR (40) NOT NULL,
    [accuracy]      FLOAT (53)   NOT NULL,
    [description]   VARCHAR (50) COLLATE Czech_CI_AS NOT NULL,
    CONSTRAINT [pk_id_device] PRIMARY KEY CLUSTERED ([serial_number] ASC),
    CONSTRAINT [chk_accuracy] CHECK ([accuracy] >= (0)
                                         AND [accuracy] <= (1))
);

CREATE TABLE [dbo].[Measurements] (
    [id_measurement] INT           IDENTITY (1, 1) NOT NULL,
    [device]         VARCHAR (40)     NOT NULL,
    [mtype]          SMALLINT      NOT NULL,
    [description]    VARCHAR (50)  COLLATE Czech_CI_AS NOT NULL,
    [date]           SMALLDATETIME NOT NULL,
    CONSTRAINT [pk_id_measurement] PRIMARY KEY CLUSTERED ([id_measurement] ASC),
    CONSTRAINT [fk_device] FOREIGN KEY ([device]) REFERENCES [dbo].[Devices] ([serial_number]) ON DELETE CASCADE,
    CONSTRAINT [fk_mtype] FOREIGN KEY ([mtype]) REFERENCES [dbo].[MTypes] ([id_mtype]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[MTypes] (
    [id_mtype] SMALLINT     IDENTITY (1, 1) NOT NULL,
    [name]     VARCHAR (30) COLLATE Czech_CI_AS NOT NULL,
    CONSTRAINT [pk_id_mtype] PRIMARY KEY CLUSTERED ([id_mtype] ASC),
    CONSTRAINT [uc_name] UNIQUE NONCLUSTERED ([name] ASC)
);

CREATE TABLE [dbo].[Points] (
    [x]           FLOAT (53)   NOT NULL,
    [y]           FLOAT (53)   NOT NULL,
    [measurement] INT          NOT NULL,
    [value1]      FLOAT (53)   NOT NULL,
    [value2]      FLOAT (53)   NOT NULL,
    [variance]    FLOAT (53)   NOT NULL,
    [description] VARCHAR (50) COLLATE Czech_CI_AS NOT NULL,
    CONSTRAINT [pk_points] PRIMARY KEY CLUSTERED ([x] ASC, [y] ASC, [measurement] ASC),
    CONSTRAINT [fk_measurement] FOREIGN KEY ([measurement]) REFERENCES [dbo].[Measurements] ([id_measurement]) ON DELETE CASCADE,
    CONSTRAINT [chk_variance] CHECK ([variance]>=(0) AND [variance]<=(1))
);
