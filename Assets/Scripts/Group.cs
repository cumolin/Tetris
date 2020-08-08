using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Group : MonoBehaviour
{
    private float lastFall = 0;


    // Start is called before the first frame update
    void Start()    //遊戲開始時檢查是否game over
    {
        if (!isValidGridPos())
        {
            //Debug.Log("Game Over");
            gameover();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()   //移動group
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (isValidGridPos())
                updateGrid();
            else
                transform.position += new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if(this.tag != "GroupO")
                transform.Rotate(0, 0, -90);
            if (isValidGridPos())
                updateGrid();
            else
                transform.Rotate(0, 0, 90);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Time.time - lastFall >= (1.1 - Grid.level * 0.1))
        {
            transform.position += new Vector3(0, -1, 0);
            if (isValidGridPos())
                updateGrid();
            else
            {
                transform.position += new Vector3(0, 1, 0);

                Grid.deleteFullRows();

                FindObjectOfType<Spawner>().SpawnNext();

                enabled = false;
            }

            lastFall = Time.time;
        }
    }

    bool isValidGridPos()   //檢查下個位置是否可放置方塊
    {
        foreach(Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);

            if (!Grid.insideBorder(v))
            {
                return false;
            }

            if(Grid.grid[(int)v.x, (int)v.y] != null && Grid.grid[(int)v.x, (int)v.y].parent != transform)
            {
                return false;
            }
        }
        return true;
    }

    void updateGrid()   //更新網格數據
    {
        for(int y = 0; y < Grid.h; y++)
        {
            for(int x = 0; x < Grid.w; x++)
            {
                if (Grid.grid[x, y] != null)
                    if (Grid.grid[x, y].parent == transform)
                        Grid.grid[x, y] = null;
            }
        }
        foreach (Transform child in transform)
        {
            Vector2 v = Grid.roundVec2(child.position);
            Grid.grid[(int)v.x, (int)v.y] = child;
        }
    }

    void gameover()
    {
        Debug.Log("Game Over");
        GameObject.Find("Canvas/UI/GameOverPanel").SetActive(true);
    }
}
