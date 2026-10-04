# Bear the Animal Tosser

A 2D top-down Unity arcade game: you are a bear who grabs loose livestock, stacks them in your arms, and hurls them into the right pen before the clock runs out.

**Play it in the browser:** https://barkevk.itch.io/bear-the-animal-tosser

## Why

Built as a three-person term project for CISC 226 (Game Design) at Queen's University, Winter 2023, by
[Barkev Kasparian](https://github.com/BarkevK), [Rowan Mohammed](https://github.com/Rom0) and [Henry Vendittelli](https://github.com/hvenry).
The hook is a weight-based stacking rule: animals flee, light ones are fast, and heavy ones must go on the bottom of the stack, so every run is a small routing puzzle under a timer.
This repo is a cleaned-up, refactored version of the coursework repo, migrated to Unity 6, with gameplay unchanged from the itch.io release.

## Quick start

```bash
git clone https://github.com/hvenry/bear-the-animal-tosser.git
```

1. Install the editor version in `ProjectSettings/ProjectVersion.txt` (or newer) through Unity Hub, then **Projects > Add** the folder.
2. Open `Assets/Scenes/Main.unity` and press **Play**.

Controls: `WASD`/arrows to move, right click to pick up, left click to throw.
Per-platform install notes (including Arch and Wayland) and build steps are in [docs/setup.md](docs/setup.md).

## Docs

- [Setup](docs/setup.md) - installing the editor, running, building
- [Gameplay](docs/gameplay.md) - controls, stacking rule, levels
- [Animal AI](docs/animal-ai.md) - the animal state machine
- [Level flow](docs/level-flow.md) - spawning, scoring, timer, scenes
- [AGENTS.md](AGENTS.md) - repo conventions and the full docs index

## Status

Archived: preserved for portfolio purposes, with no new gameplay planned.
Changes since the coursework version are in [CHANGELOG.md](CHANGELOG.md).
Released under the [MIT License](LICENSE), © 2023 the three contributors above.
Tilemaps are authored in [Tiled](https://www.mapeditor.org/) and imported with [SuperTiled2Unity](https://github.com/Seanba/SuperTiled2Unity) by Sean Barton, vendored under `Assets/SuperTiled2Unity/` under its own terms; see [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).
