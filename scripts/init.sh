#!/bin/bash

# Env variables
server=${DB_SERVER_NAME}
pass=${SA_PASSWORD}


# Wait for SQL server to be up
echo "Waitinig 15 seconds ..."
sleep 15s
echo "Creating database ... "
/opt/mssql-tools/bin/sqlcmd -S "${server}" -U sa -P "${pass}" -d master -i /opt/mssql_scripts/initial_set_up.sql