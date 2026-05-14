using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected List<Pokemon> listPokemons = new List<Pokemon>();
    public List<Pokemon> ListPokemons => listPokemons;

    [SerializeField] protected Dictionary<int, Pokemon> pokemonDict = new Dictionary<int, Pokemon>();
    public Dictionary<int, Pokemon> PokemonDict => pokemonDict;

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
            Pokemon p = prefab.GetComponent<Pokemon>();

            if(p != null)
            {
                listPokemons.Add(p);
            }
            else
            {
                Debug.LogWarning("Prefab thieu pokemon script: " + prefab.name);
            }
        }
        this.HidePrefabs();
    }
    protected virtual void HidePrefabs()
    {
        foreach (Pokemon p in listPokemons)
        {
            p.gameObject.SetActive(false);
        }
    }
    public virtual Pokemon SpawnPokemonById(int id, Vector3 pos, Quaternion ros,Cell cell)
    {
        if (!CanSpawnPokemon(id)) return null;

        Pokemon finalPokemon = Instantiate(pokemonDict[id], pos, ros, cell.transform);

        finalPokemon.gameObject.SetActive(true);

        cell.SetPokemon(finalPokemon);

        return finalPokemon;
    }
    protected virtual void LoadPokemonDict()
    {
        foreach (Pokemon p in listPokemons)
        {
            int id = p.IdPokemon;

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