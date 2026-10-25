#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

namespace LostKingdom
{
    // Exercises the saved scene in real Play Mode; it does not run during normal play or builds.
    public class DemoSmokeTest : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        static void StartTest()
        {
            if (!UnityEditor.SessionState.GetBool("LostKingdomSmokeTest", false)) return;
            UnityEditor.SessionState.SetBool("LostKingdomSmokeTest", false);
            var test = new GameObject("Automated play mode verification").AddComponent<DemoSmokeTest>();
            DontDestroyOnLoad(test.gameObject);
        }
        readonly List<string> results = new List<string>();
        Keyboard keyboard;
        InputSettings.BackgroundBehavior originalBackground;
        InputSettings.UpdateMode originalUpdate;
        InputSettings.EditorInputBehaviorInPlayMode originalEditorInput;
        bool originalRunInBackground;
        IEnumerator Start()
        {
            originalBackground = InputSystem.settings.backgroundBehavior;
            originalUpdate = InputSystem.settings.updateMode;
            originalEditorInput = InputSystem.settings.editorInputBehaviorInPlayMode;
            originalRunInBackground = Application.runInBackground;
            keyboard = InputSystem.AddDevice<Keyboard>("DemoTestKeyboard");
            InputSystem.settings.backgroundBehavior = InputSettings.BackgroundBehavior.IgnoreFocus;
            InputSystem.settings.updateMode = InputSettings.UpdateMode.ProcessEventsInDynamicUpdate;
            InputSystem.settings.editorInputBehaviorInPlayMode = InputSettings.EditorInputBehaviorInPlayMode.AllDeviceInputAlwaysGoesToGameView;
            InputSystem.EnableDevice(keyboard);
            Application.runInBackground = true;
            IEnumerator routine = Checks();
            while (true)
            {
                object current;
                try { if (!routine.MoveNext()) break; current = routine.Current; }
                catch (System.Exception error)
                {
                    results.Add("FAIL: " + error); Finish(false); yield break;
                }
                yield return current;
            }
            Finish(true);
        }
        void Assert(bool value, string description)
        { if (!value) throw new System.Exception(description); results.Add("PASS: " + description); }
        void Click(GameObject panel, string name)
        {
            foreach (var button in panel.GetComponentsInChildren<Button>(true))
                if (button.name == name) { button.onClick.Invoke(); return; }
            throw new System.Exception("Button missing: " + name);
        }
        void Keys(params Key[] keys)
        {
            keyboard.MakeCurrent();
            InputSystem.QueueStateEvent(keyboard, new KeyboardState(keys));
        }
        void Warp(Vector3 position)
        {
            var player = FindAnyObjectByType<PlayerMovement>(); var cc = player.GetComponent<CharacterController>();
            cc.enabled = false; player.transform.position = position; cc.enabled = true; Physics.SyncTransforms(); cc.Move(Vector3.down * .02f);
        }
        IEnumerator Checks()
        {
            yield return null; yield return null;
            var game = GameManager.Instance;
            Assert(game.State == GameManager.GameState.Menu && game.ui.menu.activeSelf, "Saved scene starts at main menu");
            Click(game.ui.menu, "Settings"); Assert(game.ui.settings.activeSelf, "Serialized Settings button works");
            Click(game.ui.settings, "Back"); Assert(game.ui.menu.activeSelf, "Serialized Back button works");
            Click(game.ui.menu, "Play"); Assert(game.IsPlaying && game.ui.hud.activeSelf, "Serialized Play button starts gameplay");
            yield return new WaitForSeconds(.5f);
            var player = FindAnyObjectByType<PlayerMovement>();
            Vector3 start = player.transform.position;
            Keys(Key.W); yield return new WaitForSeconds(.6f);
            Keys(); yield return null;
            float walked = Vector3.Distance(start, player.transform.position);
            Assert(walked > 1.2f, "Input System WASD moves CharacterController (distance=" + walked + ", position=" + player.transform.position + ", enabled=" + keyboard.enabled + ")");
            start = player.transform.position;
            Keys(Key.W, Key.LeftShift); yield return new WaitForSeconds(.6f); Keys(); yield return null;
            Assert(Vector3.Distance(start, player.transform.position) > walked * 1.25f, "Shift sprint is faster than walking");
            yield return new WaitForSeconds(.2f);
            float initialY = player.transform.position.y;
            Keys(Key.Space); yield return new WaitForSeconds(.2f); Keys(); yield return null;
            Assert(player.transform.position.y > initialY + .5f, "Space jumps from ground");
            yield return new WaitForSeconds(1);
            Assert(player.GetComponent<CharacterController>().isGrounded, "Gravity returns Aren to ground");
            game.Pause(true); start = player.transform.position; Keys(Key.W);
            yield return new WaitForSecondsRealtime(.3f); Keys(); yield return null;
            Assert(Time.timeScale == 0 && Vector3.Distance(start, player.transform.position) < .01f, "Pause freezes player and game time");
            Click(game.ui.pause, "Resume"); Assert(game.IsPlaying && Time.timeScale == 1, "Resume restores gameplay");
            var gate = FindAnyObjectByType<MagicalGate>(); gate.Interact(); Assert(!gate.IsOpen, "Gate rejects interaction before five fragments");
            Warp(new Vector3(0, .1f, 160));
            for (int i = 0; i < 20; i++) player.GetComponent<CharacterController>().Move(Vector3.forward * .3f);
            Assert(player.transform.position.z < 162.3f, "Closed gate physically blocks CharacterController");
            var fragments = FindObjectsByType<LightFragment>();
            Assert(fragments.Length == 5, "Scene contains exactly five fragments");
            for (int i = 0; i < fragments.Length; i++)
            {
                Warp(fragments[i].transform.position - Vector3.up * .8f);
                yield return new WaitForSeconds(.25f);
                Assert(game.Fragments == i + 1 && !fragments[i].gameObject.activeSelf, "Physics trigger collects fragment " + (i + 1) + " exactly once");
            }
            Assert(game.ui.fragmentText.text == "Light Fragments: 5/5", "HUD updates to five fragments");
            Warp(new Vector3(0, .1f, 159)); yield return new WaitForSeconds(.2f);
            Assert(game.ui.promptText.text == "Press E to open the gate.", "Gate proximity shows interaction prompt");
            Keys(Key.E); yield return null; yield return null; Keys();
            yield return new WaitForSeconds(2.3f);
            Assert(gate.IsOpen && !gate.door.gameObject.activeSelf, "E animates and opens gate");
            Warp(new Vector3(0, .1f, 160));
            for (int i = 0; i < 30; i++) player.GetComponent<CharacterController>().Move(Vector3.forward * .25f);
            Assert(player.transform.position.z > 166, "Open gate permits physical passage");
            Warp(new Vector3(0, 1, 176)); yield return new WaitForSeconds(.3f);
            Assert(game.ui.promptText.text.Contains("collect the Crystal"), "Crystal proximity shows prompt");
            Keys(Key.E); yield return null; yield return null; Keys();
            Assert(game.Crystals == 1 && game.State == GameManager.GameState.Restoring, "E collects first crystal and starts restoration");
            yield return new WaitForSeconds(4.5f);
            Assert(game.State == GameManager.GameState.Complete && game.ui.complete.activeSelf, "Four-second restoration reaches completion screen");
            Assert(game.restoration.sun.intensity > 2 && game.restoration.restoredLights.activeSelf && RenderSettings.fogDensity < .005f, "Forest lighting, fog and wisps restore");
            Click(game.ui.complete, "Restart Level"); yield return new WaitForSeconds(1);
            game = GameManager.Instance;
            Assert(game.IsPlaying && game.Fragments == 0 && game.Crystals == 0 && !FindAnyObjectByType<MagicalGate>().IsOpen, "Restart resets progression and gate");
            Assert(game.restoration.sun.intensity < 1 && !game.restoration.restoredLights.activeSelf, "Restart restores cursed forest state");
            game.Pause(true); Click(game.ui.pause, "Main Menu"); yield return new WaitForSeconds(1);
            Assert(GameManager.Instance.State == GameManager.GameState.Menu && Time.timeScale == 1, "Main Menu reload resets time and menu state");
        }
        void Finish(bool success)
        {
            if (keyboard != null) InputSystem.RemoveDevice(keyboard);
            InputSystem.settings.backgroundBehavior = originalBackground;
            InputSystem.settings.updateMode = originalUpdate;
            InputSystem.settings.editorInputBehaviorInPlayMode = originalEditorInput;
            Application.runInBackground = originalRunInBackground;
            File.WriteAllLines("Logs/PlayModeVerification.txt", results);
            Debug.Log("LOST KINGDOM PLAY MODE " + (success ? "PASS" : "FAIL") + ": " + results.Count + " checks. See Logs/PlayModeVerification.txt");
            UnityEditor.EditorApplication.isPlaying = false;
            Destroy(gameObject);
            if (Application.isBatchMode) UnityEditor.EditorApplication.Exit(success ? 0 : 1);
        }
    }
}
#endif
