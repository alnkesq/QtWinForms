#!/usr/bin/env pwsh
param([switch] $Run)

$ErrorActionPreference = "Stop"

Write-Host "Building QtWinForms..." -ForegroundColor Cyan

Write-Host "`nBuilding QtWinFormsNative (C++)..." -ForegroundColor Yellow
Push-Location src/QtBackend/build
try {
    cmake --build . --config Release
    if ($LASTEXITCODE -ne 0) {
        Write-Host "ERROR: QtWinFormsNative build failed!" -ForegroundColor Red
        exit 1
    }
    Write-Host "✓ QtWinFormsNative built successfully" -ForegroundColor Green
} finally {
    Pop-Location
}

# Build C# projects
if($Run){
    dotnet run --project src/TestApp
}else{
    Write-Host "`nBuilding C# projects..." -ForegroundColor Yellow
    dotnet build src/TestApp
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