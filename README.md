# WitneticNotes
Project example of arquitect clear, using c#, .net core 3.1, docker, sql server

# Requirement
.net core 3.1 

docker

# Installation
clone the project 

reset packages nugets

run script SQL_DB.sql in sql server
the script  is in root project



# Setting

edit the file appsettings.json in Notes.Api

configuration conexion database
in DatabaseConnection replace 

Server-BD write your server DB

USERDB write your user DB

PASSWORD write your password DB

TOKEN-SECRET write your token example sDFDke73OkImW3MKIOWELsad343dasQ


#create image docker

in the root project run 

docker build -t name .




