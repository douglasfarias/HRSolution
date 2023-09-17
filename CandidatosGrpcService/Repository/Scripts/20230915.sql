-- create database hrsolution;
-- go

create table Candidatos
(
    Codigo    uniqueidentifier primary key default newid(),
    Nome      varchar(300) not null,
    Sobrenome varchar(300) not null,
    Email     varchar(300) not null,
    Telefone  varchar(15)  not null,
    Inativo   bit          not null        default 0,
);
go

create or alter procedure ListarCandidatos
as
begin
    select * from Candidatos;
end;
go

create type BuscarCandidatoCommand as table
(
    Codigo uniqueidentifier not null
);
go

create or alter procedure BuscarCandidato(@command BuscarCandidatoCommand readonly)
as
begin
    select *
    from Candidatos
             left join @command as command on Candidatos.Codigo = command.Codigo;

end;
go

create type AdicionarCandidatoCommand as table
(
    Nome      varchar(300) not null,
    Sobrenome varchar(300) not null,
    Email     varchar(300) not null,
    Telefone  varchar(15)  not null
);
go

create or alter procedure AdicionarCandidato(@command AdicionarCandidatoCommand readonly)
as
begin
    insert into Candidatos (Nome, Sobrenome, Email, Telefone)
    output inserted.*
    select command.Nome, command.Sobrenome, command.Email, command.Telefone
    from @command as command;
end;
go

create type EditarCandidatoCommand as table
(
    Codigo    uniqueidentifier not null,
    Nome      varchar(300)     not null,
    Sobrenome varchar(300)     not null,
    Email     varchar(300)     not null,
    Telefone  varchar(15)      not null
);
go

create or alter procedure EditarCandidato(@command EditarCandidatoCommand readonly)
as
begin
    update Candidatos
    set Nome      = command.Nome,
        Sobrenome = command.Sobrenome,
        Email     = command.Email,
        Telefone  = command.Telefone
    output inserted.*
    from @command as command
    where Candidatos.Codigo = command.Codigo;

end;
go

create type InativarCandidatoCommand as table
(
    Codigo uniqueidentifier not null
);
go

create or alter procedure InativarCandidato(@command InativarCandidatoCommand readonly)
as
begin
    update Candidatos set Inativo = 1 where Codigo = (select command.Codigo from @command as command);

end;
go
