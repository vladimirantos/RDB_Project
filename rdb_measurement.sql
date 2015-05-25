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
	description varchar(50) not null,
	accuracy float not null,
	constraint pk_serialNumber primary key (serialNumber)
);

create table MTypes(
	idType int not null,
	name varchar(30) not null,
	constraint pk_idType primary key(idType),
	constraint uk_name unique(name)
);

create table Measurements(
	idMeasurement int not null,
	serialNumberDevice varchar(40) not null,
	idMtype int not null,
	[description] varchar(30) not null,
	unit varchar(10) not null,
	[date] datetime2 not null,
	constraint pk_idMeasurement primary key(idMeasurement),
	constraint fk_device foreign key (serialNumberDevice) references devices(serialNumber) on delete cascade,
	constraint fk_mtype foreign key (idMtype) references MTypes(idType) on delete cascade
);

create table Points(
	id_point int not null,
	idMeasurement int not null,
	x float not null,
	y float not null,
	value1 float not null,
	value2 float not null,
	variance float not null,
	description varchar(50) not null,
	constraint pk_points primary key (id_point, idMeasurement),
	constraint fk_measurement foreign key(idMeasurement) references Measurements(idMeasurement),
	constraint chk_variance check(variance >= 0 and variance <= 1)
);

--create view SearchResult as 
--select date, x, y, value1, value2, (value1 - value2) as difference, serialNumber from Measurements as m
--join Devices as d on d.serialNumber = m.serialNumberDevice
--join Points as p on p.idMeasurement = m.idMeasurement

--create view Test as
--select date, unit, id_point as idPoint, x, y, po.description as poinDescription, value1, value2, variance,
--serialNumber, de.description as deviceDescription, mt.idType, mt.name from Measurements as m
--join Devices as de ON de.serialNumber = m.serialNumberDevice
--join MTypes as mt ON mt.idType = m.idMtype
--join Points as po ON po.idMeasurement = m.idMeasurement;

insert into Devices(serialNumber, accuracy, description)VALUES('001', 0.1, 'Jirka'), ('002', 0.5, 'Pristroj1'), ('003', 0.4, 'Pristroj2');
insert into MTypes(idType, name)VALUES(1, 'mìøení 1'), (2, 'Mìøení2'), (3, 'mìøení 3');
insert into Measurements(idMeasurement, device, mtype, description, date)
VALUES(1, '001', 1, 'mìøení a', 1432126019),
(2, '001', 2, 'mìøení b', GETDATE()),
(3, '002', 3, 'mìøení c', GETDATE());
insert into Points(x, y, measurement, value1, value2, variance, description)
values(0,0, 1, 150, 168.5, 0.5, 'nìco jsem zmìøil'),
(10, 15, 2, 38, 68, 0.01, 'zase jsem nìco namìøil');