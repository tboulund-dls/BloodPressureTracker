#!/bin/bash

echo "Building the application locally..."

# Clean the build output directory
rm -rf ../build_output/*
mkdir -p ../build_output
cd ../build_output

# Run CMake with Debugging enabled
cmake -DCMAKE_BUILD_TYPE=Debug ../src/

# Build the application with all cores available
cmake --build . -j

# Check if the build was successful
if [ $? -ne 0 ]; then
    echo "Application build failed! Exiting..."
    exit 1
fi

echo "Build completed successfully."

# Check if the Docker container is running, if not start it
if [ "$(docker ps -q -f name=bms_db)" ]; then
    echo "bms_db container is already running."
else
    echo "Starting bms_db container..."
    docker run --name bms_db -d -p 3306:3306 bms_db
fi

# Enable core dumps
echo "Enabling core dumps..."
ulimit -c unlimited  # This allows core dumps to be generated

# Set the core dump pattern (optional)
# This sets where the core dumps are saved and how they are named
echo "/tmp/core.%e.%p" > /proc/sys/kernel/core_pattern



# Run the application with debugging symbols and core dump enabled
echo "Running the application with GDB for debugging..."
gdb --args ./BloodPressureTracker
