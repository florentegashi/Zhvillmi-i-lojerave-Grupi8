using UnityEngine;

namespace LostKingdom
{
    public static class CollectionEffect
    {
        public static void Play(Vector3 position, Color color)
        {
            var obj = new GameObject("Light burst"); obj.transform.position = position;
            var ps = obj.AddComponent<ParticleSystem>();
            var main = ps.main; main.loop = false; main.duration = .5f; main.startLifetime = .9f;
            main.startSpeed = 3; main.startSize = .15f; main.startColor = color;
            var emission = ps.emission; emission.rateOverTime = 0; emission.SetBursts(new[] { new ParticleSystem.Burst(0, 35) });
            var shape = ps.shape; shape.shapeType = ParticleSystemShapeType.Sphere; shape.radius = .3f;
            var renderer = ps.GetComponent<ParticleSystemRenderer>();
            renderer.sharedMaterial = Resources.Load<Material>("LostKingdomSpark");
            ps.Play(); Object.Destroy(obj, 2);
        }
    }
}
