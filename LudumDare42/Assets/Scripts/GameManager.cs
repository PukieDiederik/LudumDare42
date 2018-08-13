using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static int width = 10;       //width of the grid
    public static int height = 10;		//height of the grid
    public GameObject tile;             //Tile GameObject
    public static int[,] grid;	        //The grid where all the info about the boxes and players is stored
    public static GameObject[,] tiles;  //The tile grid
    public Text text;                   //Publix input for textbox score
    public static Text scoreText;       //Textbox where the score will be displayed

    //Boxes
    public static float boxSpawnDelay =3.5f; //how long to wait for the next boxes
    float spawnSpeed = 2f;
    float currentSpawnDelay = 1f;
    public GameObject box;

    //Scores
    public static int score = 0;   //Current score

    //Overlay Menus
    public GameObject DeathMenu;        //The death menu that appears when the player dies
    public GameObject highscoreInput;   //The field in which you can input you new highscore data
    public GameObject Leaderboard;      //leaderboard panel
    public GameObject RestartMenu;      //The menu for when you haven't created a new highscore
    public Text nameInput;              //Name input for highscore submition.
    public InputField nameInputField;   //The field where to input you name   
    //Highscore labels
    //scores
    public Text[] highscores = new Text[5];
    //names
    public Text[] names = new Text[5];

    //audiosources
    public AudioSource playerDeathSound;


    // Use this for initialization
    void Start() {
        scoreText = text;   //Set the static score text to the right ui element
        //Start lineair time score icrementing
        InvokeRepeating("IncrementScore", 1, 1);
        InvokeRepeating("SpeedUp", 1, 5);

    }

    // Update is called once per frame
    void Update() {
        //Spawn boxes every 'boxspawndelay' seconds
        currentSpawnDelay -= Time.deltaTime;    
        if (currentSpawnDelay < 0) {
            GenerateBoxes(3);
            currentSpawnDelay = boxSpawnDelay;
        }
    }

    //generates the boxes
    void GenerateBoxes(int amount) {
        if (!PlayerController.isDead) { 
            for (int i = 0; i < amount; i++) {
                Vector2Int position = new Vector2Int(Random.Range(0, width), Random.Range(0, height));
                Box.SpawnAt(position, Box.boxTypes[Random.Range(0, Box.boxTypes.Length - 1)], .4f);
                //Debug.Log("Spawning");
            }
        }
    }
    //Speeds up the spawn rate of the crates
    void SpeedUp() {
        boxSpawnDelay -= .1f;
        if (spawnSpeed >= boxSpawnDelay) { spawnSpeed -= .051f; }
        Mathf.Clamp(boxSpawnDelay, 1f, 100);
        //Debug.Log(boxSpawnDelay);
    }

    //Increments the score every so many seconds
    void IncrementScore() {
        score += 1;
        scoreText.text = score.ToString();
        //Debug.Log("IncrementScore by 1");
    }

    //What to do when the player dies by blunt force
    public void Die() {
        if (!PlayerController.hasShield) {
            playerDeathSound.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = false;
            PlayerController.isDead = true;
            CancelInvoke("IncrementScore");
            CancelInvoke("SpeedUp");
            DeathMenu.SetActive(true);
            if (score > ScoreBoardManager.scores[4].score) {
                highscoreInput.SetActive(true);
                nameInputField.Select();
            } else {
                RestartMenu.SetActive(true);
            }
        } else {
            //if the player has a shield it will destroy the crate and delete the shield
            Box.BreakBoxAt(PlayerController.pos);
            PlayerController.hasShield = false;
        }
    }
    public void SubmitHighscore() {
        ScoreBoardManager.SaveScore(new ScoreBoardManager.Score(score, nameInput.text));
        highscoreInput.SetActive(false);
        Leaderboard.SetActive(true);

        //set Highscore labels
        for(int i= 0; i < 5; i++) {
            highscores[i].text = (i + 1) +"  " + ScoreBoardManager.scores[i].score.ToString();
            names[i].text = ScoreBoardManager.scores[i].name.ToString();
        }                
    }

    public void Exit() {
        Application.Quit();
    }

    public void Restart() {
        //reset scores and inputfields
        score = 0;
        scoreText.text = 0.ToString();
        nameInput.text = "";
        boxSpawnDelay = 3f;

        //TODO:: reset all values
        //Reset player values
        PlayerController.isDead = false;
        PlayerController.hasShield = false;
        PlayerController.pos = new Vector2Int(0, 0);
        GameObject.FindGameObjectWithTag("Player").transform.position = new Vector2(.5f, .5f);
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().enabled = true;

        //Clear the playing field
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                Destroy(Box.boxes[x, y]);
                Box.boxes[x, y] = null;
            }
        }

        //Disable Boosters
        //TODO:: disable boosters

        //Disable all menus
        Leaderboard.SetActive(false);
        highscoreInput.SetActive(false);
        RestartMenu.SetActive(false);
        DeathMenu.SetActive(false);

        //Restart Coroutines
        InvokeRepeating("IncrementScore", 1, 1);
        InvokeRepeating("SpeedUp", 1, 1);

        Debug.Log("Restarted");
    }


}
