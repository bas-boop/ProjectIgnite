using System.Collections;
using UnityEngine;

public class QuickTimeEventSpawner : MonoBehaviour
{
    public GameObject prefab;  
    public QuickTimeAsset quickTimeEvent;
    public Transform spawnLocation; 
    private bool isSpawning = true; 

    private void Start()
    {
        StartCoroutine(WaitAndStartCoroutine());
    }

    private IEnumerator WaitAndStartCoroutine()
    {
        while (isSpawning)
        {
            float randomStartDelay = Random.Range(3f, 8f);
            yield return new WaitForSeconds(randomStartDelay);

            StartCoroutine(SpawnObject());
        }
    }

    private IEnumerator SpawnObject()
    {
        GameObject spawnedObject = Instantiate(prefab, spawnLocation.position, Quaternion.identity);
        print("spawn");
        QuickTimeEvent quickTimeObj = spawnedObject.GetComponent<QuickTimeEvent>();
        if (quickTimeObj != null)
        {
            quickTimeObj.SetQTE(quickTimeEvent.KeyCode , quickTimeEvent.Time);
        }

        yield return new WaitForSeconds(quickTimeEvent.Time);
    }

    public void StopSpawning()
    {
        isSpawning = false;   
    }
}
