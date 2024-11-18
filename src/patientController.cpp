#include "headers/patientController.h"

bms::PatientController::PatientController(DatabaseController* db) {
    if (db == nullptr) {
        std::cerr << "Error: DatabaseController pointer is null!" << std::endl;
        this->db = nullptr; // or throw an exception, or handle it based on your needs
    } else {
        this->db = db;
    }
}


void bms::PatientController::createPatient(patientModel &model)
{
  try {
        // Connect to the database (replace with your actual MySQL credentials)
        auto con = db->getConnection();

        if(con == nullptr)
          printf("con is null");

        // Prepare an SQL insert statement
        std::unique_ptr<sql::PreparedStatement> pstmt(con->prepareStatement(
            "INSERT INTO patients (ssn, mail, name) VALUES (?, ?, ?)"
        ));

        // Set the parameters in the prepared statement
        pstmt->setString(1, model.ssn);
        pstmt->setString(2, model.mail);
        pstmt->setString(3, model.name);

        // Execute the query
        pstmt->executeUpdate();

        std::cout << "Patient data stored successfully!" << std::endl;
    } catch (sql::SQLException &e) {
        printf("createPation():  %s", e.what());
    }
}

void bms::PatientController::getPatients(std::vector<patientModel> &models)
{
  try
  {
    if(db->getConnection() == nullptr)
      std::cerr << "getConnection() is null" << "\n";

    std::unique_ptr<sql::PreparedStatement> pstmt(db->getConnection()
      ->prepareStatement("SELECT * FROM patients"));
    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());
    while (res->next()) {

      patientModel patient;

      patient.ssn = res->getString("ssn");
      patient.mail = res->getString("mail");
      patient.name = res->getString("name");
      models.push_back(patient);
    }
  }
  catch(sql::SQLException &e)
  {
    std::cerr << e.what() << '\n';
  }
}

void bms::PatientController::getPatient(std::string ssn, patientModel &patient)
{
  try
  {
    if(db->getConnection() == nullptr)
      std::cerr << "getConnection() is null" << "\n";

    std::unique_ptr<sql::PreparedStatement> pstmt(db->getConnection()
      ->prepareStatement("SELECT * FROM patients WHERE ssn = ?"));
    pstmt->setString(1, ssn);
    std::unique_ptr<sql::ResultSet> res(pstmt->executeQuery());




    std::cout << "\n";
    while (res->next()) {
      patient.ssn   = res->getString("ssn");
      patient.mail  = res->getString("mail");
      patient.name  = res->getString("name");
    }
  }
  catch(sql::SQLException &e)
  {
    std::cerr << e.what() << '\n';
  }

}




