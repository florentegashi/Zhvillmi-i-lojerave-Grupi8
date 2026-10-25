using UnityEngine;

namespace LostKingdom
{
    public class CrystalCollectible : InteractionTarget
    {
        public MagicalGate gate;
        public Transform visual;
        bool collected;
        public override bool Available => !collected && gate.IsOpen;
        public override string Prompt => "Press E to collect the Crystal of Light.";
        void Update()
        {
            if (!GameManager.Instance.IsPlaying) return;
            visual.Rotate(0, 30 * Time.deltaTime, 0);
            visual.localPosition = Vector3.up * (1.8f + Mathf.Sin(Time.time * 1.5f) * .2f);
        }
        public override void Interact()
        {
            if (!Available) return;
            collected = true; visual.gameObject.SetActive(false);
            CollectionEffect.Play(transform.position + Vector3.up * 2, Color.cyan);
            GameManager.Instance.CollectCrystal();
        }
    }
}
