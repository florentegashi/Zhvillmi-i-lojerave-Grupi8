using System.Collections;
using UnityEngine;

namespace LostKingdom
{
    public class MagicalGate : InteractionTarget
    {
        public Transform door;
        public bool IsOpen { get; private set; }
        public override bool Available => !IsOpen;
        public override string Prompt => GameManager.Instance.Fragments < 5 ? "Collect all 5 light fragments to unlock this gate." : "Press E to open the gate.";
        public override void Interact()
        {
            if (IsOpen || GameManager.Instance.Fragments < 5) return;
            IsOpen = true; StartCoroutine(Open());
            GameManager.Instance.ui.Notify("The path to the Crystal of Light is open.");
        }
        IEnumerator Open()
        {
            Vector3 start = door.localPosition;
            for (float t = 0; t < 2; t += Time.deltaTime)
            { door.localPosition = start + Vector3.up * (7 * Mathf.SmoothStep(0, 1, t / 2)); yield return null; }
            door.gameObject.SetActive(false);
            GameManager.Instance.ui.SetObjective("Enter the sanctuary and claim the Crystal of Light.");
        }
    }
}
