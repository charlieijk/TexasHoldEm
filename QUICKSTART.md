# Quick Start Guide

Get up and running with Texas Hold'em Poker in minutes!

## Prerequisites Check

```bash
# Check .NET version (need 8.0+)
dotnet --version

# If not installed, download from:
# https://dotnet.microsoft.com/download/dotnet/8.0
```

## Setup Steps

### 1. Download Godot 4.5 (.NET)

**Important:** You need the **.NET version** of Godot, not the standard version!

- Download: https://godotengine.org/download/
- Look for "Godot Engine - .NET"
- Version: 4.5 or later

### 2. Build the Project

```bash
# macOS/Linux
./build.sh

# Windows
build.bat
```

### 3. Open in Godot

1. Launch Godot 4.5 (.NET version)
2. Click **"Import"**
3. Select this project folder
4. Click **"Import & Edit"**

### 4. Run the Game

In Godot Editor:
- Press **F5** or click the **Play** button (▶)
- Or click **Build** first, then **Play**

## Troubleshooting

### Build fails with "Godot.NET.Sdk not found"
- ✅ Make sure you downloaded Godot 4.5 **.NET version**
- ✅ Verify .NET 8.0 SDK is installed: `dotnet --version`

### "dotnet: command not found"
- Install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
- Restart your terminal after installation

### Game doesn't start in Godot
- Click the **Build** button (top-right) in Godot Editor
- Check Output panel for any C# compilation errors
- Ensure all `.cs` files have no syntax errors

### Cards not displaying
- Make sure `Playing Cards/` folder exists with card assets
- Check Godot's Output panel for asset loading errors

## Project Info

- **Language:** C# with .NET 8.0
- **Engine:** Godot 4.5
- **Platform:** Cross-platform (Windows, macOS, Linux)
- **Players:** 1 human vs 1 AI

## Next Steps

- Read the full [README.md](README.md) for detailed documentation
- Check the code files to understand the game architecture
- Try modifying game parameters (chip counts, blind amounts)
- Implement your own AI strategy!

## Quick Commands Reference

```bash
# Build
./build.sh              # macOS/Linux
build.bat               # Windows

# Clean build artifacts
dotnet clean TexasHoldEm.csproj

# Open in IDE
code .                  # VS Code
open TexasHoldEm.sln   # macOS default IDE
start TexasHoldEm.sln  # Windows default IDE
```

## Game Controls

Once the game is running:
- Click **Fold** to forfeit your hand
- Click **Check** to pass (when no bet to match)
- Click **Call** to match the current bet
- Click **Raise** to increase the bet

Your cards are shown at the bottom, AI's cards at the top.

Enjoy the game! 🎰
