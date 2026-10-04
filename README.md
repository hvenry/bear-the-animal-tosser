# Bear the Animal Tosser

A 2D top-down Unity arcade game where a bear wrangles loose livestock into pens, built for Queen's University CISC 226 (Winter 2023) by [Barkev Kasparian](https://github.com/BarkevK), [Rowan Mohammed](https://github.com/Rom0) and [Henry Vendittelli](https://github.com/hvenry).

**Play it in the browser:** https://barkevk.itch.io/bear-the-animal-tosser

## Features

- Grab animals and stack up to four in the bear's arms
- Weight-based stacking: heavy animals must go on the bottom
- Animals roam on their own and flee when you get close
- Throws fly a fixed arc to exactly where you click
- Three timed levels with a random herd every run

## Quick start

```bash
git clone https://github.com/hvenry/bear-the-animal-tosser.git
```

1. Install the Unity editor version in `ProjectSettings/ProjectVersion.txt` (or newer) through Unity Hub, then **Projects > Add** the folder.
2. Open `Assets/Scenes/Main.unity` and press **Play**.

## Docs

- [Setup](docs/setup.md) - installing the editor per platform, running, building
- [Gameplay](docs/gameplay.md) - controls, stacking rule, levels
- [Animal AI](docs/animal-ai.md) - the animal state machine
- [Carry and throw](docs/carry-and-throw.md) - pickup, stacking and the arc throw
- [Level flow](docs/level-flow.md) - spawning, scoring, timer, scenes
