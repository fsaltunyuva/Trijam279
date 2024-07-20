using System;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isSunlighted = false;
    private int id = 0;
    [SerializeField] private TileController tileController;

    private void Start()
    {
        id = Int32.Parse(gameObject.name);
    }

    private void Update()
    {
        if (isSunlighted)
        {
            this.gameObject.GetComponent<MeshRenderer>().material = tileController.lightedMaterial;
        }
        else
        {
            this.gameObject.GetComponent<MeshRenderer>().material = tileController.unlightedMaterial;
        }
    }
}
