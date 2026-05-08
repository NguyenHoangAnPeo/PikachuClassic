using UnityEngine;

public class CellButton : MonoBehaviour
{
    [SerializeField] protected Cell cell;
    public Cell Cell => cell;

    private void Awake()
    {
        this.cell = GetComponent<Cell>();
    }

    private void OnMouseDown()
    {
        //if (cell == null || cell.Pokemon == null) return;

        if (GridManager.Instance.TileMatchController.SecondCell != null) return;

        GridManager.Instance.TileMatchController.SelectedCell(cell);
        Debug.Log("Click: " + cell.x + "," + cell.y);
    }
}