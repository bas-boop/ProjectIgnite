using UnityEngine;

using Environment;
using Framework.Extensions;

namespace Player
{
    public sealed class Interacter : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;

        public void CheckInteraction(float range)
        {
            foreach (Weapon weapon in weapons)
            {
                if (!weapon.transform.position.IsWithinRange(transform.position, range))
                    continue;
                
                weapon.Interact();
                break;
            }
        }
    }
}