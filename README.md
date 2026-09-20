# Bear the Animal Tosser

A 2D top-down arcade game built in Unity. You are a bear with a farm to clear: grab
the livestock, stack them in your arms, and hurl them into the right pen before the
clock runs out.

**Play it in the browser:** https://barkevk.itch.io/bear-the-animal-tosser

Originally built as a term project for **CISC 226 (Game Design)** at Queen's
University, Winter 2023.

## Authors

This game was a three-person project:

| Contributor | GitHub |
| --- | --- |
| Barkev Kasparian | [@BarkevK](https://github.com/BarkevK) |
| Rowan Mohammed | [@Rom0](https://github.com/Rom0) |
| Henry Vendittelli | [@hvenry](https://github.com/hvenry) |

This repository is a cleaned-up, refactored version of the original coursework
repo, preserved for archival and portfolio purposes. The gameplay, levels, art and
audio are unchanged from the version published on itch.io.

## Gameplay

You play a bear dropped into a farm full of loose animals. Each animal wanders on
its own and bolts when you get too close, so cornering them is half the problem.
Every species has its own pen, and only the matching pen counts.

### Objective

Get **every** loose animal into a pen before the timer expires. Penning them all
wins the level; running out of time ends the run.

### Controls

| Input | Action |
| --- | --- |
| `W` `A` `S` `D` / Arrow keys | Move the bear |
| **Right click** | Pick up the nearest animal in range |
| **Left click** | Throw the bottom animal toward the cursor |

### The stacking rule

The bear carries up to **four** animals at once, and they stack bottom-first. An
animal can only be added to the stack if it is **no heavier than everything already
being carried** — so you can pile a chicken onto a cow, but not a cow onto a
chicken. Trying it plays an error sound and the pickup is refused.

This is the core tactical constraint: heavy animals first, light ones last.

| Animal | Weight | Speed |
| --- | --- | --- |
| Chicken | 1 | 50 |
| Hog | 2 | 35 |
| Pig | 3 | 30 |
| Cow | 4 | 25 |

Lighter animals are faster and harder to catch, but stack freely. Cows are slow and
easy to grab, but must go on the bottom.

### Throwing

A throw follows a fixed parabolic arc to wherever you clicked, rather than using
physics — so the landing spot is exactly where you aimed. The animal cannot collide
mid-flight, then returns to wandering on landing. Land it inside the matching pen
and it is counted as wrangled.

### Levels

| Level | Scene | Time limit |
| --- | --- | --- |
| 1 | `BiggerMap` | 120s |
| 2 | `BiggerMap 2` | 90s |
| 3 | `BiggerMap 3` | 90s |

Each level scatters animals across four spawn zones, so the exact herd differs every
run. Later levels spawn more animals and give you less time.

## Requirements

- **Unity 2021.3.22f1** (LTS). Other 2021.3.x patch releases will almost certainly
  work; major version jumps may prompt an API upgrade.
- No external dependencies beyond the Unity packages listed in
  `Packages/manifest.json` — they are restored automatically on first open.

[SuperTiled2Unity](https://github.com/Seanba/SuperTiled2Unity) is vendored under
`Assets/SuperTiled2Unity/` and imports the Tiled tilemaps; nothing to install. See
[THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md) for its licensing.

## Setup

The reliable path on every platform is Unity Hub, which installs the exact editor
version this project expects.

### Arch Linux

Unity Hub is on the AUR:

```bash
# with an AUR helper
yay -S unityhub

# or manually
git clone https://aur.archlinux.org/unityhub.git
cd unityhub && makepkg -si
```

Then clone and open:

```bash
git clone https://github.com/hvenry/bear-the-animal-tosser.git
cd bear-the-animal-tosser
unityhub
```

In Unity Hub: **Installs → Install Editor → Archive**, pick `2021.3.22f1`, then
**Projects → Add** and select the cloned folder.

> If the editor window renders blank or the Hub will not launch under Wayland, start
> it with `unityhub --no-sandbox`, or run under XWayland (`GDK_BACKEND=x11 unityhub`).

### Windows

1. Install [Unity Hub](https://unity.com/download).
2. Install editor `2021.3.22f1` via **Installs → Install Editor → Archive**.
3. Clone the repo and add the folder through **Projects → Add**.

```powershell
git clone https://github.com/hvenry/bear-the-animal-tosser.git
```

### macOS

```bash
brew install --cask unity-hub
git clone https://github.com/hvenry/bear-the-animal-tosser.git
```

Install editor `2021.3.22f1` through **Installs → Install Editor → Archive** (choose
the Apple Silicon or Intel build to match your machine), then add the cloned folder
via **Projects → Add**.

### Direct editor download

If you would rather skip the Hub, the exact version is available here:

```
https://unity.com/releases/editor/whats-new/2021.3.22
```

## Running the game

1. Open the project in Unity `2021.3.22f1`. The first import takes a few minutes
   while the `Library/` cache is built — this is expected and only happens once.
2. Open `Assets/Scenes/Main.unity` (the main menu).
3. Press **Play**, then use the menu to reach level select.

To jump straight into gameplay, open `Assets/Scenes/Levels/BiggerMap.unity` and
press Play. Note that starting from a level scene skips menu setup, which is fine
for testing but not how a player reaches it.

### Building

`File → Build Settings` already lists all eight scenes in the correct order. Select
a target and build. WebGL is what itch.io hosts; desktop standalone builds also work.

## Project layout

```
Assets/
├── Scenes/
│   ├── Main.unity              Main menu
│   ├── Win.unity               Win screen
│   ├── GameOver.unity          Loss screen
│   ├── Menu/                   Level select, options
│   └── Levels/                 The three playable levels
├── Scripts/
│   ├── AnimalScripts/          Animal AI state machine
│   ├── PlayerScripts/          Movement, pickup/throw, spawning, arc math
│   ├── PenScripts/             Pen collision + scoring
│   └── GUIScripts/             Menu navigation, state machine base
├── winCondition.cs             Per-level win/loss rules and HUD
├── EventManager.cs             Pen → win-tracker event channel
├── GameOverScreen.cs           Game over buttons
├── Prefabs/                    One prefab per animal species
├── Sprites/ Animation/         Bear and animal art
├── Tiles/ Palettes/            Tilemap data
├── Music/ Fonts/ Images/       Audio and UI assets
└── SuperTiled2Unity/           Vendored Tiled importer (third party)
```

## How it works

### Animal AI

Each animal runs a small state machine (`MovementSM`, extending `StateMachine`):

```
        ┌──── player near ────┐
        ▼                     │
     Fleeing ──── far ──►  Idle  ◄──── timer ────►  Roaming
                             │                         │
                             └──── picked up ──────────┘
                                       ▼
                                     Held ──── thrown ──►  Thrown
                                                             │
                                                     lands   ▼
                                                            Idle
```

- **Idle** — stands still, watching for the player or its roam timer.
- **Roaming** — wanders in one random heading until spooked or the timer elapses.
- **Fleeing** — runs directly away from the player, re-checking every 2s whether it
  has escaped.
- **Held** — carried by the bear; collision disabled so it does not shove the player.
- **Thrown** — flies a fixed parabola to the cursor, then drops back to Idle.

States share `AnimalBaseState`, which caches the player transform and holds the
distance checks, so the per-frame work stays minimal.

### Scoring

When an animal touches a pen matching its species, `PenCollision` stops the two
colliding (so it settles inside) and raises `EventManager.onSafe` with that animal's
id. `winCondition` assigned those ids at level start and counts each one only once;
when the count reaches the total, the win scene loads.

## Credits

- Code, art, design and audio by the three contributors listed above.
- Tilemaps authored in [Tiled](https://www.mapeditor.org/) and imported with
  [SuperTiled2Unity](https://github.com/Seanba/SuperTiled2Unity) by Sean Barton,
  vendored under `Assets/SuperTiled2Unity/`. It bundles further third-party code
  under its own terms — see [THIRD-PARTY-NOTICES.md](THIRD-PARTY-NOTICES.md).
