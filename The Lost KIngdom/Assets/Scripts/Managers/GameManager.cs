using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LostKingdom
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public UIManager ui;
        public ForestRestoration restoration;
        public AudioSource collectionAudio;
        public int Fragments { get; private set; }
        public int Crystals { get; private set; }
        public enum GameState { Menu, Playing, Paused, Restoring, Complete }
        public GameState State { get; private set; }
        public bool IsPlaying => State == GameState.Playing;
        void Awake() { Instance = this; Time.timeScale = 1; State = GameState.Menu; }
        void Start() { ui.ShowMenu(); LockCursor(false); }
        public void Play() { State = GameState.Playing; ui.ShowHUD(); LockCursor(true); }
        public void Resume() { Pause(false); }
        public void Pause(bool pause)
        {
            if (State != GameState.Playing && State != GameState.Paused) return;
            State = pause ? GameState.Paused : GameState.Playing;
            Time.timeScale = pause ? 0 : 1; ui.ShowPause(pause); LockCursor(!pause);
        }
        public void Restart() { Time.timeScale = 1; StartDirectly = true; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        public void MainMenu() { Time.timeScale = 1; StartDirectly = false; SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        public static bool StartDirectly;
        void LateUpdate() { if (StartDirectly && State == GameState.Menu) { StartDirectly = false; Play(); } }
        public void CollectFragment()
        {
            Fragments = Mathf.Min(Fragments + 1, 5); ui.UpdateCounters(Fragments, Crystals);
            if (collectionAudio) collectionAudio.Play();
            if (Fragments == 5) { ui.Notify("All fragments collected! The magical gate is now unlocked.", 7); ui.SetObjective("Follow the lanterns to the ancient gate. Press E to open it."); }
            else ui.Notify("Light fragment recovered — " + Fragments + " of 5", 3);
        }
        public void CollectCrystal()
        {
            if (!IsPlaying || Crystals != 0) return;
            Crystals = 1; State = GameState.Restoring; ui.UpdateCounters(Fragments, Crystals);
            if (collectionAudio) collectionAudio.Play();
            ui.SetPrompt(""); ui.SetObjective("The light has returned to Mystic Forest!");
            ui.Notify("The light has returned to Mystic Forest!", 6); StartCoroutine(Finish());
        }
        IEnumerator Finish() { yield return restoration.Restore(); State = GameState.Complete; LockCursor(false); ui.ShowComplete(); }
        public static void LockCursor(bool locked) { Cursor.lockState = locked ? CursorLockMode.Locked : CursorLockMode.None; Cursor.visible = !locked; }
        void OnDestroy() { Time.timeScale = 1; LockCursor(false); if (Instance == this) Instance = null; }
    }
}
