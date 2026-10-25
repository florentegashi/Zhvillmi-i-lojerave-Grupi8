using System.Collections;
using UnityEngine;

namespace LostKingdom
{
    public class ForestRestoration : MonoBehaviour
    {
        public Light sun;
        public Material ground, leaves;
        public GameObject restoredLights;
        public IEnumerator Restore()
        {
            Color oldAmbient = RenderSettings.ambientLight, oldFog = RenderSettings.fogColor;
            Color oldGround = ground.color, oldLeaves = leaves.color;
            float intensity = sun.intensity;
            restoredLights.SetActive(true);
            for (float elapsed = 0; elapsed < 4; elapsed += Time.deltaTime)
            {
                float t = Mathf.SmoothStep(0, 1, elapsed / 4);
                RenderSettings.ambientLight = Color.Lerp(oldAmbient, new Color(.55f, .65f, .52f), t);
                RenderSettings.fogColor = Color.Lerp(oldFog, new Color(.48f, .7f, .65f), t);
                RenderSettings.fogDensity = Mathf.Lerp(.012f, .004f, t);
                Camera.main.backgroundColor = RenderSettings.fogColor;
                sun.intensity = Mathf.Lerp(intensity, 2.2f, t);
                sun.color = Color.Lerp(new Color(.55f, .7f, 1), new Color(1, .91f, .68f), t);
                ground.color = Color.Lerp(oldGround, new Color(.18f, .38f, .18f), t);
                leaves.color = Color.Lerp(oldLeaves, new Color(.22f, .58f, .3f), t);
                yield return null;
            }
        }
        void Awake()
        {
            // Runtime copies prevent restoration from modifying saved material assets in the editor.
            Material originalGround = ground, originalLeaves = leaves;
            ground = new Material(ground); leaves = new Material(leaves);
            foreach (var renderer in FindObjectsByType<Renderer>())
            {
                if (renderer.sharedMaterial == originalGround) renderer.sharedMaterial = ground;
                else if (renderer.sharedMaterial == originalLeaves) renderer.sharedMaterial = leaves;
            }
        }
        void OnDestroy() { Destroy(ground); Destroy(leaves); }
    }
}
