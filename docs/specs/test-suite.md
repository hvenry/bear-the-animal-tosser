# Test suite

**Status:** draft

Adds automated EditMode and PlayMode tests so gameplay rules can be changed without replaying every level by hand.

## Goal
The repo has no tests; every rule (stacking, scoring once, timer, fleeing) is verified only by playing.
Done means the core rules are covered, tests run headless from the command line, and bug fixes can land with a regression test first.

## Scope

- In: test assemblies, EditMode tests for pure logic, PlayMode tests for scene-level rules, headless run commands in `AGENTS.md`.
- Out: CI pipeline, visual or audio checks, changing level tuning.

## Design

- `com.unity.test-framework` is already in `Packages/manifest.json`.
- Game scripts currently compile into `Assembly-CSharp`, which test assemblies cannot reference.
  Add `Assets/Scripts/BearGame.asmdef` and move `winCondition.cs`, `EventManager.cs` and `GameOverScreen.cs` under `Assets/Scripts/` (in the editor, so `.meta` GUIDs follow) so all game code is in one referenceable assembly.
- `Assets/Tests/EditMode/` (asmdef, Editor platform only):
  - `MathParabola`: endpoints, peak height at `t = 0.5`.
  - `PickUp.CanStack`: needs a seam, e.g. extract the weight rule into a static `StackRule.CanStack(int candidate, IEnumerable<int> carried)`.
  - `winCondition.Switch`: an id counts once; out-of-range ids are ignored.
- `Assets/Tests/PlayMode/`:
  - Thrown animal lands exactly on the target point after the flight duration.
  - Animal moves to `Fleeing` when the player is within `fleeThreshold`.
  - Pen collision with the matching tag raises `onSafe` once; a non-matching tag does not.
  - Loading each level scene spawns at least one animal and the roster `_total > 0` after the scan delay.
- Run headless:
  `"$UNITY" -batchmode -nographics -projectPath . -runTests -testPlatform EditMode -testResults Logs/editmode.xml -logFile -`
  and a single test with `-testFilter <Namespace.Class.Method>`.

## Tasks

- [ ] Add the game asmdef and move root-level scripts under `Assets/Scripts/` in the editor
- [ ] Confirm all scenes and prefabs still resolve their scripts (no "missing script" warnings)
- [ ] Extract the stacking rule into a testable pure function
- [ ] Add EditMode assembly and tests
- [ ] Add PlayMode assembly and tests
- [ ] Add test commands (suite and single test) to `AGENTS.md` Commands

## Done when

- [ ] EditMode and PlayMode suites pass headless with a zero exit code
- [ ] All three levels still play identically in the editor
- [ ] `AGENTS.md` lists the suite and single-test commands and drops the "no test suite" line
- [ ] `docs/testing.md` written from this spec, triggered from `AGENTS.md`, and this spec deleted

## Open questions

- Assembly name and namespace for game code. Default: `BearGame`, no namespace change to avoid touching every file.

## Related

- [Animal AI](../animal-ai.md)
- [Carry and throw](../carry-and-throw.md)
- [Level flow](../level-flow.md)
