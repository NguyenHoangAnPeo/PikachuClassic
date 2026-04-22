using System.Collections.Generic;
using UnityEngine;

public class PokemonSpawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] protected List<Transform> listPokemons = new List<Transform>();
    public List<Transform> ListPokemons => listPokemons;

    [ContextMenu("Load Child Prefabs")]

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
}