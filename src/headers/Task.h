#pragma once
#include <iostream>
#include <functional>
#include <string>
#include <memory>
#include <thread>
#include "threadPool.h"

/** Designed by Christian J
 *  This is my take on creating a async design, to do Fire and wait and fire and forget. 
 *  
 *  *Task()  run    -> entry function. (std::function<int()>)
 *  *Task()  error  -> will be called if something went wrong, or if run returns 1
 *  *Task()  final  -> this will be run everytime in the end. main goal is to clean up after the Task.
 *  *Task()  invoke -> Run the function, on a thread. 
 *  *Task()  await  -> join thread, can be as parmeter to chain task, or add caller thread.
 * 
 * Task ptask;
 * ptask.run([pointer, reff or copy variable](){
 * })->(next chain function)
 * 
 */

namespace bms{

    class Task{
    public:
    using TaskFunction = std::function<int()>;
    Task();
    Task *invoke();
    Task *await();
    Task *await(Task *waitOn);
    Task *run(std::function<int()> handler);
    Task *ok(std::function<void()> handler);
    Task *error(std::function<void()> handler);
    Task *final(std::function<void()> handler);
    void execute();


    void* payLoad;

    int id = 0;

    private:
    TaskFunction taskFunction;
    std::function<void()> okFunction;
    std::function<void()> errorFunction;
    std::function<void()> finalFunction;
  
    std::unique_ptr<Thread> t;



  };
}
