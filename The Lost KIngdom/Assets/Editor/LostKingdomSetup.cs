using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace LostKingdom.Editor
{
    // Generates real, editable scene objects; no runtime world generation is required.
    public static class LostKingdomSetup
    {
        const string Root = "Assets/LostKingdomGenerated";
        static Material ground, leaf, bark, stone, path, gold, blue, water;
        static TMP_FontAsset font;
        static GameManager game;
        static UIManager ui;
        static GameObject treePrefab, rockPrefab, fragmentPrefab, lanternPrefab;
        static readonly Color Ink = new Color(.025f, .055f, .085f, .96f);
        static readonly Color Parchment = new Color(.91f, .88f, .72f);

        [MenuItem("The Lost Kingdom/Generate First Playable Demo")]
        public static void Generate()
        {
            if (File.Exists("Assets/Scenes/MysticForest.unity") && !Application.isBatchMode &&
                !EditorUtility.DisplayDialog("Regenerate Mystic Forest?", "This replaces only the generated MysticForest scene and generated assets. Save your custom changes elsewhere first.", "Regenerate", "Cancel")) return;
            foreach (string folder in new[] { "Scenes", "Scripts/Player", "Scripts/Camera", "Scripts/Collectibles", "Scripts/Interactions", "Scripts/Managers", "Scripts/UI", "Prefabs", "Materials", "Models", "Audio", "Animations", "Resources", "Editor" })
                Directory.CreateDirectory("Assets/" + folder);
            Directory.CreateDirectory(Root);
            AssetDatabase.Refresh();
            if (!AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset"))
            {
                foreach (string package in Directory.GetFiles("Library/PackageCache", "TMP Essential Resources.unitypackage", SearchOption.AllDirectories))
                { UnityEditor.AssetPackage.Package.Import(Path.GetFullPath(package), false); break; }
                AssetDatabase.Refresh();
            }
            font = AssetDatabase.LoadAssetAtPath<TMP_FontAsset>("Assets/TextMesh Pro/Resources/Fonts & Materials/LiberationSans SDF.asset");
            if (!font) throw new System.InvalidOperationException("TMP Essential Resources could not be imported. Import them from Window > TextMeshPro, then generate again.");
            EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Single);
            Random.InitState(1807);
            ground = Mat("Forest floor", new Color(.055f, .12f, .105f));
            leaf = Mat("Pine needles", new Color(.055f, .17f, .16f));
            bark = Mat("Bark", new Color(.17f, .12f, .13f));
            stone = Mat("Ancient stone", new Color(.21f, .29f, .33f));
            path = Mat("Lantern trail", new Color(.25f, .28f, .25f));
            gold = Mat("Fragment light", new Color(.55f, 1, .73f), 2.5f);
            blue = Mat("Crystal light", new Color(.15f, .65f, 1), 3f);
            water = Mat("Still water", new Color(.035f, .18f, .23f));
            Material sparks = new Material(Shader.Find("Universal Render Pipeline/Particles/Unlit"));
            sparks.SetColor("_BaseColor", Color.white); Save(sparks, "Assets/Resources/LostKingdomSpark.mat");
            MakePrefabs();
            BuildEnvironment();
            BuildGameplay();
            BuildUI();
            EditorSceneManager.SaveScene(EditorSceneManager.GetActiveScene(), "Assets/Scenes/MysticForest.unity");
            EditorBuildSettings.scenes = new[] { new EditorBuildSettingsScene("Assets/Scenes/MysticForest.unity", true) };
            PlayerSettings.companyName = "Eldoria Student Studio";
            PlayerSettings.productName = "The Lost Kingdom";
            PlayerSettings.defaultScreenWidth = 1600; PlayerSettings.defaultScreenHeight = 900;
            AssetDatabase.SaveAssets();
            Debug.Log("LOST KINGDOM: Generated MysticForest scene, materials, prefabs and build settings successfully.");
        }

        static Material Mat(string name, Color color, float emission = 0)
        {
            var m = new Material(Shader.Find("Universal Render Pipeline/Lit")); m.name = name; m.color = color;
            m.SetFloat("_Smoothness", .15f);
            if (emission > 0) { m.EnableKeyword("_EMISSION"); m.SetColor("_EmissionColor", color * emission); }
            string assetPath = "Assets/Materials/" + name + ".mat";
            Save(m, assetPath); return AssetDatabase.LoadAssetAtPath<Material>(assetPath);
        }
        static void Save(Object asset, string location)
        {
            var existing = AssetDatabase.LoadAssetAtPath<Object>(location);
            if (existing) { EditorUtility.CopySerialized(asset, existing); Object.DestroyImmediate(asset); }
            else AssetDatabase.CreateAsset(asset, location);
        }
        static GameObject Shape(string name, PrimitiveType type, Vector3 position, Vector3 scale, Material material, Transform parent = null, bool solid = true)
        {
            var obj = GameObject.CreatePrimitive(type); obj.name = name;
            obj.transform.position = position; obj.transform.localScale = scale; obj.transform.SetParent(parent, true);
            obj.GetComponent<Renderer>().sharedMaterial = material;
            if (!solid) Object.DestroyImmediate(obj.GetComponent<Collider>());
            return obj;
        }
        static GameObject Prefab(GameObject obj, string name)
        {
            var result = PrefabUtility.SaveAsPrefabAsset(obj, "Assets/Prefabs/" + name + ".prefab");
            Object.DestroyImmediate(obj); return result;
        }
        static GameObject Place(GameObject prefab, Vector3 position)
        { var obj = (GameObject)PrefabUtility.InstantiatePrefab(prefab); obj.transform.position = position; return obj; }
        static Light Point(Transform parent, Vector3 pos, Color color, float range, float intensity)
        {
            var obj = new GameObject("Glow"); obj.transform.SetParent(parent); obj.transform.position = pos;
            var light = obj.AddComponent<Light>(); light.type = LightType.Point; light.color = color; light.range = range; light.intensity = intensity; return light;
        }
        static void MakePrefabs()
        {
            var tree = new GameObject("Low-poly pine");
            Shape("Trunk", PrimitiveType.Cylinder, new Vector3(0, 2, 0), new Vector3(.65f, 2, .65f), bark, tree.transform);
            for (int i = 0; i < 3; i++)
            {
                var crown = Shape("Faceted crown", PrimitiveType.Sphere, new Vector3(0, 4 + i * 1.5f, 0), new Vector3(4 - i, 3.7f - i * .4f, 4 - i), leaf, tree.transform, false);
                crown.transform.rotation = Quaternion.Euler(0, i * 45, 12);
            }
            treePrefab = Prefab(tree, "PineTree");
            var rock = Shape("Mossy boulder", PrimitiveType.Sphere, Vector3.zero, new Vector3(2, 1.5f, 2.2f), stone);
            rockPrefab = Prefab(rock, "Rock");
            var fragment = new GameObject("Light fragment");
            var trigger = fragment.AddComponent<SphereCollider>(); trigger.isTrigger = true; trigger.radius = 1.15f;
            var rb = fragment.AddComponent<Rigidbody>(); rb.isKinematic = true; rb.useGravity = false;
            var gem = Shape("Floating shard", PrimitiveType.Cube, Vector3.zero, new Vector3(.55f, .8f, .55f), gold, fragment.transform, false);
            gem.transform.rotation = Quaternion.Euler(25, 0, 45);
            Point(fragment.transform, Vector3.zero, new Color(.3f, 1, .65f), 7, 2);
            fragment.AddComponent<LightFragment>(); fragmentPrefab = Prefab(fragment, "LightFragment");
            var lantern = new GameObject("Trail lantern");
            Shape("Pedestal", PrimitiveType.Cube, new Vector3(0, .45f, 0), new Vector3(.4f, .9f, .4f), stone, lantern.transform);
            Shape("Wisp", PrimitiveType.Sphere, new Vector3(0, 1.1f, 0), Vector3.one * .32f, gold, lantern.transform, false);
            Point(lantern.transform, new Vector3(0, 1.4f, 0), new Color(.5f, 1, .7f), 8, 1.5f);
            lanternPrefab = Prefab(lantern, "TrailLantern");
        }
        static float TrailX(float z) => Mathf.Sin(z * .048f) * 13;
        static void BuildEnvironment()
        {
            Shape("Mystic Forest ground", PrimitiveType.Cube, new Vector3(0, -1, 85), new Vector3(100, 2, 210), ground);
            // Continuous path and solid borders make the route readable and prevent bypassing the gate.
            for (int z = 0; z <= 164; z += 3)
                Shape("Lantern trail", PrimitiveType.Cube, new Vector3(TrailX(z), .015f, z), new Vector3(7, .04f, 5), path);
            for (int z = 0; z < 161; z += 12)
                Place(lanternPrefab, new Vector3(TrailX(z) + (z % 24 == 0 ? -4.4f : 4.4f), 0, z));
            for (int i = 0; i < 240; i++)
            {
                float z = Random.Range(-10, 187), x = Random.Range(-47, 47);
                if (Mathf.Abs(x - TrailX(z)) < 8 || NearStation(x, z) || z > 155 && Mathf.Abs(x) < 12) continue;
                var tree = Place(treePrefab, new Vector3(x, 0, z)); tree.transform.localScale = Vector3.one * Random.Range(.8f, 1.6f);
            }
            for (int i = 0; i < 95; i++)
            {
                float z = Random.Range(-8, 155), x = Random.Range(-44, 44);
                if (Mathf.Abs(x - TrailX(z)) < 6 || NearStation(x, z)) continue;
                var r = Place(rockPrefab, new Vector3(x, .3f, z)); r.transform.localScale *= Random.Range(.5f, 1.6f);
                r.transform.rotation = Random.rotation;
                Shape("Bush", PrimitiveType.Sphere, new Vector3(x + 2, .45f, z), new Vector3(2, 1.3f, 2), leaf, null, false);
            }
            Shape("West ridge", PrimitiveType.Cube, new Vector3(-49, 6, 85), new Vector3(3, 14, 210), stone);
            Shape("East ridge", PrimitiveType.Cube, new Vector3(49, 6, 85), new Vector3(3, 14, 210), stone);
            Shape("Southern ridge", PrimitiveType.Cube, new Vector3(0, 6, -19), new Vector3(100, 14, 3), stone);
            Shape("Northern ridge", PrimitiveType.Cube, new Vector3(0, 6, 189), new Vector3(100, 14, 3), stone);
            Sign(new Vector3(0, 2.4f, 7), "MYSTIC FOREST\nFollow the lanterns. Seek five lights off the trail.");
            // Station 1: circle a fallen tree.
            Sign(new Vector3(TrailX(23), 2.2f, 23), "01 / FALLEN WATCHER\nA light sleeps beyond the old tree.  <");
            var log = Shape("Fallen watcher", PrimitiveType.Cylinder, new Vector3(-6, 1, 31), new Vector3(1.8f, 6, 1.8f), bark);
            log.transform.rotation = Quaternion.Euler(0, 0, 90);
            // Station 2: optional hopping route over shallow water; walking out is always possible.
            Sign(new Vector3(TrailX(49), 2.2f, 49), "02 / MOON POOL\nAcross the stones, a second light.  >");
            Shape("Moon pool", PrimitiveType.Cube, new Vector3(27, .06f, 57), new Vector3(19, .1f, 15), water);
            for (int i = 0; i < 5; i++) Shape("Stepping stone", PrimitiveType.Cylinder, new Vector3(18 + i * 3, .2f + i * .08f, 54 + i % 2 * 3), new Vector3(2, .35f, 2), stone);
            // Station 3: small open-topped ruin with a winding, accessible route.
            Sign(new Vector3(TrailX(79), 2.2f, 79), "03 / BROKEN COURT\nSearch the ruined walls.  <");
            Shape("Ruin west", PrimitiveType.Cube, new Vector3(-33, 2, 88), new Vector3(1, 4, 19), stone);
            Shape("Ruin north", PrimitiveType.Cube, new Vector3(-25, 2, 97), new Vector3(16, 4, 1), stone);
            Shape("Ruin south", PrimitiveType.Cube, new Vector3(-27, 2, 79), new Vector3(12, 4, 1), stone);
            Shape("Ruin partition", PrimitiveType.Cube, new Vector3(-24, 1.7f, 87), new Vector3(1, 3.4f, 11), stone);
            // Station 4: three short jumps, with a wide landing on each step.
            Sign(new Vector3(TrailX(110), 2.2f, 110), "04 / STAR SHRINE\nSpace to jump. Climb the three stones.  >");
            for (int i = 0; i < 3; i++) Shape("Shrine step " + i, PrimitiveType.Cube, new Vector3(2 + i * 3, (i + 1) * .4f, 118), new Vector3(3.1f, (i + 1) * .8f, 5), stone);
            // Station 5: ring of standing stones.
            Sign(new Vector3(TrailX(138), 2.2f, 138), "05 / WISP CIRCLE\nThe final fragment waits to the west.  <");
            for (int i = 0; i < 7; i++)
            {
                float a = i * Mathf.PI * 2 / 7;
                Shape("Standing stone", PrimitiveType.Cube, new Vector3(-29 + Mathf.Sin(a) * 6, 1.8f, 144 + Mathf.Cos(a) * 6), new Vector3(1, 3.6f, 1.2f), stone);
            }
            RenderSettings.fog = true; RenderSettings.fogMode = FogMode.ExponentialSquared; RenderSettings.fogDensity = .012f;
            RenderSettings.fogColor = new Color(.055f, .105f, .16f);
            RenderSettings.ambientMode = AmbientMode.Flat; RenderSettings.ambientLight = new Color(.19f, .25f, .34f);
            var sun = new GameObject("Moon / restored sunlight").AddComponent<Light>(); sun.type = LightType.Directional;
            sun.transform.rotation = Quaternion.Euler(48, -30, 0); sun.color = new Color(.55f, .7f, 1); sun.intensity = .8f; sun.shadows = LightShadows.Soft;
            game = new GameObject("Game systems").AddComponent<GameManager>();
            MakeAudio();
            var restoration = game.gameObject.AddComponent<ForestRestoration>(); restoration.sun = sun; restoration.ground = ground; restoration.leaves = leaf;
            var lights = new GameObject("Awakened forest wisps");
            for (int i = 0; i < 50; i++) Shape("Restored wisp", PrimitiveType.Sphere, new Vector3(Random.Range(-35, 35), Random.Range(1, 5), Random.Range(2, 182)), Vector3.one * .18f, gold, lights.transform, false);
            lights.SetActive(false); restoration.restoredLights = lights; game.restoration = restoration;
        }
        static bool NearStation(float x, float z)
        {
            foreach (Vector2 point in new[] { new Vector2(-10, 32), new Vector2(27, 57), new Vector2(-25, 88), new Vector2(5, 118), new Vector2(-29, 144) })
                if (Vector2.Distance(new Vector2(x, z), point) < 15) return true;
            return false;
        }
        static void Sign(Vector3 position, string text)
        {
            Shape("Wayfinder post", PrimitiveType.Cube, position - Vector3.up * 1.2f, new Vector3(.2f, 2.4f, .2f), bark);
            Shape("Wayfinder board", PrimitiveType.Cube, position, new Vector3(6.5f, 1.6f, .2f), stone);
            var obj = new GameObject("Wayfinder inscription"); obj.transform.position = position + Vector3.back * .12f;
            obj.transform.rotation = Quaternion.Euler(0, 180, 0);
            var label = obj.AddComponent<TextMeshPro>(); label.font = font; label.text = text; label.fontSize = 3;
            label.alignment = TextAlignmentOptions.Center; label.color = Parchment;
            label.rectTransform.sizeDelta = new Vector2(6.2f, 1.5f);
            // TMP faces along its local -Z, toward the approaching player.
            obj.transform.rotation = Quaternion.identity;
        }
        static void BuildGameplay()
        {
            var player = new GameObject("Aren"); player.layer = 2; player.transform.position = new Vector3(0, .15f, 0);
            var controller = player.AddComponent<CharacterController>(); controller.height = 1.9f; controller.radius = .38f;
            controller.center = Vector3.up * .95f; controller.stepOffset = .35f; controller.slopeLimit = 48;
            var body = Shape("Aren cloak", PrimitiveType.Capsule, new Vector3(0, 1, 0), new Vector3(.72f, .85f, .72f), Mat("Aren teal", new Color(.15f, .55f, .6f)), player.transform, false); body.layer = 2;
            var head = Shape("Aren hood", PrimitiveType.Sphere, new Vector3(0, 1.65f, .05f), Vector3.one * .5f, Mat("Aren gold", new Color(.9f, .68f, .34f)), player.transform, false); head.layer = 2;
            var nose = Shape("Facing marker", PrimitiveType.Cube, new Vector3(0, 1.55f, .35f), new Vector3(.18f, .14f, .2f), gold, player.transform, false); nose.layer = 2;
            var movement = player.AddComponent<PlayerMovement>(); player.AddComponent<PlayerInteraction>();
            var cameraObj = new GameObject("Third person camera"); cameraObj.tag = "MainCamera";
            var camera = cameraObj.AddComponent<UnityEngine.Camera>(); camera.fieldOfView = 65; camera.nearClipPlane = .1f; camera.farClipPlane = 260;
            camera.clearFlags = CameraClearFlags.SolidColor; camera.backgroundColor = RenderSettings.fogColor;
            cameraObj.AddComponent<AudioListener>(); cameraObj.transform.position = new Vector3(0, 4, -6);
            var follow = cameraObj.AddComponent<ThirdPersonCamera>(); follow.target = player.transform; movement.cameraTransform = cameraObj.transform;
            PrefabUtility.SaveAsPrefabAsset(player, "Assets/Prefabs/Aren.prefab");
            Vector3[] shards = { new Vector3(-10, 1.3f, 34), new Vector3(30, 1.6f, 57), new Vector3(-29, 1.3f, 92), new Vector3(8, 3.5f, 118), new Vector3(-29, 1.3f, 144) };
            for (int i = 0; i < shards.Length; i++) { var obj = Place(fragmentPrefab, shards[i]); obj.name = "Light Fragment " + (i + 1); }
            // Full-width sanctuary wall seals the final grove; the only opening is the gate.
            Shape("Sanctuary west wall", PrimitiveType.Cube, new Vector3(-27, 5, 163), new Vector3(46, 10, 3), stone);
            Shape("Sanctuary east wall", PrimitiveType.Cube, new Vector3(27, 5, 163), new Vector3(46, 10, 3), stone);
            for (int side = -1; side <= 1; side += 2)
                Shape("Gate pillar", PrimitiveType.Cube, new Vector3(side * 4.3f, 4, 163), new Vector3(1.5f, 8, 2.5f), stone);
            Shape("Gate lintel", PrimitiveType.Cube, new Vector3(0, 8, 163), new Vector3(10, 1.3f, 3), stone);
            var gateObj = new GameObject("Ancient magical gate"); gateObj.transform.position = new Vector3(0, 0, 163);
            var gate = gateObj.AddComponent<MagicalGate>();
            gate.door = Shape("Sealed stone door", PrimitiveType.Cube, new Vector3(0, 3.5f, 163), new Vector3(7, 7, 1), stone, gateObj.transform).transform;
            Shape("Gate rune", PrimitiveType.Cube, new Vector3(0, 4, 162.4f), new Vector3(.15f, 4, .1f), blue, gate.door, false);
            var trigger = gateObj.AddComponent<BoxCollider>(); trigger.isTrigger = true; trigger.center = Vector3.up * 2; trigger.size = new Vector3(11, 5, 11);
            Sign(new Vector3(0, 2.5f, 155), "THE SEALED SANCTUARY\nFive scattered lights will wake the gate.");
            Shape("Crystal dais", PrimitiveType.Cylinder, new Vector3(0, .4f, 179), new Vector3(7, .4f, 7), stone);
            var crystal = new GameObject("Crystal of Light"); crystal.transform.position = new Vector3(0, 1, 179);
            var collectible = crystal.AddComponent<CrystalCollectible>(); collectible.gate = gate;
            var visual = new GameObject("Floating crystal"); visual.transform.SetParent(crystal.transform); visual.transform.localPosition = Vector3.up * 1.8f;
            var gem = Shape("Heart of Eldoria shard", PrimitiveType.Cube, visual.transform.position, new Vector3(1.1f, 2.2f, 1.1f), blue, visual.transform, false); gem.transform.rotation = Quaternion.Euler(30, 0, 40);
            collectible.visual = visual.transform;
            Point(crystal.transform, new Vector3(0, 4, 179), Color.cyan, 16, 4);
            var crystalTrigger = crystal.AddComponent<SphereCollider>(); crystalTrigger.isTrigger = true; crystalTrigger.radius = 4;
            PrefabUtility.SaveAsPrefabAsset(gateObj, "Assets/Prefabs/MagicalGate.prefab");
            PrefabUtility.SaveAsPrefabAsset(crystal, "Assets/Prefabs/CrystalOfLight.prefab");
        }

        static GameObject Panel(string name, Transform parent, Vector2 anchor, Vector2 pivot, Vector2 size, Vector2 offset, Color color)
        {
            var obj = new GameObject(name, typeof(RectTransform), typeof(Image)); obj.transform.SetParent(parent, false);
            var rect = obj.GetComponent<RectTransform>(); rect.anchorMin = rect.anchorMax = anchor; rect.pivot = pivot; rect.sizeDelta = size; rect.anchoredPosition = offset;
            obj.GetComponent<Image>().color = color; return obj;
        }
        static TMP_Text Text(Transform parent, string value, int size, Vector2 position, Vector2 area, TextAlignmentOptions alignment = TextAlignmentOptions.Center)
        {
            var obj = new GameObject(value.Split('\n')[0], typeof(RectTransform), typeof(TextMeshProUGUI)); obj.transform.SetParent(parent, false);
            var rect = obj.GetComponent<RectTransform>(); rect.anchorMin = rect.anchorMax = new Vector2(.5f, .5f); rect.sizeDelta = area; rect.anchoredPosition = position;
            var text = obj.GetComponent<TextMeshProUGUI>(); text.font = font; text.text = value; text.fontSize = size; text.color = Parchment; text.alignment = alignment; text.raycastTarget = false; return text;
        }
        static void Button(Transform parent, string title, float y, UnityEngine.Events.UnityAction action)
        {
            var obj = Panel(title, parent, Vector2.one * .5f, Vector2.one * .5f, new Vector2(320, 52), new Vector2(0, y), new Color(.12f, .26f, .29f));
            var button = obj.AddComponent<Button>(); var colors = button.colors; colors.highlightedColor = new Color(.5f, .8f, .7f); button.colors = colors;
            UnityEventTools.AddPersistentListener(button.onClick, action); Text(obj.transform, title, 21, Vector2.zero, new Vector2(310, 48));
        }
        static void BuildUI()
        {
            var canvas = new GameObject("Fantasy interface", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
            canvas.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            var scaler = canvas.GetComponent<CanvasScaler>(); scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize; scaler.referenceResolution = new Vector2(1600, 900); scaler.matchWidthOrHeight = .5f;
            var events = new GameObject("Input System EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            events.GetComponent<InputSystemUIInputModule>().AssignDefaultActions();
            ui = canvas.AddComponent<UIManager>(); game.ui = ui; canvas.AddComponent<PauseMenu>();
            var menuManager = canvas.AddComponent<MainMenuManager>();
            ui.hud = new GameObject("Gameplay HUD", typeof(RectTransform)); ui.hud.transform.SetParent(canvas.transform, false);
            var hudRect = ui.hud.GetComponent<RectTransform>(); hudRect.anchorMin = Vector2.zero; hudRect.anchorMax = Vector2.one; hudRect.sizeDelta = Vector2.zero;
            var stats = Panel("Journey", ui.hud.transform, new Vector2(0, 1), new Vector2(0, 1), new Vector2(390, 146), new Vector2(24, -24), Ink);
            Text(stats.transform, "MYSTIC FOREST  /  I", 18, new Vector2(0, 46), new Vector2(350, 30), TextAlignmentOptions.Left);
            ui.fragmentText = Text(stats.transform, "Light Fragments: 0/5", 24, new Vector2(0, 4), new Vector2(350, 35), TextAlignmentOptions.Left);
            ui.crystalText = Text(stats.transform, "Crystals Collected: 0/3", 21, new Vector2(0, -35), new Vector2(350, 35), TextAlignmentOptions.Left);
            var objective = Panel("Objective", ui.hud.transform, new Vector2(1, 1), new Vector2(1, 1), new Vector2(460, 125), new Vector2(-24, -24), Ink);
            ui.objectiveText = Text(objective.transform, "", 22, Vector2.zero, new Vector2(415, 105), TextAlignmentOptions.Left);
            var lower = Panel("Interaction and guidance", ui.hud.transform, new Vector2(.5f, 0), new Vector2(.5f, 0), new Vector2(1000, 150), new Vector2(0, 20), new Color(.025f, .055f, .085f, .85f));
            ui.promptText = Text(lower.transform, "", 25, new Vector2(0, 35), new Vector2(950, 44));
            ui.notificationText = Text(lower.transform, "", 21, new Vector2(0, -10), new Vector2(950, 48));
            Text(lower.transform, "WASD  Move     Mouse  Look     Shift  Sprint     Space  Jump     E  Interact     Esc  Pause", 17, new Vector2(0, -53), new Vector2(960, 28));
            ui.menu = ScreenPanel(canvas.transform, "Main menu", new Vector2(760, 730));
            Text(ui.menu.transform, "AN ELDORIA CHRONICLE", 18, new Vector2(0, 294), new Vector2(680, 30));
            Text(ui.menu.transform, "THE LOST\nKINGDOM", 66, new Vector2(0, 184), new Vector2(680, 190));
            Text(ui.menu.transform, "I  /  MYSTIC FOREST", 24, new Vector2(0, 55), new Vector2(680, 40));
            Text(ui.menu.transform, "The Heart of Eldoria is broken.\nAren, gather five lights and awaken the forest.", 22, new Vector2(0, -12), new Vector2(650, 85));
            Button(ui.menu.transform, "Play", -105, game.Play);
            Button(ui.menu.transform, "Settings", -170, ui.OpenSettings);
            Button(ui.menu.transform, "Exit", -235, menuManager.Quit);
            Text(ui.menu.transform, "FIRST PLAYABLE DEMO  ·  Windows PC", 16, new Vector2(0, -315), new Vector2(680, 35));
            ui.pause = ScreenPanel(canvas.transform, "Pause", new Vector2(650, 500));
            Text(ui.pause.transform, "JOURNEY PAUSED", 40, new Vector2(0, 155), new Vector2(610, 80));
            Button(ui.pause.transform, "Resume", 35, game.Resume); Button(ui.pause.transform, "Restart Level", -35, game.Restart); Button(ui.pause.transform, "Main Menu", -105, game.MainMenu);
            ui.complete = ScreenPanel(canvas.transform, "Level complete", new Vector2(820, 650));
            Text(ui.complete.transform, "LEVEL 1 COMPLETE!", 44, new Vector2(0, 225), new Vector2(780, 80));
            Text(ui.complete.transform, "Crystal of Light Collected – 1/3\n\nMystic Forest has been restored.", 27, new Vector2(0, 92), new Vector2(760, 155));
            Text(ui.complete.transform, "Level 2 – Ancient Cave\nwill be available in a future version.", 22, new Vector2(0, -55), new Vector2(760, 90));
            Button(ui.complete.transform, "Restart Level", -165, game.Restart); Button(ui.complete.transform, "Main Menu", -235, game.MainMenu);
            ui.settings = ScreenPanel(canvas.transform, "Settings", new Vector2(650, 550));
            Text(ui.settings.transform, "SETTINGS", 40, new Vector2(0, 200), new Vector2(600, 70));
            Slider(ui.settings.transform, "Mouse sensitivity", 95, .3f, 2.5f, PlayerPrefs.GetFloat("Sensitivity", 1), menuManager.Sensitivity);
            Slider(ui.settings.transform, "Volume", -25, 0, 1, PlayerPrefs.GetFloat("Volume", .65f), menuManager.Volume);
            Button(ui.settings.transform, "Back", -185, ui.CloseSettings);
            ui.ShowMenu();
        }
        static GameObject ScreenPanel(Transform canvas, string name, Vector2 size)
        { return Panel(name, canvas, Vector2.one * .5f, Vector2.one * .5f, size, Vector2.zero, Ink); }
        static void Slider(Transform parent, string label, float y, float min, float max, float value, UnityEngine.Events.UnityAction<float> change)
        {
            Text(parent, label, 22, new Vector2(0, y + 35), new Vector2(530, 40));
            var track = Panel(label, parent, Vector2.one * .5f, Vector2.one * .5f, new Vector2(400, 12), new Vector2(0, y), new Color(.2f, .35f, .37f));
            var handle = Panel("Handle", track.transform, Vector2.one * .5f, Vector2.one * .5f, new Vector2(24, 30), Vector2.zero, Parchment);
            var slider = track.AddComponent<UnityEngine.UI.Slider>(); slider.handleRect = handle.GetComponent<RectTransform>(); slider.targetGraphic = handle.GetComponent<Image>();
            slider.minValue = min; slider.maxValue = max; slider.value = value; UnityEventTools.AddPersistentListener(slider.onValueChanged, change);
        }
        static void MakeAudio()
        {
            const int rate = 22050, samples = 15435;
            string file = "Assets/Audio/FragmentChime.wav";
            using (var writer = new BinaryWriter(File.Create(file)))
            {
                writer.Write(System.Text.Encoding.ASCII.GetBytes("RIFF")); writer.Write(36 + samples * 2);
                writer.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt ")); writer.Write(16); writer.Write((short)1); writer.Write((short)1);
                writer.Write(rate); writer.Write(rate * 2); writer.Write((short)2); writer.Write((short)16);
                writer.Write(System.Text.Encoding.ASCII.GetBytes("data")); writer.Write(samples * 2);
                for (int i = 0; i < samples; i++)
                {
                    float t = (float)i / rate;
                    float envelope = Mathf.Min(t * 80, 1) * Mathf.Exp(-t * 7);
                    writer.Write((short)((Mathf.Sin(t * 880 * Mathf.PI * 2) + .4f * Mathf.Sin(t * 1320 * Mathf.PI * 2)) * envelope * 7000));
                }
            }
            AssetDatabase.ImportAsset(file);
            var source = game.gameObject.AddComponent<AudioSource>(); source.playOnAwake = false;
            source.clip = AssetDatabase.LoadAssetAtPath<AudioClip>(file); source.volume = .6f; game.collectionAudio = source;
        }
    }
}
