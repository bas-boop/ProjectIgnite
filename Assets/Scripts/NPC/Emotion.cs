using UnityEngine;

namespace NPC
{
    public sealed class Emotion : MonoBehaviour
    {
        [SerializeField] private GameObject sadFace;
        [SerializeField] private GameObject happyFace;

        public bool IsHappy { get; private set; }

        private void Start()
        {
            sadFace.SetActive(true);
            happyFace.SetActive(false);
        }

        public void MakeHappy()
        {
            happyFace.SetActive(true);
            sadFace.SetActive(false);
            IsHappy = true;
        }
    }
}