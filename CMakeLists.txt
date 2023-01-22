set(APP_VERSION "0.1.0")
set(APP_ID "lunac")
set(APP_NAME "Luna Compiler")
set(APP_DESCRIPTION "Command line application for creating and compiling Luna projects.")
set(COMPILE_ID "lunac")

MACRO(RECURSE_FILES return_list filter)
    FILE(GLOB_RECURSE new_list ${filter})
    SET(file_list "")
    FOREACH(file_path ${new_list})
        SET(file_list ${file_list} ${file_path})
    ENDFOREACH()
    LIST(REMOVE_DUPLICATES file_list)
    SET(${return_list} ${file_list})
ENDMACRO()

cmake_minimum_required(VERSION 3.21)
project(${COMPILE_ID})

set(CMAKE_EXPORT_COMPILE_COMMANDS ON)

set(CMAKE_CXX_STANDARD 20)
set(CMAKE_CXX_STANDARD_REQUIRED 20)

set(SOURCE_DIR ${CMAKE_CURRENT_SOURCE_DIR}/src)
set(INCLUDE_DIR ${CMAKE_CURRENT_SOURCE_DIR}/include)

add_compile_definitions(APP_NAME=\"${APP_NAME}\")
add_compile_definitions(APP_DESCRIPTION=\"${APP_DESCRIPTION}\")

RECURSE_FILES(cpp_file_list ${SOURCE_DIR}/*.cpp)
RECURSE_FILES(c_file_list ${SOURCE_DIR}/*.c)

add_executable(
        ${COMPILE_ID}
        ${cpp_file_list}
        ${c_file_list}
)

target_include_directories(${COMPILE_ID} PRIVATE .)
target_include_directories(${COMPILE_ID} PRIVATE ${SOURCE_DIR})
target_include_directories(${COMPILE_ID} PRIVATE ${INCLUDE_DIR})

# CLI11 header-only library
target_include_directories(${COMPILE_ID} PRIVATE "${CMAKE_CURRENT_SOURCE_DIR}/CLI11/include")