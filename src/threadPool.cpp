#include "headers/threadPool.h"

namespace bms
{
  // Constructor that takes a std::function
    Thread::Thread(std::function<void()> func) : func_(func), thread_id(0) {}

    // Start the thread by calling pthread_create
    void Thread::start() {
        pthread_create(&thread_id, nullptr, Thread::threadFunction, this);
    }

    // Join the thread
    void Thread::join() {
        if (thread_id != 0) {
            pthread_join(thread_id, nullptr);
        }
    }

    void Thread::joinWith(Thread& other) {
        other.join(); // Wait for the other thread to finish
        this->join();
    }

    // Static function that runs the user-provided function in the thread
    void* Thread::threadFunction(void* arg) {
        Thread* self = static_cast<Thread*>(arg);
        self->func_();  // Call the function passed to the thread
        return nullptr;
    }
} // namespace bms

