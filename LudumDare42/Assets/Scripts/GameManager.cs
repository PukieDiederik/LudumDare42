using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	public static int width = 10;				//width of the grid
	public static int height =10;				//height of the grid
    public GameObject tile;     //Tile GameObject
    public static int[,] grid;	//The grid where all the info about the boxes and players is stored
    public static GameObject[,] tiles;  //The tile grid
	
    //delay stuff for spawning the boxes
    public static float boxSpawnDelay = 5f;
    float currentSpawnDelay = 5f;

    public Transform boxesParent;
    

	// Use this for initialization
	void Start () {
		/*grid = new int[width, height];
        tiles = new GameObject[width, height];

         
        //fill tiles with tile objects and set the grid value to 0
        for(int x = 0; x < width; x++) {
            for(int y =0; y < height; y++) {
                tiles[x, y] = Instantiate(tile, new Vector2(x, y), Quaternion.identity, this.transform);
                grid[x, y] = 0;
            }
        }*/
	}
	
	// Update is called once per frame
	void Update () 
    {
		currentSpawnDelay -= Time.deltaTime;
        if (currentSpawnDelay < 0) {
            GenerateBoxes(3);
            currentSpawnDelay = boxSpawnDelay;
        }
    }

    //generates the boxes
    void GenerateBoxes(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Box.SpawnAt(new Vector2Int(Random.Range(0,width), Random.Range(0,height)), Box.boxTypes[Random.Range(0,Box.boxTypes.Length - 1)], boxesParent);
        }
    }
}
