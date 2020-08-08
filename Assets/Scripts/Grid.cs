using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{
    public static Grid _instance;
    public delegate void OnDeleteValueChanged(int deletedrows, int level);
    public event OnDeleteValueChanged OnChanged;

    public static int w = 10;   //遊戲寬度
    public static int h = 30;   //遊戲高度
    public static Transform[,] grid = new Transform[w, h];

    public static int deletedRows = 0;
    public static int level = 1;



    private void Awake()
    {
        _instance = this;
        
    }

    public static Vector2 roundVec2(Vector2 v)  //四捨五入
    {
        return new Vector2(Mathf.Round(v.x), Mathf.Round(v.y));
    }

    public static bool insideBorder(Vector2 pos)    //檢查方塊是在邊界內
    {
        return ((int)pos.x >= 0 && (int)pos.x < w && (int)pos.y >= 0);
    }

    public static void deleteRow(int y) //刪除第y列
    {
        for(int x = 0; x < w; x++)
        {
            Destroy(grid[x, y].gameObject);
            grid[x, y] = null;
            
        }
        deletedRows++;
        level = deletedRows / 10 + 1;
        _instance.OnChanged(deletedRows, level);
    }

    public static void decreaseRow(int y)   //將第y列下移到第y-1列
    {
        for(int x = 0; x < w; x++)
        {
            if(grid[x, y] != null)
            {
                grid[x, y - 1] = grid[x, y];
                grid[x, y] = null;

                grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    public static void decreaseRowsAbove(int y) //將第y列以上每一列都下移一列
    {
        for(int i = y; i < h; i++)
        {
            decreaseRow(i);
        }
    }

    public static bool isRowFull(int y) //檢查第y列是否已滿
    {
        for (int x = 0; x < w; x++)
        {
            if (grid[x, y] == null)
                return false;
        }
        return true;
    }

    public static void deleteFullRows() //刪除所有已滿的列
    {
        for(int y = 0; y < h; y++)
        {
            if (isRowFull(y))
            {
                deleteRow(y);
                decreaseRowsAbove(y + 1);
                y--;
            }
        }
    }

}
