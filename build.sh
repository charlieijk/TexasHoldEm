#!/bin/bash

# Texas Hold'em Poker - Build Script
# This script builds the C# project for Godot

set -e  # Exit on error

echo "================================"
echo "Texas Hold'em Poker - Build"
echo "================================"
echo ""

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK not found!"
    echo "Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0"
    exit 1
fi

# Display .NET version
echo "Using .NET version:"
dotnet --version
echo ""

# Check if project file exists
if [ ! -f "TexasHoldEm.csproj" ]; then
    echo "Error: Project file 'TexasHoldEm.csproj' not found!"
    echo "Make sure you're running this script from the project directory."
    exit 1
fi

# Clean previous build
echo "Cleaning previous build..."
dotnet clean "TexasHoldEm.csproj" --verbosity quiet
echo ""

# Restore dependencies
echo "Restoring dependencies..."
dotnet restore "TexasHoldEm.csproj"
echo ""

# Build the project
echo "Building project..."
dotnet build "TexasHoldEm.csproj" --configuration Debug
echo ""

# Check build status
if [ $? -eq 0 ]; then
    echo "================================"
    echo "Build completed successfully!"
    echo "================================"
    echo ""
    echo "Next steps:"
    echo "  1. Open Godot 4.5 (.NET version)"
    echo "  2. Import this project"
    echo "  3. Press F5 to run"
    echo ""
    echo "Or run from command line:"
    echo "  godot --path . res://node_3d.tscn"
else
    echo "================================"
    echo "Build failed!"
    echo "================================"
    echo "Check the error messages above."
    exit 1
fi
