

#include "headers/Task.h"
namespace bms{
  Task::Task() : okFunction(nullptr), errorFunction(nullptr), finalFunction(nullptr) {}

  Task* Task::invoke() {
    // Create a Thread instance using manual new, suitable for C++11
    t = std::unique_ptr<Thread>(new Thread([this]() { this->execute(); }));
    
    // Start the thread
    t->start();
    return this;
}

  Task* Task::await(Task* waitOn) {
      // Ensure the current thread waits for the other thread to finish
      if (waitOn && waitOn->t) {
          waitOn->t->join();  // Wait for the thread in the other task to finish
      }
      return this;
  }

  Task *Task::await(){
    if(t){
        t->join();
    }
    return this;
  }

  Task *Task::ok(std::function<void()> handler) {
      okFunction = handler;
      return this;
  }

  Task *Task::error(std::function<void()> handler) {
      errorFunction = handler;
      return this;
  }

  Task *Task::run(std::function<int()> handler){
    taskFunction = handler;
    return this;
  }

  Task *Task::final(std::function<void()> handler) {
    finalFunction = handler;
    return this;
  }

  void Task::execute() {
    try {
      int result = taskFunction();

      if (result == 0 && okFunction) {
          okFunction();
      } else if (result != 0 && errorFunction) {
          errorFunction();
      }


    } catch (const std::exception& e) {
        std::cerr << "Error occurred: " << e.what() << std::endl;
        if (errorFunction) {
            errorFunction();
        }
    } catch (...) {
        std::cerr << "Unknown error occurred." << std::endl;
        if (errorFunction) {
            errorFunction();
        }
    }
    
    if (finalFunction) {
      finalFunction();
    } 
  }
}
