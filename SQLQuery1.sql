select * from Clinica

select * from Sucursal


insert into Sucursal("ClinicaId") values(17)

delete from Sucursal

DBCC CHECKIDENT (Sucursal, RESEED, 0);
