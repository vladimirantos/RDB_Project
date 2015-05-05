create table Dblog(
	idLog int identity,
	action varchar(10) not null,
	[table] varchar(12) not null,
	time datetime DEFAULT GETDATE(),
	constraint pk_id_log primary key(idLog),
	constraint ck_action check (action='insert' OR action='update' OR action='delete' OR action='select')
);

create table Devices(
	serialNumber varchar(40) not null,
	accuracy float not null,
	description varchar(50) not null,
	constraint pk_serialNumber primary key (serialNumber),
	constraint chk_accuracy check (accuracy >= 0 AND accuracy <= 1)
);

create table MTypes(
	idType int not null,
	name varchar(30) not null,
	constraint pk_idType primary key(idType),
	constraint uk_name unique(name)
);

create table Measurements(
	idMeasurement int not null,
	device varchar(40) not null,
	mtype int not null,
	[description] varchar(30) not null,
	[date] datetime not null,
	constraint pk_idMeasurement primary key(idMeasurement),
	constraint fk_device foreign key (device) references devices(serialNumber) on delete cascade,
	constraint fk_mtype foreign key (mtype) references MTypes(idType) on delete cascade
);

create table Points(
	x float not null,
	y float not null,
	measurement int not null,
	value1 float not null,
	value2 float not null,
	variance float not null,
	description varchar(50) not null,
	constraint pk_points primary key (x, y, measurement),
	constraint fk_measurement foreign key(measurement) references Measurements(idMeasurement),
	constraint chk_variance check(variance >= 0 and variance <= 1)
);