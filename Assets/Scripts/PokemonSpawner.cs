using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected List<Transform> listPokemons = new List<Transform>();
    public List<Transform> ListPokemons => listPokemons;

    private void Reset()
    {
        if (listPokemons.Count == 0) LoadPrefabs();
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
    public virtual Transform SpawnPokemonRandom(Vector3 pos, Quaternion ros,Transform parent)
    {
        if (listPokemons == null) return null;
        int index = Random.Range(0, listPokemons.Count);
        var randomPokemonInList = listPokemons[index];

        Transform finalPokemon = Instantiate(randomPokemonInList, pos, ros, parent);
        finalPokemon.gameObject.SetActive(true);
        return finalPokemon;
    }
}