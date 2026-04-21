using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchController : MonoBehaviour
{
    [SerializeField] protected Cell firstCell = null;
    public Cell FirstCell => firstCell;
    [SerializeField] protected Cell secondCell = null;
    public Cell SecondCell => secondCell;

    public void SelectedCell(Cell cell)
    {
        if (cell == firstCell) return;
        if (firstCell == null)
        {
            firstCell = cell;
            //Highlight(cell, true);
            return;
        }
        secondCell = cell;
        //Highlight(cell, true);

        List<Cell> path = GridManager.Instance.FindShortestPath(firstCell, secondCell);

        if (IsMatch(firstCell, secondCell, out path))
        {
            Debug.Log("Match!");

            StartCoroutine(RemoveCell(path));
        }
        else
        {
            Debug.Log("UnMatch!");
            return;
        }
    }
    protected virtual bool IsMatch(Cell firstCell, Cell secondCell, out List<Cell> path)
    {
        path = null;

        if (GridManager.Instance == null) return false;

        path = GridManager.Instance.FindShortestPath(firstCell, secondCell);

        if (path == null) return false;

        GridManager.Instance.HighlightPath(path);

        return true;
    }
    IEnumerator RemoveCell(List<Cell> path)
    {
        yield return new WaitForSeconds(0.5f);

        GridManager.Instance.UnHighlightPath(path);

        this.firstCell = null;
        this.secondCell = null;
    }
}
