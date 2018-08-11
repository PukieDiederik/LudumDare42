using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static int width = 10;               //width of the grid
    public static int height = 10;				//height of the grid
    public GameObject tile;     //Tile GameObject
    public static int[,] grid;	//The grid where all the info about the boxes and players is stored
    public static GameObject[,] tiles;  //The tile grid
    public Text text;                   //Publix input for textbox score
    public static Text scoreText;      //Textbox where the score will be displayed

    //delay stuff for spawning the boxes
    public static float boxSpawnDelay = 5f;
    float currentSpawnDelay = 5f;

    //Scores
    private static int score = 0;    //Current score
    public static int Score {      //Score editor
        get {
            return score;
        }
        set {
            score += value;                     //Set the score
            scoreText.text = score.ToString();  //Update Label
            //Debug.Log(score);
        }
    }

    // Use this for initialization
    void Start() {
        scoreText = text;
        /*grid = new int[width, height];
        tiles = new GameObject[width, height];

         
        //fill tiles with tile objects and set the grid value to 0
        for(int x = 0; x < width; x++) {
            for(int y =0; y < height; y++) {
                tiles[x, y] = Instantiate(tile, new Vector2(x, y), Quaternion.identity, this.transform);
                grid[x, y] = 0;
            }
        }*/
        //Start lineair time score icrementing
        InvokeRepeating("IncrementScore", 1, 1);

    }

    // Update is called once per frame
    void Update() {
        currentSpawnDelay -= Time.deltaTime;
        if (currentSpawnDelay < 0) {
            GenerateBoxes(3);
            currentSpawnDelay = boxSpawnDelay;
        }
    }

    //generates the boxes
    void GenerateBoxes(int amount) {
        for (int i = 0; i < amount; i++) {
            Box.SpawnAt(new Vector2Int(Random.Range(0, width), Random.Range(0, height)), Box.boxTypes[Random.Range(0, Box.boxTypes.Length - 1)], this.transform);
        }
    }

    //Increments the score every so many seconds
    void IncrementScore() {
        score += 1;
        scoreText.text = score.ToString();
        //Debug.Log("IncrementScore by 1");
    }
}
