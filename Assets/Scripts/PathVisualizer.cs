using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PathVisualizer : AnMonoBehaviour
{
    [SerializeField] protected LineRenderer lineRenderer;
    public LineRenderer LineRenderer => lineRenderer;

    protected Vector3 zOffset = new Vector3(0, 0, -0.1f);
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadLineRender();
    }
    protected virtual void LoadLineRender()
    {
        if (this.lineRenderer != null) return;
        this.lineRenderer = transform.GetComponent<LineRenderer>();
    }
    public virtual void ShowPath(List<Cell> path)
    {
        if (path == null) return;

        lineRenderer.positionCount = path.Count;
        for(int i = 0;i < path.Count; i++)
        {
            lineRenderer.SetPosition(i, path[i].transform.position + zOffset);
        }
    }
    public void ClearPath()
    {
        lineRenderer.positionCount = 0;
    }
}
