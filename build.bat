@echo off
REM Texas Hold'em Poker - Build Script (Windows)
REM This script builds the C# project for Godot

echo ================================
echo Texas Hold'em Poker - Build
echo ================================
echo.

REM Check if .NET is installed
where dotnet >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo Error: .NET SDK not found!
    echo Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
    exit /b 1
)

REM Display .NET version
echo Using .NET version:
dotnet --version
echo.

REM Check if project file exists
if not exist "TexasHoldEm.csproj" (
    echo Error: Project file 'TexasHoldEm.csproj' not found!
    echo Make sure you're running this script from the project directory.
    exit /b 1
)

REM Clean previous build
echo Cleaning previous build...
dotnet clean "TexasHoldEm.csproj" --verbosity quiet
echo.

REM Restore dependencies
echo Restoring dependencies...
dotnet restore "TexasHoldEm.csproj"
echo.

REM Build the project
echo Building project...
dotnet build "TexasHoldEm.csproj" --configuration Debug
echo.

REM Check build status
if %ERRORLEVEL% EQU 0 (
    echo ================================
    echo Build completed successfully!
    echo ================================
    echo.
    echo Next steps:
    echo   1. Open Godot 4.5 ^(.NET version^)
    echo   2. Import this project
    echo   3. Press F5 to run
    echo.
) else (
    echo ================================
    echo Build failed!
    echo ================================
    echo Check the error messages above.
    exit /b 1
)

pause
