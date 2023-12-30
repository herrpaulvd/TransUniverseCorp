FROM mcr.microsoft.com/mssql/server:2022-latest as img
ENV ACCEPT_EULA="Y"
ENV MSSQL_SA_PASSWORD="228c@T228"
EXPOSE 1433
WORKDIR /home
COPY DB/dbrestore.sql ./
USER root
RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "228c@T228" -i '/home/dbrestore.sql' && pkill sqlservr
