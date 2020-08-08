using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] groups;
    public int randomValue;

    public GameObject nextSpawnIcon;

    // Start is called before the first frame update
    void Start()
    {
        randomValue = Random.Range(0, groups.Length);
        SpawnNext();
    }

    public void SpawnNext() //隨機生成group
    {
        

        Instantiate(groups[randomValue], transform.position, Quaternion.identity);

        randomValue = Random.Range(0, groups.Length);

        nextSpawnIcon.GetComponent<NextSpawn>().nextSpawn(randomValue);
    }
}
