Základní operace s linqem:
http://www.c-sharpcorner.com/UploadFile/3d39b4/simple-select-insert-update-and-delete-using-linq-to-sql/

KniHOVNA
http://filehelpers.sourceforge.net/

Create table [Pivo]
(
	[id_pivo] Integer Identity NOT NULL,
	[znacka] Nvarchar(20) NOT NULL,
	[stupen] Integer NOT NULL Constraint [CK_stupen] Check (stupen>=1 and stupen<=20),
Primary Key ([id_pivo])
) 
go

Create table [PobockaPivovaru]
(
	[id_pivovaru] Integer Identity NOT NULL,
	[jmeno_piv] Nvarchar(20) NOT NULL,
	[mesto] Nvarchar(20) NOT NULL,
	[adresa] Nvarchar(20) NULL,
Primary Key ([id_pivovaru])
) 
go

Create table [Odebírá]
(
	[id_pivovaru] Integer NOT NULL,
	[id_pivo] Integer NOT NULL,
	[cena] Money NULL,
	[hospoda] Nvarchar(20) NOT NULL,
Primary Key ([id_pivovaru],[id_pivo],[hospoda])
) 
go

Create table [Vari]
(
	[id_pivovaru] Integer NOT NULL,
	[id_pivo] Integer NOT NULL,
Primary Key ([id_pivovaru],[id_pivo])
) 
go


Alter table [Odebírá] add  foreign key([id_pivo]) references [Pivo] ([id_pivo])  on update no action on delete cascade 
go
Alter table [Vari] add  foreign key([id_pivo]) references [Pivo] ([id_pivo])  on update no action on delete cascade 
go
Alter table [Odebírá] add  foreign key([id_pivovaru]) references [PobockaPivovaru] ([id_pivovaru])  on update no action on delete cascade 
go
Alter table [Vari] add  foreign key([id_pivovaru]) references [PobockaPivovaru] ([id_pivovaru])  on update no action on delete cascade 
go

ALTER TABLE [PobockaPivovaru] ADD CONSTRAINT UQ_Pobocka UNIQUE ([jmeno_piv],[mesto])
go

-- INSERTY
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('Vyškov', 'Vyškov', 'Čsl. Armády 116/7')
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('České Budějovice', 'Budějovický Budvar', 'Karolíny Světlé 4')
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('Liberec', 'Svijany', 'Máchova 492')
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('Příšovice', 'Svijany', 'Svijany 25')
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('Turnov', 'Rohozec', 'Malý Rohozec 29')
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('Velké Popovice', 'Plzeňský Prazdroj', 'Ringhofferova 1')
INSERT INTO PobockaPivovaru (mesto, jmeno_piv, adresa) VALUES ('Plzeň', 'Plzeňský Prazdroj', 'U Prazdroje 7')

DELETE FROM Pivo

INSERT INTO Pivo (znacka, stupen) VALUES ('Budvar ležák', 12)
INSERT INTO Pivo (znacka, stupen) VALUES ('Budvar výčepní', 10)
INSERT INTO Pivo (znacka, stupen) VALUES ('Pardál', 10)
INSERT INTO Pivo (znacka, stupen) VALUES ('Pardál Echt', 11)

INSERT INTO Pivo (znacka, stupen) VALUES ('Gambrinus', 10)
INSERT INTO Pivo (znacka, stupen) VALUES ('Gambrinus Excelent', 11)
INSERT INTO Pivo (znacka, stupen) VALUES ('Gambrinus Premium', 12)
INSERT INTO Pivo (znacka, stupen) VALUES ('Pilsner Urquell', 12)

INSERT INTO Pivo (znacka, stupen) VALUES ('Baron', 15)
INSERT INTO Pivo (znacka, stupen) VALUES ('Svijanská Kněžna', 13)
INSERT INTO Pivo (znacka, stupen) VALUES ('Svijanský Kníže', 13)
INSERT INTO Pivo (znacka, stupen) VALUES ('Svijanský Rytíř', 12)

/*
Svijanská Desítka 10 %
Svijanský Máz 11 %
Svijanský fanda 11 %
Svijanský Rytíř 12 %
Kvasničák 13 %
Svijanský Kníže 13 %
Svijanská Kněžna 13 %
Baron 15 %
Fitness
Svijanský Vozka
Weizen 12 %
*/

INSERT INTO Pivo (znacka, stupen) VALUES ('Podskalák', 10)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skaláczech', 7)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skalák', 11)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skalák malinový', 11)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skalák Premium', 12)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skalák řezaný', 11)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skalák speciál', 13)
INSERT INTO Pivo (znacka, stupen) VALUES ('Skalák speciál tmavý', 13)

INSERT INTO Pivo (znacka, stupen) VALUES ('Kozel Nealko', 1)
INSERT INTO Pivo (znacka, stupen) VALUES ('Kozel Premium', 12)
INSERT INTO Pivo (znacka, stupen) VALUES ('Kozel světlý', 10)

INSERT INTO Pivo (znacka, stupen) VALUES ('Alkostop', 1)
INSERT INTO Pivo (znacka, stupen) VALUES ('Atlet', 8)
INSERT INTO Pivo (znacka, stupen) VALUES ('Březňák', 12)
INSERT INTO Pivo (znacka, stupen) VALUES ('Desítka', 10)
INSERT INTO Pivo (znacka, stupen) VALUES ('Džbán', 11)
INSERT INTO Pivo (znacka, stupen) VALUES ('Džbán tmavý', 11)
INSERT INTO Pivo (znacka, stupen) VALUES ('Generál', 14)
INSERT INTO Pivo (znacka, stupen) VALUES ('Jubiler', 16)
INSERT INTO Pivo (znacka, stupen) VALUES ('Řezák', 11)

INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (1,1)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (1,2)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (1,3)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (1,4)

INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (2,1)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (2,2)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (2,3)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (2,4)

INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (3,1)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (3,2)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (3,3)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (3,4)

INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,1)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,2)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,3)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,4)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,5)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,6)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,7)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (4,8)

INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (5,1)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (5,2)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (5,3)

INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,1)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,2)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,3)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,4)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,5)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,6)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,7)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,8)
INSERT INTO Vari (id_pivovaru, id_pivo) VALUES (6,9)

DELETE FROM Odebírá

INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Blanice', 1, 1, 25)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Blanice', 5, 2, 22)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Blanice', 4, 3, 22)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Blanice', 3, 1, 26)

INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Na Mýtince', 3, 1, 27)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Na Mýtince', 3, 2, 23)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Na Mýtince', 3, 3, 21)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('Na Mýtince', 3, 4, 19)

INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Balvanu', 1, 4, 22)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Balvanu', 5, 3, 16)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Balvanu', 4, 4, 20)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Balvanu', 3, 3, 20)

INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Biskupa', 1, 2, 20)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Biskupa', 1, 3, 17)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Biskupa', 2, 1, 18)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Biskupa', 5, 3, 15)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Biskupa', 4, 1, 16)
INSERT INTO Odebírá (hospoda, id_pivovaru, id_pivo, cena) VALUES ('U Biskupa', 3, 3, 20)
