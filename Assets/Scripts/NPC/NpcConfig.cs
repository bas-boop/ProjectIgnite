using UnityEngine;

using Environment;
using Framework;
using Framework.Gameplay.MiniGames;

namespace NPC
{
    public sealed class NpcConfig : MonoBehaviour
    {
        [SerializeField] private GameObject weaponPrefab;
        [SerializeField] private GameObject weaponUI;
        [SerializeField] private Timer timer;
        [SerializeField] private MiniGameSystem miniGameSystem;
        [SerializeField] private Transform weaponSpawnLocation;
        [SerializeField, Tooltip("Location for the animated character to spawn (NOT world position)")] private Transform charactersSpawn;
        [SerializeField, Tooltip("How long the player has to solve this puzzle")] private float solveTime;
        
        public NpcType NpcType { get; private set;}

        //<summary>
        // Set everything before the character spawns
        //</summary>
        //<param name ="type">Will pass what kind of character will be spawned</param>
        public void SetNpc(NpcType type)
        {
            NpcType = type;
            switch (type)
            {
                case NpcType.Armed:
                {
                    GameObject weapon = Instantiate(weaponPrefab, weaponSpawnLocation.position, Quaternion.identity, weaponSpawnLocation);
                    weapon.SetActive(false);
                    
                    Weapon w = weapon.GetComponentInChildren<Weapon>();
                    w.WeaponUI = weaponUI;
                    timer.SetTimerTarget(solveTime);
                    w.WeaponTimer = timer;
                    w.miniGameSystem = miniGameSystem;
                    break;
                }
                case NpcType.Normal:
                default:
                    break;
            }   
        }
    }
}
