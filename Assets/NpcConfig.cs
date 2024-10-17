using UnityEngine;
using static NpcSystem.NpcTypes;

namespace NpcSystem
{
    public class NpcConfig : MonoBehaviour
    {
        [SerializeField] private GameObject WeaponPrefab;
        [SerializeField] private Transform WeaponSpawnLocation;
        [SerializeField] private GameObject[] characters; 
        [SerializeField] private Transform charactersSpawn;
        
        //<summary>
        //set everything before the character spawns
        //</summary>
        //<param name ="type"> will pass what kind of character will be spawned</param>
        public void SetNpc(NpcType type)
        {
            //set skin once assets implemented
            if (type == NpcType.Armed) 
            {
                Instantiate(WeaponPrefab, WeaponSpawnLocation.position, Quaternion.identity, WeaponSpawnLocation);
            }
        }
        
        private void SetRandomSkin()
        {
            int randomIndex = Random.Range(0, characters.Length);
            Instantiate(characters[randomIndex], charactersSpawn.position , Quaternion.identity);
        }

    }
}
