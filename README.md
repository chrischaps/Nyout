# Nyout

Nyout is yut nori, the Korean race game families play at the new year: four sticks thrown instead of dice, pieces racing around a square board with two diagonals, shortcuts through the middle, captures that send a rival home, and stacks that move together. This repository is the first version of it, a small Unity prototype from 2025 with local play only.

The game has since been rebuilt from scratch in LÖVE for Android, with the board drawn as the star chart Joseon scholars saw in it, bots at three levels, play across phones on the same Wi-Fi, and sound synthesized as it plays. That version is not in this repository.

**Project page:** [chaps.dev/projects/nyout](https://chaps.dev/projects/nyout/) shows the new version, with clips of the stick throw, a capture, a piece coming home and three phones playing over Wi-Fi.

![A whole game of the new Nyout as a long exposure over the star-chart board](https://chaps.dev/projects/nyout/cover.jpg)

## Running it

Needs Unity 6000.1.15f1 (see `ProjectSettings/ProjectVersion.txt`).

1. Open this folder as a project in Unity Hub or the Unity Editor.
2. Open `Assets/Scenes/SampleScene.unity` and press Play.
3. To build, use File > Build Settings > Build (or Build And Run).

## How it's built

- `Assets/Scripts/GameManager.cs`: a singleton state machine that runs each turn, from waiting for a roll, through choosing a piece and a destination, to moving.
- `Board.cs`, `BoardGenerator.cs`, `Node.cs`: the board as a graph of nodes with outgoing edges, which is how the corner shortcuts work.
- `Player.cs`, `Horse.cs`: players and their pieces ("horses", as in the game), moved with DOTween animations; landing on a rival kicks it home.
- UI Toolkit with property binding for the interface, the Input System for input, and URP for rendering.
