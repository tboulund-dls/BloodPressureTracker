#include "headers/measurementController.h"
bms::MeasurementController::MeasurementController(DatabaseController *db)
{
  if (db == nullptr) {
        std::cerr << "Error: DatabaseController pointer is null!" << std::endl;
        this->db = nullptr; // or throw an exception, or handle it based on your needs
    } else {
        this->db = db;
    }
}

//- Create new measurement
void bms::MeasurementController::createMeasurement(measurementModel &model){
  auto con = db->getConnection();

  string query = "INSERT INTO measurements (ssn, date, systolic, diastolic, seen) VALUES (?, ?, ?, ?, ?)";

  try {
        // Connect to the database (replace with your actual MySQL credentials)
        auto con = db->getConnection();

        if(con == nullptr)
          printf("con is null");

        // Prepare an SQL insert statement
        std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
            "INSERT INTO measurements (ssn, date, systolic, diastolic, seen) VALUES (?, ?, ?, ?, ?)"
        ));

        // Set the parameters in the prepared statement
        pstmt->setString(1, model.ssn);
        pstmt->setString(2, model.date);
        pstmt->setUInt(3, model.systolic);
        pstmt->setUInt(4, model.diastolic);
        pstmt->setBoolean(5, model.seen);

        // Execute the query
        pstmt->executeUpdate();

        std::cout << "Measurement stored successfully!" << std::endl;
    } catch (sql::SQLException &e) {
        printf("createPation():  %s", e.what());
    }
}
void bms::MeasurementController::readMeasurement(std::string ssn, std::vector<bms::measurementModel> &resultset){
  try
  {
    auto con = db->getConnection();
    if(con == nullptr)
      printf("con is null");

    // Prepare an SQL insert statement
    std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
        "Query"
    ));

    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());
    while (res->next()) {

      patientModel patient;
      patient.ssn = res->getString("ssn");
      patient.mail = res->getString("mail");
      patient.name = res->getString("name");
    }

  }
  catch(const std::exception& e)
  {
    std::cerr << e.what() << '\n';
  }
}
void bms::MeasurementController::readMeasurement(unsigned int id){
  try
  {
    auto con = db->getConnection();
    if(con == nullptr)
      printf("con is null");

    // Prepare an SQL insert statement
    std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
        "Query"
    ));

    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());
    while (res->next()) {

      patientModel patient;
      patient.ssn = res->getString("ssn");
      patient.mail = res->getString("mail");
      patient.name = res->getString("name");
    }

  }
  catch(const std::exception& e)
  {
    std::cerr << e.what() << '\n';
  }
}
void bms::MeasurementController::updateMeasurement(measurementModel *model){
  try
  {
    auto con = db->getConnection();
    if(con == nullptr)
      printf("con is null");

    // Prepare an SQL insert statement
    std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
        "Query"
    ));

    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());
    while (res->next()) {

      patientModel patient;
      patient.ssn = res->getString("ssn");
      patient.mail = res->getString("mail");
      patient.name = res->getString("name");
    }

  }
  catch(const std::exception& e)
  {
    std::cerr << e.what() << '\n';
  }

}
void bms::MeasurementController::deleteMeasurement(measurementModel *model){
  try
  {
    auto con = db->getConnection();
    if(con == nullptr)
      printf("con is null");

    // Prepare an SQL insert statement
    std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
        "Query"
    ));

    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());
    while (res->next()) {

      patientModel patient;
      patient.ssn = res->getString("ssn");
      patient.mail = res->getString("mail");
      patient.name = res->getString("name");
    }

  }
  catch(const std::exception& e)
  {
    std::cerr << e.what() << '\n';
  }
}
void bms::MeasurementController::deleteMeasurement(unsigned int id){
  try
  {
    auto con = db->getConnection();
    if(con == nullptr)
      printf("con is null");

    // Prepare an SQL insert statement
    std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
        "Query"
    ));

    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());
    while (res->next()) {

      patientModel patient;
      patient.ssn = res->getString("ssn");
      patient.mail = res->getString("mail");
      patient.name = res->getString("name");
    }

  }
  catch(const std::exception& e)
  {
    std::cerr << e.what() << '\n';
  }
}