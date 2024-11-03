using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Environment;
using Framework.Extensions;

namespace Player
{
    public sealed class Interacter : MonoBehaviour
    {
        [SerializeField] private List<Weapon> weapons;

        public void CheckInteraction(float range)
        {
            foreach (Weapon weapon in weapons.Where(weapon =>
                         weapon.transform.position.IsWithinRange(transform.position, range)))
            {
                if (weapon.Interact())
                    break;
            }
        }

        public void AddWeapon(Weapon target)
        {
            if (weapons.Contains(target))
                return;

            weapons.Add(target);
        }

        public void RemoveWeapon(Weapon target)
        {
            if (weapons.Contains(target))
                weapons.Remove(target);
        }
    }
}