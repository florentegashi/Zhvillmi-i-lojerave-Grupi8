using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LostKingdom.Editor
{
    // Local file bridge lets an already-open editor execute explicit project tooling requests.
    [InitializeOnLoad]
    public static class DemoAutomation
    {
        const string Request = "Logs/DemoCommand.txt";
        static DemoAutomation() { EditorApplication.update += Update; }
        static void Update()
        {
            if (EditorApplication.isCompiling || EditorApplication.isUpdating || EditorApplication.isPlayingOrWillChangePlaymode || !File.Exists(Request)) return;
            string command = File.ReadAllText(Request).Trim(); File.Delete(Request);
            try
            {
                if (command == "generate") LostKingdomSetup.Generate();
                else if (command == "verify") Verify();
                File.WriteAllText("Logs/DemoCommandResult.txt", "OK: " + command);
            }
            catch (System.Exception error) { Debug.LogException(error); File.WriteAllText("Logs/DemoCommandResult.txt", error.ToString()); }
        }
        [MenuItem("The Lost Kingdom/Run Play Mode Smoke Test")]
        public static void Verify()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MysticForest.unity");
            SessionState.SetBool("LostKingdomSmokeTest", true);
            EditorApplication.isPlaying = true;
        }
    }
}
