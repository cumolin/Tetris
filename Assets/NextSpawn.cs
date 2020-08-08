using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextSpawn : MonoBehaviour
{
    public GameObject[] prefabs;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void nextSpawn(int value)
    {
        //Debug.Log(value);
        for(int i = 0; i < 7; i++)
        {
            this.transform.GetChild(i).gameObject.SetActive(false);
        }
        this.transform.GetChild(value).gameObject.SetActive(true);
    }
}
