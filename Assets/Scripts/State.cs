using System.Collections.Generic;
using UnityEngine;

struct State
{
    public Cell cell;
    public Vector2Int dir;
    public int turn;

    public State(Cell c, Vector2Int d, int t)
    {
        cell = c; dir = d; turn = t;
    }
}
class StateComparer : IEqualityComparer<State>
{
    public bool Equals(State a, State b)
    {
        return a.cell == b.cell && a.dir == b.dir && a.turn == b.turn;
    }

    public int GetHashCode(State s)
    {
        int h = 17;
        h = h * 31 + (s.cell != null ? s.cell.GetHashCode() : 0);
        h = h * 31 + s.dir.GetHashCode();
        h = h * 31 + s.turn.GetHashCode();
        return h;
    }
}
