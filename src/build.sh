#!/bin/bash

echo "Building the application locally..."

# Clean the build output directory
rm -rf build/*
mkdir -p build
cd build

# Run CMake to configure the project (uncommented)
cmake ../src/

# Check if CMake was successful
if [ $? -ne 0 ]; then
    echo "CMake configuration failed! Exiting..."
    exit 1
fi

# Build the application with all cores available
cmake --build . -j

# Check if the build was successful
if [ $? -ne 0 ]; then
    echo "Application build failed! Exiting..."
    exit 1
fi

echo "Build completed successfully."
