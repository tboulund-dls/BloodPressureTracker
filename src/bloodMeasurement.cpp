#include "headers/bloodMeasurement.h"


namespace bms
{
  const char* connectionstring  = std::getenv("connectionstring");
  const char* systemApplication = std::getenv("sysApp");
  const char* patientActive     = std::getenv("patient_active");
  const char* measureActive     = std::getenv("measure_active");
  bool IsRunning = false;


  Task tasks[50];
  JsonElement payload[50];
  static int ti = 0;
  static int ji = 0;

  static bool lock = false;

  void lockTask(){
    lock = true;
  }

  void UnlockTask(){
    lock = false;
  }

  bool isLocked(){
    return lock;
  }

  void write(string s)
  {
    std::cout << s;
  }

  void writeLine(string s)
  {
    std::cout << s << "\n";
  }


  Task *createTask(){
    if(ti >= 50)
      ti = 0;
    return &tasks[ti++];
  }

  void createJson(const char* rawString, int &id){
    if(ji >= 50)
      ji = 0;
    payload[ji] = JsonElement(rawString);
    payload[ji].handleRawData();
    id = ji++;
  }
  
  
  JsonElement *getJson(int id){
    if(id >= 50)
      return nullptr;
    return &payload[id];
  }



  Task *createTask(int bufferID){
    if(ti >= 50)
      ti = 0;

    tasks[ti].id = bufferID;
    return &tasks[ti++];      
  }

  
} // namespace bms




