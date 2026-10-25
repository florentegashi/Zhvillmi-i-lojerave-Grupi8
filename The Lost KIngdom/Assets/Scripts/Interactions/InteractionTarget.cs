using UnityEngine;

namespace LostKingdom
{
    public abstract class InteractionTarget : MonoBehaviour
    {
        public abstract string Prompt { get; }
        public virtual bool Available => true;
        public abstract void Interact();
        protected virtual void OnTriggerEnter(Collider other) { var player = other.GetComponent<PlayerInteraction>(); if (player) player.Register(this); }
        protected virtual void OnTriggerExit(Collider other) { var player = other.GetComponent<PlayerInteraction>(); if (player) player.Unregister(this); }
    }
}
