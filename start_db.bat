ECHO off

sqlcmd -S localhost -E -i Night_Rider.sql


rem server is local host

ECHO .
ECHO if no errors appear DB was created
PAUSE
