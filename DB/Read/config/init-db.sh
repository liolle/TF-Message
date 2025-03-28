#!/bin/bash
DBSTATUS=1
ERRCODE=1
i=0

while [[ $DBSTATUS -ne 0 ]] && [[ $i -lt 60 ]] && [[ $ERRCODE -ne 0 ]]; do
  i=$i+1
  DBSTATUS=$(/opt/mssql-tools18/bin/sqlcmd -h -1 -t 1 -U sa -P $SA_PASSWORD -Q "SET NOCOUNT ON; Select SUM(state) from sys.databases")
  ERRCODE=$?
  sleep 1
done

echo "Running setup"
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P $SA_PASSWORD -N -C -d master -i setup.sql
