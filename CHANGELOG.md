# Changelog

## Unity 6 migration — 2026-09-20

- Upgraded the project from Unity 2021.3.22f1 to Unity 6 (6000.6.2f1). Packages were
  resolved to their Unity 6 versions; `com.unity.textmeshpro` is now folded into
  `com.unity.ugui`.
- Verified after migration: the project compiles with no errors, the Console is
  clean, and all three levels play correctly.
- Ignored the `.slnx` solution file Unity 6 generates.

## Cleanup pass — 2026-09-20

This repository is a cleaned-up extraction of the original CISC 226 coursework repo.
Gameplay, levels, art and audio are unchanged; the work below is structural.

### Repository structure

- Flattened the nested `CISC-226-GAME/CISC-226-GAME/` wrapper so the Unity project
  sits at the repository root.
- Untracked and removed committed editor/user state: `.idea/`, `.vscode/`,
  `UserSettings/` (including a stray `.idea` inside `Assets/`).
- Replaced the `.gitignore` with a current Unity ignore set that also covers
  `UserSettings/`, IDE directories and OS junk.
- Added `.gitattributes` so Unity YAML assets use the YAML merge driver and are not
  line-ending mangled.
- Added `README.md`, `THIRD-PARTY-NOTICES.md` and this changelog.
- Set `productName` to "Bear the Animal Tosser" (was `CISC-226-GAME`).

### Removed dead code and unused assets

Each of the following was verified unreferenced by any scene, prefab or script
before removal:

- `Projectile.cs` — self-documented as "NO LONGER USED"; its logic had been folded
  into the `Thrown` state. Its only host was the orphaned `Animal Prefab`.
- `Animal Prefab.prefab` — superseded by the four per-species prefabs.
- `ParabolaController.cs` — a ~400-line third-party parabola demo, never attached.
- `do_not_destroy.cs`, `AnimalState.cs` — unreferenced.
- `Timer.cs`, `RoamTimerAction.cs`, `IActionInterface.cs` — a self-contained cluster
  that nothing outside itself used; roam/idle cycling runs off `timeSpent` instead.
- `Assets/SuperTiled2Unity/Examples/` — third-party demo content (~644 KB),
  confirmed self-contained.
- Unused sprites: `animal1/2/3`, `Camel_2`, `Panda`, and a duplicate
  `Sprites/animals/icons/` folder.
- Stale serialized fields left behind in scenes and prefabs by the above.

### Bug fixes

- **`winCondition.cs` could not build.** It imported `UnityEditor`, which does not
  exist in a player build — this breaks standalone and WebGL builds.
- **Event handler was never unsubscribed.** `EventManager.onSafe` kept a reference
  to a destroyed `winCondition` across scene loads, so handlers accumulated and
  fired on dead objects. Now removed in `OnDestroy`.
- **An animal could score twice.** Re-entering a pen incremented the safe count
  again, which could win a level with animals still loose. Each id now counts once.
- **Pigs never spawned.** `Spawner` picked a species with `Random.Range(0, 3)`, whose
  integer overload is exclusive on the upper bound, so the `case 3` pig branch was
  unreachable. Now indexes the prefab array directly.
- **Null-delegate crash risk.** A pen collision before `winCondition` subscribed
  would throw on `EventManager.onSafe(id)`. Raising now goes through
  `EventManager.RaiseSafe`, which null-checks.
- Added null guards around `GetComponent`/`AudioSource` use in the pickup, pen and
  win-condition paths.

### Refactor and optimization

- Added `AnimalBaseState`, a shared base for the animal states. The player transform
  is now resolved **once per animal** in `MovementSM.Awake` instead of each of the
  five states running its own `FindWithTag("Player")`, and the duplicated distance
  and movement code lives in one place.
- Distance checks compare squared magnitudes, avoiding a square root per animal per
  frame.
- `winCondition.OnGUI` no longer allocates a `GUIStyle` every frame (it ran twice a
  frame, every frame, for the whole level); it is built once and cached. The HUD
  clock is also clamped so it cannot display a negative time.
- `winCondition` tracks penned animals with a `bool[]` rather than shuffling a
  `GameObject[]`, and gathers its roster with a tag loop instead of four
  `Concat` calls.
- `PickUp`'s four-slot carry logic replaced a hand-unrolled shuffle with a loop, and
  the stacking rule is now a named `CanStack` check.
- `PlayerMovement` no longer calls `GetComponent<PickUp>()` every frame.
- `Thrown` now snaps to the exact landing point on arrival instead of stopping
  wherever the last frame left it.
- `Roaming` uses `Random.insideUnitCircle` rather than building a `Vector3` and
  reading it as a `Vector2`.
- `MathParabola` is now a static class; `StateMachine.ChangeState` null-guards.
- Removed commented-out dead blocks, unused `using` directives, leftover
  `Debug.Log` noise and empty `Start`/`Update` stubs throughout.
- Added XML doc comments and tooltips explaining the non-obvious parts: the
  stacking rule, the fixed-arc throw, the spawn-then-scan ordering, and the
  id-assignment handshake between `winCondition` and the pens.

### Preserved deliberately

- Level tuning is untouched: spawn counts, per-species speed/weight, the authored
  `roamTimer`, and the 120s/90s/90s level timers all behave exactly as before.
- `EventManager` stays a `MonoBehaviour` because the three level scenes attach it to
  a scene object; making it static would have orphaned those components.
