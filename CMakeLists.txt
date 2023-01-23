cmake_minimum_required(VERSION 3.23)
project(lunac)

set(CMAKE_CXX_STANDARD 23)

add_executable(lunac main.cpp)

add_subdirectory(curlcpp)
INCLUDE_DIRECTORIES(${CURLCPP_SOURCE_DIR}/include)