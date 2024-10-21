using UnityEngine;

namespace NpcSystem
{
    sealed class NpcConfig : MonoBehaviour
    {
        [SerializeField] private GameObject WeaponPrefab;
        [SerializeField] private Transform WeaponSpawnLocation;
        [SerializeField , Tooltip("prefabs of the implemented animated characters")] private GameObject[] characters; 
        [SerializeField , Tooltip("location for the animated character to spanw(NOT world position)")] private Transform charactersSpawn;

        private void Awake()
        {
            if (characters.Length == 0)
            {
                Debug.LogWarning("skins not yet implemented");
            }
            SetNpc(NpcType.Armed);
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
                    Instantiate(WeaponPrefab, WeaponSpawnLocation.position, Quaternion.identity, WeaponSpawnLocation);
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
