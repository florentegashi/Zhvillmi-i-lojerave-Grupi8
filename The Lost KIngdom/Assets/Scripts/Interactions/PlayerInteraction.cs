using System.Collections.Generic;
using UnityEngine;

namespace LostKingdom
{
    public class PlayerInteraction : MonoBehaviour
    {
        readonly List<InteractionTarget> nearby = new List<InteractionTarget>();
        public void Register(InteractionTarget target) { if (!nearby.Contains(target)) nearby.Add(target); }
        public void Unregister(InteractionTarget target) { nearby.Remove(target); }
        void Update()
        {
            if (!GameManager.Instance.IsPlaying) return;
            InteractionTarget closest = null;
            float best = float.MaxValue;
            foreach (var item in nearby)
            {
                if (!item || !item.Available) continue;
                float d = (item.transform.position - transform.position).sqrMagnitude;
                if (d < best) { best = d; closest = item; }
            }
            GameManager.Instance.ui.SetPrompt(closest ? closest.Prompt : "");
            if (closest && DemoInput.Interact) closest.Interact();
        }
    }
}
