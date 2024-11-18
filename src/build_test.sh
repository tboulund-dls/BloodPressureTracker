#!/bin/bash

echo "Building the application locally..."

rm -rf build_test/*
mkdir -p build_test
cd build_test

cmake -DENABLE_TESTS=ON ../src/

if [ $? -ne 0 ]; then
    echo "CMake configuration failed! Exiting..."
    exit 1
fi

cmake --build . -j

if [ $? -ne 0 ]; then
    echo "Application build failed! Exiting..."
    exit 1
fi

echo "Build completed successfully."
