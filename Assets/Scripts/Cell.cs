using Unity.Mathematics;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public bool isBlocked = false;
    public bool isBorder = false;
    public bool isVisited = false;

    public SpriteRenderer sprite;

    private void Awake()
    {
        this.sprite = GetComponent<SpriteRenderer>();
    }
    public void Init(int x, int y, int width, int height)
    {
        this.x = x;
        this.y = y;

        if (x == 0 || y == 0 || x == width - 1 || y == height - 1)
        {
            isBorder = true;
            if (sprite != null)
            {
                sprite.enabled = false;
            }
        }
    }
}
