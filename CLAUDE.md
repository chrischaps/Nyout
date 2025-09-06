# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

This is a Unity 6000.1.15f1 game project called "Nyout" - a board game implementation with multiple players, horses, and turn-based movement mechanics.

## Project Structure

**Core Game Logic:**
- `Assets/Scripts/GameManager.cs` - Main game controller with state machine (singleton pattern)
- `Assets/Scripts/Player.cs` - Player controller managing horses and home nodes
- `Assets/Scripts/Horse.cs` - Individual game piece with movement and DOTween animations
- `Assets/Scripts/Node.cs` - Board positions with edge connections and occupancy management
- `Assets/Scripts/Board.cs` - Board setup and management
- `Assets/Scripts/BoardGenerator.cs` - Dynamic board generation logic

**Game Systems:**
- `Assets/Scripts/EventManager.cs` - Event system for game communication
- `Assets/Scripts/InputHandler.cs` - Input processing for player interactions
- `Assets/Scripts/UIManager.cs` - Main UI controller
- `Assets/Scripts/RollUIManager.cs` - Dice rolling interface
- `Assets/Scripts/AudioManager.cs` - Sound management
- `Assets/Scripts/DiceUtils.cs` - Dice rolling utilities
- `Assets/Scripts/GameData.cs` - Game configuration and data

**External Dependencies:**
- DOTween (Demigiant) - Animation system in `Assets/Plugins/Demigiant/DOTween/`
- Unity Input System - Action-based input handling
- Universal Render Pipeline (URP) - Rendering pipeline
- UI Toolkit - Modern UI system with data binding

## Architecture

**Game Flow:**
- State machine driven by `GameManager.CurrentGameState` enum:
  - `WaitingForRoll` → `WaitingForHorseSelection` → `WaitingForNodeSelection` → `MovingHorses` → back to `WaitingForRoll`
  - `GameOver` state for end conditions
- Turn-based system with `GameManager.CurrentPlayer` tracking
- Property binding system using Unity's `INotifyBindablePropertyChanged` for UI updates

**Core Systems:**
- **Board System**: Node-based graph with outgoing edges for movement paths
- **Horse Movement**: Async movement with DOTween animations, collision handling via "kicking out squatters"
- **Player Management**: Each player owns multiple horses starting in home nodes
- **UI Binding**: Property-based data binding between game state and UI elements

## Common Commands

**Unity Editor:**
```bash
# Open project in Unity Editor (ensure Unity 6000.1.15f1 is installed)
# File → Open Project → Select this directory
```

**Build Commands:**
```bash
# Build from Unity Editor:
# File → Build Settings → Build
# Or File → Build Settings → Build And Run
```

**Development:**
- Use Unity Editor's Play mode for testing
- Console window shows debug logs from game state changes
- Scene view for visual debugging of node connections and horse positions
- Inspector for runtime property monitoring on GameManager singleton

**Key Scripts to Understand:**
- Start with `GameManager.cs:18` - main singleton controlling game flow
- `Player.cs:4` - manages horses and home nodes per player
- `Horse.cs:5` - individual game pieces with movement logic
- `Node.cs:4` - board positions with occupancy and edge connections