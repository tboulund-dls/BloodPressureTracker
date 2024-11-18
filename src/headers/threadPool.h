#pragma once
#include <pthread.h>
#include <functional>
namespace bms
{
  class Thread {
    public:
        // Constructor that accepts a std::function instead of a raw function pointer
        explicit Thread(std::function<void()> func);

        // Start the thread
        void start();

        // Join the thread
        void join();
        void joinWith(Thread &other);

    private:
        static void* threadFunction(void* arg);

        std::function<void()> func_;  // Store the function to execute in the thread
        pthread_t thread_id;
    };

} // namespace bmt



