@ECHO OFF
docker-compose up --build
docker cp .\\db\\ExpoDB.sql expoapp_db_1:/ 
docker exec expoapp_db_1 /bin/sh -c '/opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Expo123456 -i /ExpoDB.sql'