using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using Environment;

namespace Framework.Gameplay
{
    public sealed class WeaponManager : MonoBehaviour
    {
        [SerializeField] private float spawnOtherWeaponTime = 1;
        [SerializeField] private List<Weapon> weapons;

        [SerializeField] private UnityEvent onAllDestroyed = new();
        [SerializeField] private UnityEvent onNotAllDestroyed = new();

        public void StartSpawningWeapons()
        {
            List<Weapon> ws = weapons;
            
            for (int i = ws.Count - 1; i >= 0; i--)
            {
                if (ws[i].IsActive)
                    ws.RemoveAt(i);
            }
            
            if (ws.Count == 0)
                return;
            
            ws[Random.Range(0, ws.Count)].SpawnWeapon();
            Invoke(nameof(StartSpawningWeapons), spawnOtherWeaponTime);
        }
        
        public void AddWeapon(Weapon targetWeapon)
        {
            if (weapons.Contains(targetWeapon)) 
                return;
            
            weapons.Add(targetWeapon);
        }

        public void RemoveWeapon(Weapon targetWeapon)
        {
            if (weapons.Contains(targetWeapon)) 
                weapons.Remove(targetWeapon);
        }

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