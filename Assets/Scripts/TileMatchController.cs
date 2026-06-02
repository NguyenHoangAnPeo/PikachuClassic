using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMatchController : AnMonoBehaviour
{
    [SerializeField] protected GridManager gridManager;
    public GridManager GridManager => gridManager;
    [SerializeField] protected Cell firstCell = null;
    public Cell FirstCell => firstCell;
    [SerializeField] protected Cell secondCell = null;
    public Cell SecondCell => secondCell;

    [SerializeField] protected int? idFirstPokemon = null;

    [SerializeField] protected int? idSecondPokemon;
    [SerializeField] protected int remainingPokemon;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadGridManager();
    }
    protected override void Start()
    {
        this.LoadRemainingPokemon(); //Can hoi xem dat ham o day hop li k, cos cach khac hon k
    }
    protected virtual void LoadGridManager()
    {
        if (this.gridManager != null) return;
        this.gridManager = transform.GetComponentInParent<GridManager>();
    }
    protected virtual void LoadRemainingPokemon()
    {
        int t = gridManager.GridSpawner.PlayableCellCount;

        if (t == 0) return;
        this.SetRemainingPokemon(t);
    }
    public void SelectedCell(Cell cell)
    {
        if (cell == firstCell) return;
        if (firstCell == null)
        {
            firstCell = cell;

            this.idFirstPokemon = cell.Pokemon.IdPokemon;

            return;
        }
        secondCell = cell;

        this.idSecondPokemon = cell.Pokemon.IdPokemon;

        List<Cell> path = GridManager.Instance.FindShortestPath(firstCell, secondCell);

        if (IsMatch(firstCell, secondCell, out path))
        {
            Debug.Log("Match!");
            firstCell.RemovePokemon();
            secondCell.RemovePokemon();

            StartCoroutine(RemoveCell(path));
            this.SubtractionPokemon();
        }
        else
        {
            Debug.Log("UnMatch!");
            ResetSelection();
            return;
        }
    }
    protected virtual bool IsMatch(Cell firstCell, Cell secondCell, out List<Cell> path)
    {
        path = null;

        if (GridManager.Instance == null) return false;

        if (IsSameId())
        {

            path = GridManager.Instance.FindShortestPath(firstCell, secondCell);

            if (path == null) return false;

            GridManager.Instance.PathVisualizer.ShowPath(path);

            return true;
        }
        else return false;
    }
    IEnumerator RemoveCell(List<Cell> path)
    {
        yield return new WaitForSeconds(0.5f);

        GridManager.Instance.PathVisualizer.ClearPath();

        this.ResetSelection();
    }
    protected void ResetSelection()
    {
        firstCell = null;
        secondCell = null;
        idFirstPokemon = null;
        idSecondPokemon = null;
    }
    protected virtual bool IsSameId()
    {
        if (this.idFirstPokemon == null || this.idSecondPokemon == null) return false;
        if (this.idFirstPokemon == this.idSecondPokemon) return true;
        else return false;
    }
    protected virtual void SubtractionPokemon()
    {
        remainingPokemon = remainingPokemon - 2;
        if(this.remainingPokemon == 0)
        {
            GameStateManager.Instance.ChangeState(GameState.Win);
        }
    }
    protected void SetRemainingPokemon(int t)
    {
        this.remainingPokemon = t;
    }
}
