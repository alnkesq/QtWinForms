#!/usr/bin/env pwsh
param([switch] $Run)

$ErrorActionPreference = "Stop"

Write-Host "Building QtWinForms..." -ForegroundColor Cyan

# Build C++ QtBackend
Write-Host "`n[1/3] Building QtBackend (C++)..." -ForegroundColor Yellow
Push-Location src/QtBackend/build
try {
    cmake --build . --config Release
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: QtBackend build failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "✓ QtBackend built successfully" -ForegroundColor Green
} finally {
    Pop-Location
}

# Copy QtBackend.dll to TestApp output
Write-Host "`n[2/3] Copying QtBackend.dll..." -ForegroundColor Yellow
Copy-Item src/QtBackend/build/Release/QtBackend.dll src/TestApp/bin/Debug/net10.0/ -Force
Write-Host "✓ QtBackend.dll copied" -ForegroundColor Green

# Build C# projects
if($Run){
    dotnet run --project src/TestApp
}else{
    Write-Host "`n[3/3] Building C# projects..." -ForegroundColor Yellow
    dotnet build
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: C# build failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "✓ C# projects built successfully" -ForegroundColor Green

    Write-Host "`n✓ Build completed successfully!" -ForegroundColor Green
    Write-Host "`nTo run the test app:" -ForegroundColor Cyan
    Write-Host "  cd src/TestApp" -ForegroundColor Gray
    Write-Host "  dotnet run" -ForegroundColor Gray
}