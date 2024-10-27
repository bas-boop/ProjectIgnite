using System.Collections.Generic;
using UnityEngine;

namespace NpcSystem.CrowdSystem
{
    public sealed class CrowdSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject npcPrefab;
        [SerializeField , Tooltip("The space between the characters")] private float spacing = 1.0f;
        [SerializeField , Tooltip("A point to mark the start of the crowd")] private Vector3 startPoint;
        [SerializeField , Tooltip("A Point to mark the end of the crowd")] private Vector3 endPoint;
        [SerializeField, Tooltip("The ammount of armed characters that should spawn")] private int armedCharacterAmount;
        [SerializeField] private bool debugGizmos;

        private void Start()
        {
            SpawnCrowd();   
        }

        /// <summary>
        /// Spawns the crowd in the scene and sets the npc's to the configured settings
        /// </summary>
        public void SpawnCrowd()
        {
            float totalDistance = Vector3.Distance(startPoint, endPoint);
            int npcCount = Mathf.FloorToInt(totalDistance / (npcPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x + spacing));
            List<NpcConfig> npc = new();
            Vector3 direction = (endPoint - startPoint).normalized;

            for (int i = 0; i < npcCount; i++)
            {
                Vector3 spawnPosition = startPoint + direction * (i * (npcPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x + spacing));
                var character = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
                var component = character.GetComponent<NpcConfig>();
                npc.Add(component);
            }
            for (int i = 0; i < armedCharacterAmount; i++)
            {
                SetArmedNpc(npc);
            }
        }

        ///<summary> 
        ///Sets the npc to a armed variant
        ///</summary>
        /// <param name="npcConfigs">contains the list of all the current nps's </param> 
        private void SetArmedNpc(List<NpcConfig> npcConfigs)
        {
            int randomIndex = Random.Range(0, npcConfigs.Count);

            if (npcConfigs[randomIndex].npcType != NpcType.Armed)
            {
                npcConfigs[randomIndex].SetNpc(NpcType.Armed);
            }
        }

        private void OnDrawGizmos()
        {
            if (debugGizmos)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(startPoint, 0.1f); 
                Gizmos.DrawLine(startPoint, startPoint + Vector3.up); 

                Gizmos.color = Color.red;
                Gizmos.DrawSphere(endPoint, 0.1f); 
                Gizmos.DrawLine(endPoint, endPoint + Vector3.up);

                Gizmos.color = Color.yellow;
                Gizmos.DrawLine(startPoint, endPoint);
            }
        }
    }
}
