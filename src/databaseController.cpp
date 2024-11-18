#include "headers/databaseController.h"

bms::DatabaseController::DatabaseController()
{
  driver = nullptr;
  connection = nullptr;
}

bms::DatabaseController::~DatabaseController()
{
  if(connection)
    connection->close();
}


//- TODO Finish this, for the controller class, to remove code dubli... 
std::unique_ptr<sql::ResultSet> bms::DatabaseController::executeQuery(const std::string& query) {
    if (getConnection() == nullptr) {
        std::cerr << "Connection is null!" << std::endl;
        return nullptr;
    }

    try {
        std::unique_ptr<sql::PreparedStatement> pstmt(getConnection()->prepareStatement(query));
        return std::unique_ptr<sql::ResultSet>(pstmt->executeQuery());
    } catch (const sql::SQLException& e) {
        std::cerr << "Error executing query: " << e.what() << std::endl;
        return nullptr;
    }
}

bool bms::DatabaseController::openConnection() {
    try {
        driver = sql::mysql::get_mysql_driver_instance();
        connection = std::shared_ptr<sql::Connection>(
            driver->connect("tcp://region_db:3306", "root", "my-secret-pw")
        );

      connection->setSchema("bm_db");
      std::cerr << "Connection Open!\n";
      return true;
    } catch (const sql::SQLException& e) {
        std::cerr << "Error opening connection: " << e.what() << '\n';
        return false;
    }
}

std::shared_ptr<sql::Connection> bms::DatabaseController::getConnection() {
  return connection;
}