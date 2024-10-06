using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using Environment;

namespace Framework.Gameplay
{
    public sealed class WeaponManager : MonoBehaviour
    {
        [SerializeField] private Weapon[] weapons;

        [SerializeField] private UnityEvent onAllDestroyed = new();
        [SerializeField] private UnityEvent onNotAllDestroyed = new();

        public void CheckAllDestroyed()
        {
            bool areAllWeaponsDestroyed = weapons.All(weapon => weapon.IsDestroyed);
            InvokeCorrectEvent(areAllWeaponsDestroyed);
        }

        public void CheckByEachWeapon()
        {
            int count = weapons.Count(weapon => weapon.IsDestroyed);
            
            if (count == weapons.Length)
                InvokeCorrectEvent(true);
        }

        private void InvokeCorrectEvent(bool success)
        {
            UnityEvent eventToInvoke = success 
                ? onAllDestroyed 
                : onNotAllDestroyed;
            eventToInvoke?.Invoke();
        }
    }
}