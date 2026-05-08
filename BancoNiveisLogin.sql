create database LoginCore;
use LoginCore;

create table Cliente(
Id int auto_increment primary key,
Nome Varchar(50) not null,
Nascimento DateTime not null,
Sexo char(1),
CPF Varchar(11) not null,
Telefone Varchar(14) not null,
Email Varchar(50) not null,
Senha Varchar(8),
ConfirmacaoSenha Varchar(8) not null,
Situacao char(1) not null
);

create table Colaborador(
Id int auto_increment primary key,
Nome Varchar(50) not null,
Email Varchar(50) not null,
Senha Varchar(8) not null,
Tipo Varchar(8) not null
);