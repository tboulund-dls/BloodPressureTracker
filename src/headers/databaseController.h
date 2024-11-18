#pragma once

#include <iostream>
#include <mysql_driver.h>
#include <mysql_connection.h>
#include <cppconn/statement.h>
#include <cppconn/resultset.h>
#include <cppconn/exception.h>
#include <cppconn/prepared_statement.h>


namespace bms {
    class DatabaseController {
    public:
        DatabaseController();
        ~DatabaseController();
        std::unique_ptr<sql::ResultSet> executeQuery(const std::string &query);
        bool openConnection();
        std::shared_ptr<sql::Connection> getConnection();

    private:
        sql::mysql::MySQL_Driver* driver;
        std::shared_ptr<sql::Connection> connection;
    };
}