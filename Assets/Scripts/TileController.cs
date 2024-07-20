using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    
    [SerializeField] private GameObject[] tiles = new GameObject[25];
    [SerializeField] public Material lightedMaterial;
    [SerializeField] public Material unlightedMaterial;
    [SerializeField] private int lightedTileChangeInterval = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChoseRandomTile());
    }
    
    IEnumerator ChoseRandomTile()
    {
        int randomTile1 = Random.Range(0, 25);
        int randomTile2 = Random.Range(0, 25);
        tiles[randomTile1].GetComponent<Tile>().isSunlighted = true;
        tiles[randomTile2].GetComponent<Tile>().isSunlighted = true;
        
        yield return new WaitForSeconds(lightedTileChangeInterval);
        
        tiles[randomTile1].GetComponent<Tile>().isSunlighted = false;
        tiles[randomTile2].GetComponent<Tile>().isSunlighted = false;

        StartCoroutine(ChoseRandomTile());
    }
}
