using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueUI : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        Grid._instance.OnChanged += deleterowsvalueChange;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void deleterowsvalueChange(int deletedrows, int level)
    {
        this.transform.GetChild(0).GetComponentInChildren<Text>().text = deletedrows.ToString();
        this.transform.GetChild(1).GetComponentInChildren<Text>().text = level.ToString();
    }


    private void OnDisable()
    {
        Grid._instance.OnChanged -= deleterowsvalueChange;

    }
}
