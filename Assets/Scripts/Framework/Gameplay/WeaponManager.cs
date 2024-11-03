using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using Environment;

namespace Framework.Gameplay
{
    public sealed class WeaponManager : MonoBehaviour
    {
        [SerializeField] private List<Weapon> weapons;

        [SerializeField] private UnityEvent onAllDestroyed = new();
        [SerializeField] private UnityEvent onNotAllDestroyed = new();

        public void AddWeapon(Weapon targetWeapon) => weapons.Add(targetWeapon);
        
        public void CheckAllDestroyed()
        {
            if (weapons.Count == 0)
            {
                Debug.LogError("No weapons have been set!");
                return;
            }
            
            bool areAllWeaponsDestroyed = weapons.All(weapon => weapon.IsDestroyed);
            InvokeCorrectEvent(areAllWeaponsDestroyed);
        }

        public void CheckByEachWeapon()
        {
            int count = weapons.Count(weapon => weapon.IsDestroyed);
            
            if (count == weapons.Count)
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