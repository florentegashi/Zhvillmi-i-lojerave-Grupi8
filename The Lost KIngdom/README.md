# The Lost Kingdom — First Playable

A third-person fantasy exploration prototype for a university project. Level 1, **Mystic Forest**, contains the complete fragment → gate → Crystal of Light → forest restoration → completion loop. Ancient Cave and Lost Temple are reserved for later development.

## Open and play

1. In Unity Hub, add this project folder (the folder containing `Assets`, `Packages`, and `ProjectSettings`).
2. Open using **Unity 6000.6.4f1**. The existing URP, Input System, and Unity UI packages are retained. No paid assets or extra packages are required.
3. Let Unity finish importing and compiling.
4. Open `Assets/Scenes/MysticForest.unity`, press the editor **Play** button, then click **Play** in the game menu. Click inside the Game view if needed to give it keyboard focus.
5. If the generated scene is missing, select **The Lost Kingdom → Generate First Playable Demo**. This imports the bundled TextMeshPro Essential Resources, creates materials and prefabs, generates the scene, saves it, and registers it in Build Settings. No component wiring is needed.

The original `SampleScene.unity`, render pipeline settings, and input action asset are preserved. Generation deliberately overwrites only the generated Mystic Forest scene and named demo assets; copy the scene before making custom level edits. `Assets/LostKingdomGenerated` marks the generated content workflow; reusable assets live in `Assets/Prefabs`, `Assets/Materials`, and `Assets/Audio`.

## Controls

| Input | Action |
| --- | --- |
| W A S D | Move relative to the camera |
| Mouse | Orbit camera |
| Left Shift | Sprint |
| Space | Jump |
| E | Open the unlocked gate / collect the crystal |
| Escape | Pause / resume |

Settings offers mouse sensitivity and collection sound volume. Preferences persist locally. Escape releases the cursor through the pause menu. In the Unity editor, the Exit menu button stops Play Mode; in a Windows build it exits the application.

## Level walkthrough

Follow the pale trail and green lanterns north. Wayfinder signs identify five side paths. Fragments collect on touch and pulse green; the final crystal glows blue.

1. **Fallen Watcher:** explore west of the first sign and circle or jump over the fallen trunk.
2. **Moon Pool:** turn east toward the pool and cross its stepping stones. The water is shallow and nonlethal; players can walk out if they miss a jump.
3. **Broken Court:** explore the western ruin, enter through the east opening, and follow the partition around to the fragment in the northwest corner.
4. **Star Shrine:** turn east and jump onto three broad stone steps. The fragment floats above the highest step.
5. **Wisp Circle:** follow the western side path to the standing stones.

Return to the lantern trail and continue to the sanctuary wall. Approach the gate, press E with 5/5 fragments, and wait for the door to rise. Approach the blue crystal and press E. The forest brightens over four seconds, greenery changes color, wisps appear, and the completion screen reports 1/3 crystals. Restart and Main Menu are available from pause and completion.

The layout targets approximately 5–8 minutes for first-time exploration. This is a pacing target, not a measured user playtest result; sprinting or knowing the route shortens it. There are no enemies, death penalties, or save files in this first demo.

## Architecture

- `Scripts/Player`: CharacterController movement, input adapter, and nearest trigger interaction selection.
- `Scripts/Camera`: smooth orbit with pitch limits and sphere-cast obstruction avoidance. Aren uses the built-in Ignore Raycast layer to prevent self-collision with the camera.
- `Scripts/Collectibles`: touch fragments, crystal interaction, and temporary particle bursts.
- `Scripts/Interactions`: common trigger target and coroutine-driven gate.
- `Scripts/Managers`: game states and four-second lighting/material restoration. Runtime material copies protect saved assets.
- `Scripts/UI`: TextMeshPro HUD, menu callbacks, pause handling, and settings.
- `Editor/LostKingdomSetup.cs`: deterministic scene and reusable asset generator. UI callbacks are persistent serialized listeners, so menus work after reopening the saved scene and in builds.
- `Editor/DemoAutomation.cs`: local editor tooling bridge. An explicit `generate` or `verify` request in `Logs/DemoCommand.txt` is consumed while the editor is idle; normal gameplay never uses it.
- `DemoSmokeTest.cs`: editor-only automated Play Mode verification; excluded from Windows players.

All progress is scene-local. `GameManager` exposes fragment/crystal counters and explicit states, and interaction targets can be reused for later level mechanics. A future full game should move persistent three-level crystal progress into a separate save service.

## Test in Unity

Verified on 25 October 2026 using Unity 6000.6.4f1: scene generation succeeded, and **29 automated Play Mode checks passed**. The verified saved scene and generated assets were copied into this project without changing their contents. See `Documentation/PlayModeVerification.txt` for individual results. Menu, gameplay HUD and sanctuary renders were also inspected/saved in `Previews`. A human end-to-end route/pacing playtest and a Windows executable build have not been performed.

Select **The Lost Kingdom → Run Play Mode Smoke Test** while outside Play Mode. It opens the saved demo scene, enters real Play Mode, exercises gameplay with an injected Input System keyboard and physics triggers, writes `Logs/PlayModeVerification.txt`, then stops Play Mode. Do not use the keyboard during this short automated run. Tests warp between stations; they do not measure completion time or replace a human route/camera playtest.

TextMeshPro Essential Resources are included. If those files are removed and Unity reports a missing font during regeneration, use **Window → TextMeshPro → Import TMP Essential Resources**, wait for import to finish, then run the generator again.

Manual acceptance checklist:

- Play, Settings/Back, and Exit work; text fits at 16:9 and at your intended window size.
- Walk, sprint, jump, rotate camera, and test camera near trees and walls.
- Pause stops movement and animations; Resume restores controls.
- Find each fragment through normal traversal and check that each increments once.
- Gate blocks passage and rejects E before 5/5; opens after 5/5.
- Collect crystal, observe the four-second restoration and completion text.
- Restart resets all fragments, the gate, lighting and crystal count; Main Menu returns with time unpaused.

## Windows build

In **File → Build Profiles**, select **Windows**, switch platform if necessary, choose x86_64, and build to a folder such as `Builds/Windows`. The generator enables `MysticForest` as the first scene. Install Windows Build Support through Unity Hub only if Unity reports it missing. Distribute the executable together with its data folder and Unity runtime files.

## Scope and remaining university work

This demo uses primitive placeholder art, a generated chime, simple particles, realtime lighting, and a single scene with menu overlays. Art, animation, richer audio, accessibility options, measured pacing, user testing, and Levels 2 and 3 remain later work. No complex shaders, networking, combat, or external assets are required.

Suggested 10-week progression: weeks 1–2 core controller and prototype; weeks 3–4 validate and polish Mystic Forest; weeks 5–6 Ancient Cave; weeks 7–8 Lost Temple and persistent progress; week 9 playtesting/accessibility/performance; week 10 fixes, Windows build, report and presentation.
