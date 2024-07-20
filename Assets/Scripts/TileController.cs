using System.Collections;
using UnityEngine;

public class TileController : MonoBehaviour
{
    [SerializeField] private GameObject[] tiles = new GameObject[25];
    [SerializeField] public Material lightedMaterial;
    [SerializeField] public Material unlightedMaterial;
    [SerializeField] private int lightedTileChangeInterval = 3;
    [SerializeField] private PlayerController playerController;

    public bool isGameOver = false;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChoseRandomTile());
    }
    
    IEnumerator ChoseRandomTile()
    {
        int randomTile1 = Random.Range(0, 25);
        int randomTile2 = Random.Range(0, 25);

        if (!playerController.isGameStarted)
        {
            tiles[randomTile1].GetComponent<Tile>().isSunlighted = false;
            tiles[randomTile2].GetComponent<Tile>().isSunlighted = false;
        }
        else
        {
            tiles[randomTile1].GetComponent<Tile>().isSunlighted = true;
            tiles[randomTile2].GetComponent<Tile>().isSunlighted = true;
        }
        
        yield return new WaitForSeconds(lightedTileChangeInterval);
        
        
        tiles[randomTile1].GetComponent<Tile>().isSunlighted = false;
        tiles[randomTile2].GetComponent<Tile>().isSunlighted = false;

        if(!isGameOver)
            StartCoroutine(ChoseRandomTile());
    }

    public void MakeAllTilesUnlighted()
    {
        foreach (var tile in tiles)
        {
            tile.GetComponent<Tile>().isSunlighted = false;
        }
    }
}
