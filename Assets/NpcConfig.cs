using UnityEngine;

using static NpcSystem.NpcTypes;

namespace NpcSystem
{
    sealed class NpcConfig : MonoBehaviour
    {
        [SerializeField , Tooltip("prefab for the weapon")] private GameObject WeaponPrefab;
        [SerializeField, Tooltip("position for the weapon to spawn")] private Transform WeaponSpawnLocation;
        [SerializeField , Tooltip("prefabs of the implemented animated characters")] private GameObject[] characters; 
        [SerializeField , Tooltip("location for the animated character to spanw(NOT world position)")] private Transform charactersSpawn;
        
        //<summary>
        //set everything before the character spawns
        //</summary>
        //<param name ="type"> will pass what kind of character will be spawned</param>
        public void SetNpc(NpcType type)
        {
            Debug.LogWarning("skins not yet implemented");
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
