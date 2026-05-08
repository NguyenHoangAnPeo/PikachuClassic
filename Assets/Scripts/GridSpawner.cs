using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public GameObject cellPrefab;
    public GameObject holders;

    [SerializeField] protected List<Transform> listCell;
    public List<Transform> ListCell => listCell;

    [SerializeField] protected PokemonSpawner pokemonSpawner;
    public PokemonSpawner PokemonSpawner => pokemonSpawner;

    [SerializeField] protected int playableCellCount = 0;
    public int PlayableCellCount => playableCellCount;

    public int width = 7;
    public int height = 7;

    float cellSize = 1.2f;

    public Cell[,] grid;

    [SerializeField] protected List<int> pairIds;
    public List<int> PairId => pairIds;

    protected void Awake()
    {
        this.SetPlayableCellCount();
        this.LoadPairIds();
        this.SpawnGrid();
        this.SpawnPokemon();
    }
    protected virtual void LoadPairIds()
    {
        for(int i = 0; i < playableCellCount / 2; ++i)
        {
            List<int> keys = new List<int>(pokemonSpawner.PokemonDict.Keys);
            int randomId = keys[Random.Range(0, keys.Count)];

            pairIds.Add(randomId);
            pairIds.Add(randomId);

            Shuffle(pairIds);
        }
    }
    protected virtual void SpawnGrid()
    {
        float offsetX = (width - 1) / 2f;
        float offsetY = (height - 1) / 2f;

        grid = new Cell[width, height];
        if (GridManager.Instance == null) return;
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float posX = (x - offsetX) * cellSize;
                float posY = (y - offsetY) * cellSize;

                Vector3 pos = new Vector3(posX, posY, 0);

                GameObject cell = Instantiate(cellPrefab, pos, Quaternion.identity);

                if(!IsBorder(x, y, width, height))
                {
                    listCell.Add(cell.transform);
                }

                Cell data = cell.GetComponent<Cell>();
                data.Init(x, y,this.IsBorder(x,y,width,height));
                //Debug.Log("("+x + "," + y+")");

                grid[x, y] = data;

                cell.transform.parent = this.holders.transform;
            }
        }
    }
    protected virtual bool IsBorder(int x, int y, int width, int height)
    {
        if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
        {
            return true;
        }
        else return false;
    }
    protected virtual void SetPlayableCellCount()
    {
        playableCellCount = (width - 2) * (height - 2);

        if(playableCellCount % 2 != 0)
        {
            Debug.LogWarning("So o pokemon bi le");
        }
        else
        {
            Debug.Log("So o pokemon hop le");
        }
    }
    void Shuffle(List<int> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int randomIndex = Random.Range(i, list.Count);

            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
    protected void SpawnPokemon()
    {
        for(int i = 0;i < pairIds.Count;i++)
        {
            Transform cell = listCell[i];
            GridManager.Instance.PokemonSpawner.SpawnPokemonById(GetId(i), cell.transform.position, cell.transform.rotation, cell.transform);
        }
    }
    protected virtual int GetId(int i)
    {
        return pairIds[i];
    }
    //ToDO bool IsEndGame return pokemonCount;
}
