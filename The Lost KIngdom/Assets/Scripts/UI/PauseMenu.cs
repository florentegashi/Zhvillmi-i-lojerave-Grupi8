using UnityEngine;
namespace LostKingdom
{
    public class PauseMenu : MonoBehaviour
    {
        void Update() { if (DemoInput.Pause) GameManager.Instance.Pause(GameManager.Instance.State != GameManager.GameState.Paused); }
    }
}
