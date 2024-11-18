# Specify the minimum version of CMake
cmake_minimum_required(VERSION 3.10)

# Set the project name and version
project(BloodPressureTracker VERSION 1.0)


option(ENABLE_PATIENT "Enable feature A" ON)
option(ENABLE_MEASUREMENT "Enable feature B" ON)
option(ENABLE_TESTS "Enable tests" OFF)

if(ENABLE_PATIENT)
    add_definitions(-DENABLE_FEATURE_A)
    message(STATUS "Feature A is enabled")
endif()

if(ENABLE_MEASUREMENT)
    add_definitions(-DENABLE_FEATURE_B)
    message(STATUS "Feature B is enabled")
endif()

if(ENABLE_TESTS)
    add_definitions(-DENABLE_TESTS)
    message(STATUS "Tests are enabled")
endif()


# Set C++ standard to C++11 (or whichever version you prefer)
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_STANDARD_REQUIRED True)

# Set the build type to Debug to include debugging symbols
set(CMAKE_BUILD_TYPE Debug)

# Optional: Add debugging flags
set(CMAKE_CXX_FLAGS_DEBUG "-g -O0")

include_directories(${CMAKE_SOURCE_DIR}/src/headers)

# Specify the source files for the project
set(SRC_FILES
    bloodMeasurement.cpp
    Task.cpp
    threadPool.cpp
    databaseController.cpp
    patientController.cpp
    measurementController.cpp
    tcpSocket.cpp
    main.cpp
)

# Add executable target
add_executable(${PROJECT_NAME} ${SRC_FILES})

# Link MySQL Connector/C++ library
find_library(MYSQLCPP_LIB mysqlcppconn PATHS /usr/lib /usr/local/lib)
if (MYSQLCPP_LIB)
    target_link_libraries(${PROJECT_NAME} ${MYSQLCPP_LIB})
else()
    message(FATAL_ERROR "MySQL Connector/C++ library not found")
endif()

# Link pthread (POSIX threads)
target_link_libraries(${PROJECT_NAME} pthread)

# Link rt (real-time library for shared memory, shm_open, etc.)
target_link_libraries(${PROJECT_NAME} rt)
