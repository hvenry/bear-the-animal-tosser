# Setup

How to install the right Unity editor, open the project, play it, and build it on macOS, Windows and Arch Linux.

## Why
The project only opens cleanly in the editor version it was saved with (or newer).
Opening it in an older editor attempts a downgrade and is not supported, and some Linux setups need workarounds before the editor will start at all.

## How it works
**Editor version.** The required version is in `ProjectSettings/ProjectVersion.txt`.
Install it, or anything newer, through Unity Hub (**Installs > Install Editor**), then **Projects > Add** the cloned folder.
The editor archive is at https://unity.com/releases/editor/archive if you skip the Hub.

**Per platform**
- macOS: `brew install --cask unity-hub`; pick the Apple Silicon or Intel editor to match the machine.
- Windows: install [Unity Hub](https://unity.com/download).
- Arch Linux: `yay -S unityhub` (AUR), or `git clone https://aur.archlinux.org/unityhub.git && cd unityhub && makepkg -si`.

**First open.** The first import builds the `Library/` cache and takes a few minutes; it happens once.
Packages in `Packages/manifest.json` restore automatically, and SuperTiled2Unity is vendored, so there is nothing else to install.

**Play.** Open `Assets/Scenes/Main.unity` (main menu) and press Play.
To jump into gameplay, open `Assets/Scenes/Levels/BiggerMap.unity` instead; this skips menu setup, which is fine for testing.

**Build.** The build scene list in `ProjectSettings/EditorBuildSettings.asset` already holds all eight scenes in order.
Pick a target in File > Build Profiles and build.
WebGL is what itch.io hosts; desktop standalone builds also work.
Headless commands are in `AGENTS.md`.

## Tech
- Unity Hub and the Unity editor.
- Unity Package Manager for `Packages/manifest.json`.

## Key files
- `ProjectSettings/ProjectVersion.txt` - required editor version
- `Packages/manifest.json` - Unity package dependencies
- `ProjectSettings/EditorBuildSettings.asset` - scenes included in builds, in order

## Decisions and gotchas
- The project was built on Unity 2021.3 and migrated to Unity 6 (see `CHANGELOG.md`); there is no supported path back.
- Wayland: if the Hub will not launch or renders blank, run `GDK_BACKEND=x11 unityhub`.
  If the Hub silently fails to start the editor, run it directly: `~/Unity/Hub/Editor/<version>/Editor/Unity -projectPath <repo>`.
- Arch with pre-Unity 6 editors: they link `libxml2.so.2`, which current Arch no longer ships, and fail with `error while loading shared libraries: libxml2.so.2`.
  `sudo pacman -S libxml2-legacy` fixes it; Unity 6 is unaffected.
- `MainMenu.QuitGame` does nothing in the editor or a WebGL build.

## Related
- [Level flow](level-flow.md)
- [Gameplay](gameplay.md)
