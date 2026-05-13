using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int x, y;
    public bool isBlocked = true;
    public bool isBorder = false;
    public bool isVisited = false;
    [SerializeField] protected Pokemon pokemon;
    public Pokemon Pokemon => pokemon;

    public SpriteRenderer sprite;

    private void Awake()
    {
        this.sprite = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        this.pokemon = transform.GetComponentInChildren<Pokemon>();
    }
    public void Init(int x, int y, bool value)
    {
        this.x = x;
        this.y = y;
        isBlocked = !value;

        if (value)
        {
            isBorder = true;
            if (sprite != null)
            {
                sprite.enabled = false;
            }
        }
    }
    public void RemovePokemon()
    {
        if (pokemon != null)
        {
            Destroy(pokemon.gameObject);
            pokemon = null;

            isBlocked = false;
        }
    }
}
