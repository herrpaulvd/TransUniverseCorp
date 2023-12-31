FROM mcr.microsoft.com/mssql/server:2022-latest as img
ENV ACCEPT_EULA="Y"
ENV MSSQL_SA_PASSWORD="228c@T228"
EXPOSE 1433
WORKDIR /home
COPY CustomerDAL/customer.sql ./
COPY DriverDAL/driver.sql ./
COPY SpaceRouteDAL/spaceroute.sql ./
COPY UserDAL/user.sql ./
USER root
RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 10 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "228c@T228" -i '/home/customer.sql' && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "228c@T228" -i '/home/driver.sql' && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "228c@T228" -i '/home/spaceroute.sql' && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P "228c@T228" -i '/home/user.sql' && pkill sqlservr
