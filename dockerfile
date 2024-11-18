FROM ubuntu:latest

RUN apt-get update && \
    apt-get install -y \
    libmysqlcppconn-dev \
    && rm -rf /var/lib/apt/lists/*

WORKDIR /usr/src/app
COPY /build_output/BloodPressureTracker /usr/src/app/
ENV LD_LIBRARY_PATH="/usr/local/lib/mysqlcppconn:$LD_LIBRARY_PATH"
CMD ["./BloodPressureTracker"]