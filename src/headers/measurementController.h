#pragma once
#include "bloodMeasurement.h"
namespace bms
{
  class MeasurementController{
    public:
    MeasurementController(DatabaseController *db);
    
    void createMeasurement(measurementModel &model);
    void readMeasurement(std::string ssn, std::vector<measurementModel> &resultset);
    void readMeasurement(std::string ssn);
    void readMeasurement(unsigned int id);
    void updateMeasurement(measurementModel *model);
    void deleteMeasurement(measurementModel *model);

    void deleteMeasurement(unsigned int id);

  private: 
    DatabaseController *db;

  };
} // namespace bms
