# Texas Hold'em Poker

A Texas Hold'em poker game built with Godot 4.5 and C#/.NET 8.0, optimized for fullscreen 1920x1080 gameplay.

## Installation (Quick)

**Want to play right now?**

1. Download [Godot 4.5 .NET](https://godotengine.org/download/) and [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Clone this repo: `git clone https://github.com/charlieijk/TexasHoldEm.git`
3. Open Godot → Import → Select `project.godot` from the TexasHoldEm folder
4. Click Build, then press F5 to play!

See [Quick Start](#quick-start) below for detailed instructions.

## Features

- Full Texas Hold'em poker gameplay with betting rounds (Pre-flop, Flop, Turn, River)
- Player vs AI opponent
- Chip management and pot tracking
- Card animations and visual feedback
- Simple AI opponent with basic decision-making
- Clean UI with action buttons (Fold, Check, Call, Raise)
- **Fullscreen 1920x1080 display** with scaled graphics and UI
- Optimized for large screen viewing

## Game Components

- **PokerGameManager**: Core game logic and state management
- **Player**: Player logic with chip management and card handling
- **Deck**: Card deck with shuffle and deal functionality
- **Card**: Individual playing card with flip animations
- **UIManager**: User interface and button management
- **PokerTable**: Visual poker table background that scales to fullscreen

## Requirements

### Software
- [Godot 4.5 or later](https://godotengine.org/download/) (with .NET support)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- macOS, Windows, or Linux

### Godot .NET Version
Make sure to download **Godot 4.5 .NET** version (not the standard version), which includes C# support.

## Quick Start

### Option 1: Download and Play (For Players)

1. **Download the Game**
   ```bash
   # Clone the repository
   git clone https://github.com/charlieijk/TexasHoldEm.git
   cd TexasHoldEm
   ```

2. **Install Godot**
   - Download [Godot 4.5 .NET](https://godotengine.org/download/) (required for C# support)
   - Install [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

3. **Run the Game**
   - Launch Godot 4.5 (.NET version)
   - Click **"Import"**
   - Browse to the TexasHoldEm folder and select `project.godot`
   - Click **"Import & Edit"**
   - Click the **"Build"** button (top-right corner)
   - Press **F5** or click the **Play ▶** button
   - Enjoy fullscreen poker!

### Option 2: Using Godot Editor (For Development)

1. **Install Prerequisites**
   ```bash
   # Check if .NET 8.0 is installed
   dotnet --version
   # Should show 8.0.x or later
   ```

2. **Open Project**
   - Launch Godot 4.5 (.NET version)
   - Click "Import"
   - Navigate to the project folder
   - Select `project.godot`
   - Click "Import & Edit"

3. **Build and Run**
   - In Godot Editor, click "Build" button (top-right corner)
   - Press F5 or click the "Play" button to run the game
   - Game will launch in **fullscreen mode**

### Option 3: Command Line Build

1. **Build the project**
   ```bash
   # Navigate to project directory
   cd /path/to/TexasHoldEm

   # Use the build script
   ./build.sh          # macOS/Linux
   build.bat           # Windows

   # Or manually:
   dotnet build TexasHoldEm.csproj
   ```

2. **Run with Godot**
   ```bash
   # Run from project directory
   godot --path . res://node_3d.tscn

   # Or use full path to Godot
   /Applications/Godot.app/Contents/MacOS/Godot --path . res://node_3d.tscn  # macOS
   ```

## Display Settings

The game is configured for optimal fullscreen viewing:
- **Resolution:** 1920x1080 (Full HD)
- **Mode:** Fullscreen (borderless window)
- **Stretch:** Canvas items with expand aspect
- **Toggle Fullscreen:** Press F11 or Alt+Enter during gameplay

## Project Structure

```
TexasHoldEm/
├── Card.cs                    # Playing card class with visuals
├── Deck.cs                    # Card deck management
├── Game.cs                    # Main game entry point
├── Player.cs                  # Player state and actions
├── PokerGameManager.cs        # Core game logic and flow
├── PokerTable.cs              # Visual poker table (fullscreen)
├── UIManager.cs               # UI and button management (scaled)
├── node_3d.tscn              # Main scene file
├── project.godot             # Godot project configuration
├── TexasHoldEm.csproj        # C# project file
├── TexasHoldEm.sln           # Solution file
├── build.sh                   # Build script for macOS/Linux
├── build.bat                  # Build script for Windows
├── README.md                  # This file
├── QUICKSTART.md              # Quick setup guide
└── Playing Cards/            # Card asset folder
```

## How to Play

1. The game starts in **fullscreen mode** with you (bottom) and an AI opponent (top)
2. Each player starts with 1000 chips
3. Blinds are automatically posted (Small: 10, Big: 20)
4. Two hole cards are dealt to each player
5. Betting rounds:
   - **Pre-flop**: After receiving hole cards
   - **Flop**: After 3 community cards are revealed (center of table)
   - **Turn**: After 4th community card
   - **River**: After 5th community card
   - **Showdown**: Best hand wins
6. Use action buttons (bottom-right corner):
   - **Fold**: Give up your hand
   - **Check**: Pass (only if no bet to match)
   - **Call**: Match the current bet
   - **Raise**: Increase the bet

## UI Layout

- **Top Left**: AI opponent's chip count
- **Top Center**: POT and Current Bet information
- **Center**: Community cards (flop, turn, river)
- **Bottom Left**: Your chip count and game state
- **Bottom Right**: Action buttons (Fold, Check, Call, Raise)
- **Cards**: Your cards at bottom, AI cards at top

## Development

### Building from Source

```bash
# Use the provided build scripts (recommended)
./build.sh          # macOS/Linux
build.bat           # Windows

# Or manually:
# Restore dependencies
dotnet restore TexasHoldEm.csproj

# Build the project
dotnet build TexasHoldEm.csproj

# Clean build artifacts
dotnet clean TexasHoldEm.csproj
```

### Opening in IDE

**Visual Studio / VS Code:**
```bash
# Open the solution file
open TexasHoldEm.sln    # macOS
start TexasHoldEm.sln   # Windows
code .                   # VS Code
```

**Rider:**
- File → Open → Select `TexasHoldEm.sln`

### Adding New Features

1. Create new C# scripts in the project root
2. Scripts should inherit from Godot's `Node` or `Node2D`
3. Use `partial class` modifier for Godot integration
4. Rebuild the project after adding new scripts
5. For UI changes, adjust positions based on 1920x1080 resolution

### Screen Resolution Notes

The game is optimized for 1920x1080 fullscreen:
- All positions and sizes are scaled for this resolution
- To adjust for different resolutions, modify:
  - `project.godot` - Display settings
  - `PokerTable.cs` - Background dimensions
  - `Game.cs` - Player positions
  - `PokerGameManager.cs` - Community card positions
  - `UIManager.cs` - UI element positions and sizes

## Known Limitations

- Hand evaluation not yet implemented (showdown doesn't determine winner)
- AI uses simple random decision-making
- Only supports 2 players currently
- No save/load game functionality
- No tournament or multi-round chip tracking
- Optimized for 1920x1080 displays (other resolutions may require adjustments)

## Troubleshooting

### Build Errors

**"Godot.NET.Sdk not found"**
- Ensure you're using Godot 4.5 .NET version
- Verify .NET 8.0 SDK is installed: `dotnet --version`

**"Cannot find Godot executable"**
- Add Godot to your system PATH
- Or use full path: `/path/to/Godot.app/Contents/MacOS/Godot --path .`

### Runtime Issues

**Game doesn't start**
- Check the Output panel in Godot Editor for errors
- Ensure all C# files compiled successfully (Build button should show green)
- Close and reopen Godot if changes don't apply

**Cards not displaying**
- Verify `Playing Cards/` folder contains card assets
- Check console for asset loading errors

**UI elements off-screen**
- Ensure you're running at 1920x1080 resolution
- Check display mode is set to fullscreen in `project.godot`

**Game appears too small**
- Close Godot completely and reopen the project
- Click Build button in Godot Editor
- Verify `project.godot` has fullscreen settings enabled

## Contributing

This is a college project, but suggestions and improvements are welcome!

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly in Godot Editor
5. Submit a pull request

## License

This project is for educational purposes.

## Credits

- Built with [Godot Engine](https://godotengine.org/)
- Developed using C# and .NET 8.0
- Playing card assets in `Playing Cards/` folder

## Future Enhancements

- [ ] Implement hand evaluation (Royal Flush, Straight, etc.)
- [ ] Improve AI with probability-based decisions
- [ ] Add support for 3-6 players
- [ ] Tournament mode with blind increases
- [ ] Save/load game state
- [ ] Statistics tracking
- [ ] Sound effects and music
- [ ] Better animations
- [ ] Multi-round chip persistence
- [ ] Responsive scaling for different resolutions
- [ ] Settings menu for resolution/fullscreen toggle
