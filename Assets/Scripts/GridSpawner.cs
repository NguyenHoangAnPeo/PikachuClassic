using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject holders;

    public int width = 7;
    public int height = 7;

    float cellSize = 1.2f;

    public Cell[,] grid;

    protected void Start()
    {
        float offsetX = (width - 1) / 2f;
        float offsetY = (height - 1) / 2f;

        grid = new Cell[width, height];

        for (int x = 0;x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                float posX = (x - offsetX) * cellSize;
                float posY = (y - offsetY) * cellSize;

                Vector3 pos = new Vector3(posX,posY, 0);

                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);

                Cell data = cell.GetComponent<Cell>();
                data.Init(x, y, width, height);
                //Debug.Log("("+x + "," + y+")");

                grid[x, y] = data;

                cell.transform.parent = this.holders.transform;
            }
        }
    }
}
