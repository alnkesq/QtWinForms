cmake_minimum_required(VERSION 3.16)
project(QtWinFormsNative LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)

# Tell CMake where Qt is installed

# Find the Qt6 components you need
find_package(Qt6 REQUIRED COMPONENTS Core Widgets Gui)

# Build your DLL
add_library(QtWinFormsNative SHARED
    lib.cpp
)

target_compile_options(QtWinFormsNative PRIVATE /Gy)
target_link_options(QtWinFormsNative PRIVATE /OPT:REF /OPT:ICF)

# Link Qt statically
target_link_libraries(QtWinFormsNative
    PRIVATE
        Qt6::Core
        Qt6::Widgets
        Qt6::Gui
)

# Needed for static Qt builds on Windows
if(WIN32)
    # Disable DLL runtime linkage (static build)
    add_definitions(-DQT_STATIC)
endif()