#!/bin/bash
set -e

# Start SQL Server
echo "Starting SQL Server in background..."
/opt/mssql/bin/sqlservr &

# Wait for SQL Server to start
echo "Waiting for SQL Server to start"
sleep 20

# Run the setup script to create the DB and the schema in the DB
echo "Running init.sql..."
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "$MSSQL_SA_PASSWORD" -C -i /docker-entrypoint-initdb.d/init.sql

# Keep the container running
echo "Initialization script completed. Keeping container running..."
wait
