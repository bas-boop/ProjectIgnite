using UnityEngine;

namespace NPC
{
    public sealed class RandomSpriteSelector : MonoBehaviour
    {
        [SerializeField] public Sprite[] sprites;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        
            if (sprites.Length > 0)
                SetRandomMaterial();
            else
                Debug.LogError("No materials found in the materials array. Please assign some materials.");
        }

        private void SetRandomMaterial()
        {
            int randomNumber = Random.Range(0, sprites.Length);
            _spriteRenderer.sprite = sprites[randomNumber];
        }
    }
}