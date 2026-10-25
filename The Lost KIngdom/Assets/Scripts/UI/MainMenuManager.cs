using UnityEngine;
namespace LostKingdom
{
    public class MainMenuManager : MonoBehaviour
    {
        public void Quit()
        {
            Application.Quit();
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #endif
        }
        public void Sensitivity(float value) { PlayerPrefs.SetFloat("Sensitivity", value); }
        public void Volume(float value) { AudioListener.volume = value; PlayerPrefs.SetFloat("Volume", value); }
    }
}
