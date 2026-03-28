using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] protected GridSpawner gridSpawner;
    public GridSpawner GridSpawner => gridSpawner;

    int maxCountTurn = 2;

    int countTurn = 0;

    private IEnumerator Start()
    {
        yield return new WaitUntil(() => gridSpawner.grid != null);

        Cell start = gridSpawner.grid[1, 1];
        Cell end = gridSpawner.grid[2, 1];

        BFS(start, end);
    }
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
    public void BFS(Cell start, Cell end)
    {
        Queue<Node> queue = new Queue<Node>();

        bool[,,] visited = new bool[gridSpawner.width, gridSpawner.height, 4];

        queue.Enqueue(new Node(start, Vector2Int.zero, 0));

        while (queue.Count > 0)
        {
            Node currentNode = queue.Dequeue();
            Cell current = currentNode.cell;

            if (current == end)
            {
                Debug.Log("Found!");
                return;
            }

            foreach (var next in GetNeighbors(current))
            {
                if (next == null || next.isBlocked) continue;

                Vector2Int newDir = new Vector2Int(next.x, next.y) - new Vector2Int(current.x, current.y);
                int dirIndex = GetDirIndex(newDir); 

                int newTurn = currentNode.turn;

                if (currentNode.dir != Vector2Int.zero && newDir != currentNode.dir)
                    newTurn++;

                if (newTurn > maxCountTurn) continue;

                if (dirIndex != -1 && visited[next.x, next.y, dirIndex]) continue;

                if (dirIndex != -1)
                    visited[next.x, next.y, dirIndex] = true;

                next.sprite.color = Color.green;

                queue.Enqueue(new Node(next, newDir, newTurn));
            }
        }
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
}
