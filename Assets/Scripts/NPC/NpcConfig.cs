using UnityEngine;
using Framework;

namespace NpcSystem
{
    sealed class NpcConfig : MonoBehaviour
    {
        [SerializeField] private GameObject WeaponPrefab;
        [SerializeField] private Transform WeaponSpawnLocation;
        [SerializeField , Tooltip("Prefabs of the implemented animated characters")] private GameObject[] characters; 
        [SerializeField , Tooltip("Location for the animated character to spanw(NOT world position)")] private Transform charactersSpawn;
        [SerializeField,Tooltip("How long the player has to solve this puzzle")] private float solveTime ;

        private void Awake()
        {
            SetNpc(NpcType.Armed);
            if (characters.Length == 0)
            {
                Debug.LogWarning("Skins not yet implemented");
            }
        }

        //<summary>
        //set everything before the character spawns
        //</summary>
        //<param name ="type"> will pass what kind of character will be spawned</param>
        public void SetNpc(NpcType type)
        {
            switch (type)
            {
                case NpcType.Armed:
                {
                    GameObject weapon = Instantiate(WeaponPrefab, WeaponSpawnLocation.position, Quaternion.identity, WeaponSpawnLocation);
                    Timer weaponTimer = weapon.GetComponentInChildren<Timer>();
                    if (weaponTimer != null) weaponTimer.SetTimerTarget(solveTime);
                    break;
                }       
            }   
        }
        
        private void SetRandomSkin()
        {
            int randomIndex = Random.Range(0, characters.Length);
            Instantiate(characters[randomIndex], charactersSpawn.position , Quaternion.identity, gameObject.transform);
        }
    }
}
