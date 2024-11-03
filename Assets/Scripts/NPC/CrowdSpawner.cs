using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public sealed class CrowdSpawner : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private GameObject npcPrefab;
        
        [Header("Settings")]
        [SerializeField, Tooltip("The space between the characters")] private Vector2 spacing = Vector2.one;
        [SerializeField, Tooltip("A point to mark the start of the crowd")] private Vector3 startPoint;
        [SerializeField, Tooltip("A Point to mark the end of the crowd")] private Vector3 endPoint;
        [SerializeField, Tooltip("The ammount of armed characters that should spawn")] private int armedCharacterAmount;
        [SerializeField, Tooltip("From the start and end tell how many can't be armed, because wall collision")] 
        private int notArmedMargin = 1;
        
        [Header("Debug")]
        [SerializeField] private bool debugGizmos;
        [SerializeField] private Color startColor = Color.green;
        [SerializeField] private Color lineColor = Color.yellow;
        [SerializeField] private Color endColor = Color.red;

        private void Start() => SpawnCrowd(); 
        
        /// <summary>
        /// Spawns the crowd in the scene and sets the npc's to the configured settings
        /// </summary>
        public void SpawnCrowd()
        {
            float totalDistance = Vector3.Distance(startPoint, endPoint);
            int npcCount = Mathf.FloorToInt(totalDistance / 
                            (npcPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x + GetRandomSpacing()));
            List<NpcConfig> npc = new();
            Vector3 direction = (endPoint - startPoint).normalized;

            for (int i = 0; i < npcCount; i++)
            {
                Vector3 spawnPosition = startPoint + direction * 
                    (i * (npcPrefab.GetComponentInChildren<SpriteRenderer>().bounds.size.x + GetRandomSpacing()));
                GameObject character = Instantiate(npcPrefab, spawnPosition, Quaternion.identity, transform);
                character.name += $" {i}";
                NpcConfig component = character.GetComponent<NpcConfig>();
                npc.Add(component);
            }

            for (int i = armedCharacterAmount - 1; i >= 0; i--)
            {
                if (armedCharacterAmount > npc.Count)
                    break;
                
                SetArmedNpc(npc);
            }
        }

        ///<summary> 
        ///Sets the npc to an armed variant
        ///</summary>
        /// <param name="npcConfigs">contains the list of all the current nps's </param> 
        private void SetArmedNpc(List<NpcConfig> npcConfigs)
        {
            int randomIndex = Random.Range(notArmedMargin, npcConfigs.Count - notArmedMargin);

            if (npcConfigs[randomIndex].NpcType == NpcType.Armed) 
                return;
            
            npcConfigs[randomIndex].SetNpc(NpcType.Armed);
            npcConfigs.RemoveAt(randomIndex);
        }

        private float GetRandomSpacing() => Random.Range(spacing.x, spacing.y);

        private void OnDrawGizmos()
        {
            if (!debugGizmos)
                return;
            
            Gizmos.color = startColor;
            Gizmos.DrawSphere(startPoint, 0.1f); 
            Gizmos.DrawLine(startPoint, startPoint + Vector3.up); 

            Gizmos.color = endColor;
            Gizmos.DrawSphere(endPoint, 0.1f); 
            Gizmos.DrawLine(endPoint, endPoint + Vector3.up);

            Gizmos.color = lineColor;
            Gizmos.DrawLine(startPoint, endPoint);
        }
    }
}
