using UnityEngine;

namespace LostKingdom
{
    public class LightFragment : MonoBehaviour
    {
        Vector3 origin;
        bool collected;
        void Start() { origin = transform.position; }
        void Update()
        {
            if (!GameManager.Instance.IsPlaying) return;
            transform.Rotate(0, 50 * Time.deltaTime, 0, Space.World);
            transform.position = origin + Vector3.up * (Mathf.Sin(Time.time * 2) * .15f);
        }
        void OnTriggerEnter(Collider other)
        {
            if (collected || !other.GetComponent<PlayerMovement>() || !GameManager.Instance.IsPlaying) return;
            collected = true;
            GameManager.Instance.CollectFragment();
            CollectionEffect.Play(transform.position, new Color(.3f, 1, .85f));
            gameObject.SetActive(false);
        }
    }
}
