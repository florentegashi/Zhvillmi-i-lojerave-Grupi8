using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace LostKingdom.Editor
{
    public static class DemoPreview
    {
        [MenuItem("The Lost Kingdom/Capture Demo Previews")]
        public static void Capture()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/MysticForest.unity");
            var camera = Camera.main;
            Vector3 pivot = new Vector3(0, 1.6f, 0);
            camera.transform.position = pivot + Quaternion.Euler(22, 0, 0) * Vector3.back * 6;
            camera.transform.LookAt(pivot);
            var canvas = Object.FindAnyObjectByType<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera; canvas.worldCamera = camera; canvas.planeDistance = 1;
            Directory.CreateDirectory("Previews");
            Render(camera, "Previews/MainMenu.png");
            var ui = canvas.GetComponent<UIManager>(); ui.ShowHUD();
            Render(camera, "Previews/MysticForest.png");
            camera.transform.position = new Vector3(12, 8, 151);
            camera.transform.LookAt(new Vector3(0, 4, 165));
            Render(camera, "Previews/Sanctuary.png");
            // Preview changes are intentionally discarded; the saved scene stays unchanged.
            EditorSceneManager.OpenScene("Assets/Scenes/MysticForest.unity");
            Debug.Log("LOST KINGDOM: Rendered three demo previews.");
        }
        static void Render(Camera camera, string path)
        {
            var target = new RenderTexture(1600, 900, 24); target.Create();
            camera.targetTexture = target; Canvas.ForceUpdateCanvases(); camera.Render();
            RenderTexture previous = RenderTexture.active; RenderTexture.active = target;
            var pixels = new Texture2D(1600, 900, TextureFormat.RGB24, false);
            pixels.ReadPixels(new Rect(0, 0, 1600, 900), 0, 0); pixels.Apply();
            File.WriteAllBytes(path, pixels.EncodeToPNG());
            camera.targetTexture = null; RenderTexture.active = previous;
            Object.DestroyImmediate(pixels); target.Release(); Object.DestroyImmediate(target);
        }
    }
}
