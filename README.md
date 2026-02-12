# Match-3 Puzzle Game

A fully functional match-3 puzzle game built with Unity and C# as part of my game development portfolio.

## Features

### Core Mechanics
- 8x8 game board with randomized tile generation
- Drag-to-swap system with adjacent tile validation
- Match detection for 3+ matching tiles horizontally or vertically
- Gravity system with automatic tile falling
- Cascade combo system with chain reactions

### Game Systems
- Score system with real-time UI display
- Move counter with limited moves per game
- Game over screen with final score display
- Restart functionality

## Technical Implementation

### Object-Oriented Architecture

The project follows clean OOP principles with clear separation of concerns:

**Core Classes:**
- `GameBoard` - Manages the tile grid and board state
- `Tile` - Individual tile data and behavior
- `InputManager` - Handles player input and tile selection
- `TileSwapper` - Manages tile swapping logic and validation
- `MatchChecker` - Detects matching tile groups
- `BoardRefiller` - Handles gravity, tile spawning, and cascades
- `ScoreManager` - Tracks and displays player score
- `MoveManager` - Manages move counter and game over conditions
- `GameManager` - Controls game flow and UI states

**Design Patterns:**
- Singleton pattern for manager classes
- Component-based architecture
- Strategy pattern for swappable behaviors

### Technologies
- Unity 6
- C#
- Coroutines for asynchronous operations
- 2D arrays for grid management
- Enumerations for type-safe tile system

## Project Structure
```
Assets/
├── Scripts/
│   ├── Board/
│   │   └── GameBoard.cs
│   ├── Tiles/
│   │   └── Tile.cs
│   ├── Enums/
│   │   └── TileType.cs
│   └── Managers/
│       ├── InputManager.cs
│       ├── TileSwapper.cs
│       ├── MatchChecker.cs
│       ├── BoardRefiller.cs
│       ├── ScoreManager.cs
│       ├── MoveManager.cs
│       └── GameManager.cs
├── Prefabs/
│   └── (Tile prefabs)
├── Scenes/
│   └── SampleScene.unity
└── Sprites/
    └── (Tile graphics)
```

## Purpose

This project demonstrates:
- Clean, modular code architecture
- Understanding of object-oriented programming principles
- Game development skills and Unity proficiency
- Ability to implement complete game systems

Built as part of my application for software engineering positions in game development.

## Installation and Setup

### Prerequisites
- Unity 6 or later
- Git

### Steps
1. Clone the repository:
```bash
   git clone https://github.com/Ali-BenOtman/Match3-Puzzle-Game.git
```

2. Open Unity Hub and add the project folder

3. Open the project in Unity

4. Navigate to Assets/Scenes/SampleScene

5. Press Play to run the game

### How to Play
1. Click and drag a tile to an adjacent position (up, down, left, right)
2. Match 3 or more tiles of the same color
3. Earn points for each match
4. Create cascades for additional scoring opportunities
5. Game ends when moves reach zero

## Development Roadmap

### Completed
- Core match-3 mechanics with swap, match, and cascade systems
- Score tracking and display
- Move counter system
- Game over functionality with restart option

### Planned Features
- Level system with progressive difficulty
- Power-ups and special tiles
- Animation system for tile movements
- Particle effects for matches
- Audio integration
- Roguelike elements with meta-progression
- Star rating system based on performance

## Key Concepts Demonstrated

This project showcases proficiency in:
- Unity game development
- C# programming and object-oriented design
- Game architecture and design patterns
- State management
- User interface implementation
- Version control with Git

