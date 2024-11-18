#pragma once
#include "bloodMeasurement.h"
#include <vector>
namespace bms
{
  class PatientController{
    public:
    PatientController(DatabaseController* db);


    /// @brief Create new patient
    void createPatient(patientModel &model);
    /// @brief Get patient
    void getPatients(std::vector<patientModel> &models);
    void getPatient(std::string ssn, patientModel &patient);

  private:
    DatabaseController *db;

  };
} // namespace bms
