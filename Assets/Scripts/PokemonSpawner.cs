using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected List<Transform> listPokemons = new List<Transform>();
    public List<Transform> ListPokemons => listPokemons;

    [SerializeField] protected Dictionary<int, Transform> pokemonDict = new Dictionary<int, Transform>();
    public Dictionary<int, Transform> PokemonDict => pokemonDict;

    private void Awake()
    {
        if (listPokemons.Count == 0) LoadPrefabs();
        this.LoadPokemonDict();
    }
    private void Reset()
    {
        if (listPokemons.Count == 0) LoadPrefabs();
        this.LoadPokemonDict();
    }
    protected void LoadPrefabs()
    {
        if (this.listPokemons.Count > 0) return;

        Transform prefabObj = transform.Find("Prefabs");
        foreach (Transform prefab in prefabObj)
        {
            listPokemons.Add(prefab);
        }
        this.HidePrefabs();
    }
    protected virtual void HidePrefabs()
    {
        foreach (Transform prefab in listPokemons)
        {
            prefab.gameObject.SetActive(false);
        }
    }
    public virtual Transform SpawnPokemonById(int id, Vector3 pos, Quaternion ros, Transform parent)
    {
        if (!CanSpawnPokemon(id)) return null;

        Transform finalPokemon = Instantiate(pokemonDict[id], pos, ros, parent);
        finalPokemon.gameObject.SetActive(true);
        return finalPokemon;
    }
    protected virtual void LoadPokemonDict()
    {
        foreach (Transform p in listPokemons)
        {
            Pokemon pokemonComponent = p.GetComponent<Pokemon>();

            if (pokemonComponent == null)
            {
                Debug.LogWarning("Prefab.Component Pokemon null: " + p.name);
                continue;
            }

            int id = pokemonComponent.IdPokemon;

            if (!pokemonDict.ContainsKey(id))
            {
                pokemonDict.Add(id, p);
            }

            else
            {
                Debug.LogWarning("Trung ID Pokemon: " + id);
            }
        }
    }
    protected virtual bool CanSpawnPokemon(int id)
    {
        if (pokemonDict == null)
        {
            Debug.LogWarning("pokemonDict chua dc khoi tao!");
            return false;
        }

        else if (!pokemonDict.ContainsKey(id))
        {
            Debug.LogWarning("Khong tim thay Pokemon voi ID: " + id);
            return false;
        }
        return true;
    }
}