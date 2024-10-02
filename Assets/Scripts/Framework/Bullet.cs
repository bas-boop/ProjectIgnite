using System;
using UnityEngine;

using NPC;

namespace Framework
{
    public sealed class Bullet : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer != 6) 
                return;
            
            other.gameObject.GetComponent<Emotion>().MakeHappy();
            Destroy(gameObject);
        }
    }
}