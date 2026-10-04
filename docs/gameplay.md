# Gameplay

The player-facing rules: controls, the objective, the stacking rule, throwing, and the three levels.

## Why
These are the rules the code exists to enforce and the tuning the repo promises not to change.
Any change to the scripts or scene values below changes the game players see on itch.io.

## How it works
**Objective.** Get every loose animal into the pen for its species before the timer runs out.
Penning them all loads the win screen; running out of time loads game over.
Animals wander on their own and bolt when the bear gets close, so cornering them is half the problem.

**Controls**

| Input | Action |
| --- | --- |
| `W` `A` `S` `D` / arrow keys | Move the bear |
| Right click | Pick up an animal in range |
| Left click | Throw the bottom animal toward the cursor |

**Stacking rule.** The bear carries up to four animals, stacked bottom-first.
An animal can be added only if it is no heavier than everything already carried, so a chicken goes on a cow but not the reverse.
A refused pickup plays an error sound.

| Animal | Weight | Speed |
| --- | --- | --- |
| Chicken | 1 | 50 |
| Hog | 2 | 35 |
| Pig | 3 | 30 |
| Cow | 4 | 25 |

Light animals are fast and hard to catch but stack freely; cows are slow but must go on the bottom.

**Throwing.** A throw flies a fixed arc to exactly where the player clicked, cannot collide mid-flight, and the animal resumes wandering on landing.
Landing it in the matching pen counts it as wrangled.

**Levels**

| Level | Scene | Time limit |
| --- | --- | --- |
| 1 | `BiggerMap` | 120s |
| 2 | `BiggerMap 2` | 90s |
| 3 | `BiggerMap 3` | 90s |

Each level scatters animals across four spawn zones, so the herd differs every run; later levels spawn more animals with less time.

## Key files
- `Assets/Prefabs/*_Prefab.prefab` - per-species weight and speed
- `Assets/Scenes/Levels/*.unity` - per-level timer and spawn counts
- `Assets/Scripts/PlayerScripts/PickUp.cs` - controls and stacking rule

## Decisions and gotchas
- Tuning lives in serialized prefab and scene values, not code; a diff to a `.prefab` or `.unity` file can be a gameplay change.
- Throws use a fixed arc rather than physics so the landing spot is exact, which makes pen shots fair.

## Related
- [Carry and throw](carry-and-throw.md)
- [Level flow](level-flow.md)
- [Animal AI](animal-ai.md)
