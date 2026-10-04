# Bear the Animal Tosser

2D top-down Unity arcade game: a bear grabs livestock, stacks them, and throws them into matching pens before the clock runs out.
Originally a Queen's CISC 226 term project (Winter 2023), kept as a cleaned-up archival and portfolio repo.
Gameplay, levels, art and audio match the itch.io release; changes are structural unless stated.
Stack: Unity (C#, built-in 2D physics, Tilemap), SuperTiled2Unity (vendored), WebGL for itch.io.

## Commands
```bash
UNITY="/Applications/Unity/Hub/Editor/$(sed -n 's/^m_EditorVersion: //p' ProjectSettings/ProjectVersion.txt)/Unity.app/Contents/MacOS/Unity"  # editor matching ProjectVersion.txt (macOS)
"$UNITY" -projectPath .                                                   # open the project in the editor
"$UNITY" -batchmode -nographics -quit -projectPath . -logFile -            # headless import + compile; non-zero exit on script errors
"$UNITY" -batchmode -nographics -quit -projectPath . -buildOSXUniversalPlayer Builds/mac/Bear.app -logFile -  # standalone macOS build
```
- Play in editor: open `Assets/Scenes/Main.unity` and press Play; open a `Levels/` scene to skip the menus.
- WebGL builds go through File > Build Profiles; there is no build script yet.
- No test suite yet, so there is no single-test command: see `docs/specs/test-suite.md`.

## Repo map
```
Assets/Scripts/AnimalScripts/   animal state machine and states
Assets/Scripts/PlayerScripts/   movement, pickup/throw, arc math, spawner
Assets/Scripts/PenScripts/      pen collision and scoring hook
Assets/Scripts/GUIScripts/      menu navigation, StateMachine base class
Assets/winCondition.cs          per-level timer, win/loss, HUD
Assets/EventManager.cs          pen -> winCondition event channel
Assets/Scenes/                  menus, Win/GameOver, Levels/
Assets/Prefabs/                 one prefab per species (tuning lives here)
Assets/SuperTiled2Unity/        vendored Tiled importer, third party
ProjectSettings/                build scene list, tags, layers, editor version
```

## Conventions
- **Open with the editor in `ProjectSettings/ProjectVersion.txt` or newer.** An older editor attempts a downgrade and corrupts serialized assets.
- **Commit every `.meta` file with its asset, and move or rename assets inside the editor.** Scenes and prefabs reference scripts and assets by the GUID in the `.meta`; losing it silently breaks those links.
- **Do not rename scenes without updating `MainMenu`, `GameOverScreen` and `winCondition`.** They load scenes by name string, so a rename only fails at runtime.
- **Species tags (`Cow`, `Pig`, `Hog`, `Chicken`) and the `PickUp` layer are load-bearing.** `winCondition` builds its roster by tag, pens match by tag, and pickup only sees the `PickUp` layer.
- **Do not change level tuning (spawn counts, timers, speed, weight) in structural work.** The repo promises gameplay identical to the itch.io release; tuning changes are a deliberate, documented decision.
- **Never edit `Assets/SuperTiled2Unity/`.** It is redistributed unmodified under its own licenses (see `THIRD-PARTY-NOTICES.md`).
- **Raise pen events through `EventManager.RaiseSafe`, and unsubscribe in `OnDestroy`.** The static delegate outlives scenes; raw invokes can null-throw and stale handlers fire on destroyed objects.
- **Record user-visible changes in `CHANGELOG.md`.** It is the history of what changed versus the original coursework.

## Docs
- Before setting up the editor or changing build steps, read `docs/setup.md`.
- Before changing tuning or anything a player sees, read `docs/gameplay.md`.
- Before changing animal behaviour or adding a state, read `docs/animal-ai.md`.
- Before changing pickup, stacking or throwing, read `docs/carry-and-throw.md`.
- Before changing spawning, scoring, timers, scenes or builds, read `docs/level-flow.md`.
- Before touching vendored code or adding a dependency, read `THIRD-PARTY-NOTICES.md`.

## Planned
- Before adding tests, read `docs/specs/test-suite.md`.
