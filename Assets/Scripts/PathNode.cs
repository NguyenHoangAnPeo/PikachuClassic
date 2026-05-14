using System.Collections.Generic;
using UnityEngine;

struct PathNode
{
    public Cell cell;
    public Vector2Int dir;
    public int turn;

    public PathNode(Cell c, Vector2Int d, int t)
    {
        cell = c; dir = d; turn = t;
    }
}
class PathNodeComparer : IEqualityComparer<PathNode>
{
    public bool Equals(PathNode a, PathNode b)
    {
        return a.cell == b.cell && a.dir == b.dir && a.turn == b.turn;
    }

    public int GetHashCode(PathNode s)
    {
        int h = 17;
        h = h * 31 + (s.cell != null ? s.cell.GetHashCode() : 0);
        h = h * 31 + s.dir.GetHashCode();
        h = h * 31 + s.turn.GetHashCode();
        return h;
    }
}
