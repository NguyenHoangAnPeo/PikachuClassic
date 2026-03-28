using UnityEngine;

public class Node
{
    public Cell cell;
    public Vector2Int dir;
    public int turn;

    public Node(Cell cell,Vector2Int dir,int turn)
    {
        this.cell = cell;
        this.dir = dir;
        this.turn = turn;
    }
}
