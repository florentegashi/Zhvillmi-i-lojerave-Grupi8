using UnityEngine;
using UnityEngine.InputSystem;

namespace LostKingdom
{
    // Polling the installed Input System keeps this small demo independent of action assets.
    public static class DemoInput
    {
        public static Vector2 Move => Keyboard.current == null ? Vector2.zero : new Vector2(
            (Keyboard.current.dKey.isPressed ? 1 : 0) - (Keyboard.current.aKey.isPressed ? 1 : 0),
            (Keyboard.current.wKey.isPressed ? 1 : 0) - (Keyboard.current.sKey.isPressed ? 1 : 0));
        public static Vector2 Look => Mouse.current == null ? Vector2.zero : Mouse.current.delta.ReadValue();
        public static bool Jump => Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame;
        public static bool Sprint => Keyboard.current != null && Keyboard.current.leftShiftKey.isPressed;
        public static bool Interact => Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame;
        public static bool Pause => Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame;
    }
}
