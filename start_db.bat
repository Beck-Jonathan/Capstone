ECHO off

sqlcmd -S localhost -E -i Night_Rider.sql
sqlcmd -S localhost -E -i Select_Part.sql


rem server is local host

ECHO .
ECHO if no errors appear DB was created
PAUSE