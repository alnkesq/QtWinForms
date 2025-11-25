## Building QtWinForms

## Windows
```powershell

# Make sure you run from a Visual Studio Command Prompt (x64 Native)
cmd /c '"C:\Program Files\Microsoft Visual Studio\18\Insiders\VC\Auxiliary\Build\vcvars64.bat" && powershell'


git clone https://github.com/alnkesq/QtWinForms
git clone https://github.com/qt/qtbase

$installdir="$pwd\qtbase-install"
cd qtbase
# Building out-of-source seems broken (C:\build\qt-static\include\QtCore/qobjectdefs.h(1): fatal error C1083: Cannot open include file: 'QtCore/': No such file or directory). Such files only contain "#include <QtCore/> // IWYU pragma: export", so use in-source.

cmake -S . -B . `
    -DFEATURE_style_windowsvista=ON `
    -DFEATURE_brotli=OFF `
    -DBUILD_SHARED_LIBS=OFF `
    "-DCMAKE_INSTALL_PREFIX=qt-install-directory" `
    -DCMAKE_BUILD_TYPE=Release `
    -DCMAKE_C_COMPILER=cl `
    -DCMAKE_CXX_COMPILER=cl `
    -DCMAKE_CXX_FLAGS_RELEASE="/Gy" `
    -G "Ninja" 


cmake --build . --config Release --parallel
cmake --install . --prefix $installdir
cd ..\qtwinforms\src\QtBackend

cmake -S . -B build "-DCMAKE_PREFIX_PATH=$installdir"
cmake --build build --config Release --parallel

cd ..\..

# The code above is only needed during the first build.
# Now you can iterate using:
./build.ps1 -Run

```

## Linux
TODO

## macOS
TODO