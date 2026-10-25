using TMPro;
using UnityEngine;
namespace LostKingdom
{
    public class UIManager : MonoBehaviour
    {
        public GameObject menu, hud, pause, complete, settings;
        public TMP_Text fragmentText, crystalText, objectiveText, promptText, notificationText;
        float notificationUntil;
        void Start() { AudioListener.volume = PlayerPrefs.GetFloat("Volume", .65f); }
        void Update() { if (Time.time > notificationUntil) notificationText.text = ""; }
        public void ShowMenu() { menu.SetActive(true); hud.SetActive(false); pause.SetActive(false); complete.SetActive(false); settings.SetActive(false); }
        public void ShowHUD() { menu.SetActive(false); hud.SetActive(true); settings.SetActive(false); UpdateCounters(0, 0); SetObjective("Recover five light fragments. Explore the lantern trail and its side paths."); }
        public void ShowPause(bool show) { pause.SetActive(show); }
        public void OpenSettings() { menu.SetActive(false); settings.SetActive(true); }
        public void CloseSettings() { PlayerPrefs.Save(); settings.SetActive(false); menu.SetActive(true); }
        public void ShowComplete() { hud.SetActive(false); complete.SetActive(true); }
        public void UpdateCounters(int fragments, int crystals) { fragmentText.text = "Light Fragments: " + fragments + "/5"; crystalText.text = "Crystals Collected: " + crystals + "/3"; }
        public void SetObjective(string value) { objectiveText.text = value; }
        public void SetPrompt(string value) { promptText.text = value; }
        public void Notify(string value, float seconds = 5) { notificationText.text = value; notificationUntil = Time.time + seconds; }
    }
}
