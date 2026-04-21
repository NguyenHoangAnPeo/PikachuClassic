using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] protected static GridManager instance;
    public static GridManager Instance => instance;
    [SerializeField] protected GridSpawner gridSpawner;
    public GridSpawner GridSpawner => gridSpawner;
    [SerializeField] protected TileMatchController tileMatchController;
    public TileMatchController TileMatchController => tileMatchController;

    int maxCountTurn = 2;

    int countTurn = 0;

    private void Awake()
    {
        if(GridManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        GridManager.instance = this;

        this.tileMatchController = transform.GetComponentInChildren<TileMatchController>();
    }
    //private IEnumerator Start()
    //{
    //    yield return new WaitUntil(() => gridSpawner.grid != null);

    //    Cell start = gridSpawner.grid[1, 1];
    //    Cell end = gridSpawner.grid[4, 5];

    //    var path = FindShortestPath(start, end);
    //    HighlightPath(path);
    //}
    public List<Cell> GetNeighbors(Cell cell)
    {
        List<Cell> neighbors = new List<Cell>();

        int x = cell.x;
        int y = cell.y;

        // UP
        if (y + 1 < gridSpawner.height)
            neighbors.Add(gridSpawner.grid[x, y + 1]);

        // DOWN
        if (y - 1 >= 0)
            neighbors.Add(gridSpawner.grid[x, y - 1]);

        // LEFT
        if (x - 1 >= 0)
            neighbors.Add(gridSpawner.grid[x - 1, y]);

        // RIGHT
        if (x + 1 < gridSpawner.width)
            neighbors.Add(gridSpawner.grid[x + 1, y]);

        return neighbors;
    }
    
    public Vector2Int LastVector(Cell current, Cell next, Vector2Int lastVector)
    {
        Vector2Int dir = new Vector2Int(next.x, next.y) - new Vector2Int(current.x, current.y);

        if (lastVector == Vector2Int.zero)
            return dir;

        if (dir != lastVector)
            countTurn++;

        return dir;
    }
    public bool IsMaxTurnCount(int countTurn)
    {
        if (countTurn == maxCountTurn) return true;
        return false;
    }
    int GetDirIndex(Vector2Int dir)
    {
        if (dir == Vector2Int.up) return 0;
        if (dir == Vector2Int.down) return 1;
        if (dir == Vector2Int.left) return 2;
        if (dir == Vector2Int.right) return 3;
        return -1;
    }
    public List<Cell> FindShortestPath(Cell start, Cell end)
    {
        Queue<State> q = new Queue<State>();
        bool[,,] visited = new bool[gridSpawner.width, gridSpawner.height, 4];
        Dictionary<State, State> parent = new Dictionary<State, State>(new StateComparer());

        State startState = new State(start, Vector2Int.zero, 0);
        q.Enqueue(startState);

        State found = default(State);
        bool hasFound = false;

        while (q.Count > 0)
        {
            State cur = q.Dequeue();

            if (cur.cell == end)
            {
                found = cur;
                hasFound = true;
                break;
            }

            foreach (var next in GetNeighbors(cur.cell))
            {
                if (next == null || next.isBlocked) continue;

                Vector2Int newDir = new Vector2Int(next.x - cur.cell.x, next.y - cur.cell.y);
                int dirIndex = GetDirIndex(newDir);
                int newTurn = cur.turn;

                if (cur.dir != Vector2Int.zero && newDir != cur.dir) newTurn++;
                if (newTurn > maxCountTurn) continue;
                if (dirIndex != -1 && visited[next.x, next.y, dirIndex]) continue;

                if (dirIndex != -1) visited[next.x, next.y, dirIndex] = true;

                State nxt = new State(next, newDir, newTurn);
                parent[nxt] = cur;
                q.Enqueue(nxt);
            }
        }

        if (!hasFound) return null;

        List<Cell> path = new List<Cell>();
        State p = found;
        path.Add(p.cell);

        while (!(p.cell == start && p.dir == Vector2Int.zero && p.turn == 0))
        {
            p = parent[p];
            path.Add(p.cell);
        }

        path.Reverse();
        return path;
    }
    public void HighlightPath(List<Cell> path)
    {
        if (path == null) return;
        foreach (var c in path)
            c.sprite.color = Color.green;
    }
    public void UnHighlightPath(List<Cell> path)
    {
        if (path == null) return;

        Color color;
        ColorUtility.TryParseHtmlString("#8A8A8A", out color);

        foreach (var c in path)
        {
            if (c == null || c.sprite == null) continue;
            c.sprite.color = color;
        }
    }
}
