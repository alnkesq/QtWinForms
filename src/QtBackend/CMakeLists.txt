cmake_minimum_required(VERSION 3.16)
project(QtBackend LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

find_package(Qt6 REQUIRED COMPONENTS Widgets)

add_library(QtBackend SHARED lib.cpp)

target_link_libraries(QtBackend PRIVATE Qt6::Widgets)

if(WIN32)
    target_compile_definitions(QtBackend PRIVATE QTBACKEND_EXPORT)
endif()
