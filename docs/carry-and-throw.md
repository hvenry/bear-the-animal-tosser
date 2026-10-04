# Carry and throw

How the bear moves, picks up a stack of up to four animals, and throws them along a fixed arc.

## Why
The stacking rule (heavy animals at the bottom) is the core tactical constraint of the game.
Throwing on a fixed arc, instead of physics, makes the landing spot exactly where the player aimed, which is what makes pen shots fair.

## How it works
**Movement.** `PlayerMovement` reads raw WASD/arrow axes in `Update`, normalizes them so diagonals are not faster, and sets `Rigidbody2D.velocity` in `FixedUpdate`.
It also feeds `Horizontal`, `Vertical` and `Speed` to the bear's animator (`Assets/Animation/Player.controller`).

**Pickup (right click).** `PickUp.TryPickUp`:

1. `Physics2D.OverlapCircle` around the bear on the `PickUp` layer mask.
2. Rejects with the error sound if all four slots are full or `CanStack` fails.
3. Parents the animal to the bear, snaps it to the first free hold spot, disables its rigidbody simulation, and moves it to `Held`.

**Stacking rule.** `CanStack` allows a candidate only if it is no heavier than every animal already carried.
Weights: chicken 1, hog 2, pig 3, cow 4 (set on the prefabs).

**Throw (left click).** `PickUp.TryThrow` releases slot 0 (the bottom of the stack), re-enables simulation, and moves it to `Thrown`.
`ShuffleStackDown` then shifts every remaining animal down one slot.

**Flight.** `Thrown.Enter` records the bear's position and the cursor's world position; `UpdateLogic` evaluates `MathParabola.Parabola` over a fixed 2s with a 2-unit arc height, then snaps to the landing point and returns to Idle.

## Key files

- `Assets/Scripts/PlayerScripts/PlayerMovement.cs` - input, velocity, animator parameters
- `Assets/Scripts/PlayerScripts/PickUp.cs` - carry slots, stacking rule, pickup and throw
- `Assets/Scripts/PlayerScripts/MathParabola.cs` - arc evaluation
- `Assets/Scripts/AnimalScripts/Held.cs` - carried state
- `Assets/Scripts/AnimalScripts/Thrown.cs` - flight state

## Decisions and gotchas

- The four hold spots are separate serialized `Transform` fields (`holdSpot`..`holdSpot4`), not an array, so existing scene wiring survives; `Start` packs them into `_spots`.
- The thrown animal is always the bottom one, so after a throw the next-heaviest becomes the bottom; the weight rule still holds.
- `OverlapCircle` returns an arbitrary overlapping collider, not strictly the nearest.
- Colliders are disabled while held and in flight so the animal cannot shove the bear; they come back on state exit.
- The landing point's `z` is pushed 5 units behind the start so the animal draws over the tilemap.
- Flight time is fixed, so short throws are slow and long throws are fast.
- `Rigidbody2D.velocity` is obsolete in current Unity (`linearVelocity` replaces it); it still compiles with a warning.

## Related

- [Animal AI](animal-ai.md)
- [Level flow](level-flow.md)
