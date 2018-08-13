using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public static GameObject[] boxTypes;//loads all boxes at assets/Resources/Boxes, this makes it easier to add diferent types of boxes
	public static GameObject[,] boxes = new GameObject[GameManager.width, GameManager.height];


	static List<GameObject> toInstantiate = new List<GameObject>();     //To be instantiated objectss
	public static List<GameObject> shadows = new List<GameObject>();    //The shadows in the game
	public GameObject crateShadowInspector;                             //TODO:: give comment
	public static GameObject crateShadow;                               //TODO:: give comment

	public GameObject boosterInspector;

	[Range(0,100)]public static int boosterPercentage = 50;
	public static GameObject booster;
	public static GameObject[,] boosters = new GameObject[GameManager.width,GameManager.height];


    public static float spawnDelay; //TODO:: give comment
    public static float breakDelay = .5f;    //TODO:: give comment
    public static float breakTimer = .5f;    //TODO:: give comment

    public GameManager gm;  //THe gamemanager needed for the death method
	public static GameObject player;

    public AudioSource crateBreakSound;
    static AudioSource cbSound;

    void Start()
	{
		crateShadow = crateShadowInspector;
		boxTypes = Resources.LoadAll<GameObject>("Boxes");
        cbSound = crateBreakSound;
		booster = boosterInspector;
		player = GameObject.Find("Player");
	}

	public void Update()
	{
		breakTimer -= Time.deltaTime;
		spawnDelay -= Time.deltaTime;
		WaitForSpawning();
		CheckForBooster(new Vector2Int((int)player.transform.position.x,(int) player.transform.position.y));
	}


	//will spawn a box at the given position with the given prefab, also have a parent for sorting reasons
	public static void SpawnAt(Vector2Int position, GameObject box, float delay)
	{
		//checks if there is already a box at the given position, if not one will be instantiated
		if (boxes[position.x,position.y] == null)
		{
			spawnDelay = delay;

			toInstantiate.Add(box);
			toInstantiate[toInstantiate.Count - 1].transform.position = new Vector3(position.x,position.y,0); 

			InstantiateShadow(position);
		}
	}

	void WaitForSpawning()
	{
		if (spawnDelay <= 0 && toInstantiate.Count > 0)
		{
			int i = 0;
			while (true){
                //instantiates the box and fills it in in the boxes array
                Vector2Int pos = new Vector2Int((int)shadows[0].transform.position.x, (int)shadows[0].transform.position.y);
                if(PlayerController.pos == pos) {
                    gm.Die();
                }
				boxes[pos.x, pos.y] = Instantiate(toInstantiate[0], shadows[0].transform.position, Quaternion.identity);
				toInstantiate.RemoveAt(0);
				
				Destroy(shadows[0]);
				shadows.RemoveAt(0);

				i++;
				if (toInstantiate.Count == 0) {break;}
			}
		}
	}

	public bool CanBreakBoxAt(Vector2 playerPosition, Vector2 position)
	{
		return false;
	}

	//breaks a box at a given position and a certain direction
	public static void BreakBoxAt(Vector2Int position)
	{
        Debug.Log(breakTimer);
		if (breakTimer < 0 && position.x >= 0 && position.x < GameManager.width && position.y >= 0 && position.y < GameManager.height && boxes[position.x,position.y] != null)
		{
            Debug.Log("BReaking");
            cbSound.Play();
            //actually destroys the box gameobject
            Destroy(boxes[position.x, position.y]);

			//makes sure that boxes[x,y] is empty
			boxes[position.x, position.y] = null;
			GameManager.score += 10;
            GameManager.scoreText.text = GameManager.score.ToString();

			breakTimer = breakDelay;

			float boosterPerc = Random.Range(0,100);

			if (boosterPerc < boosterPercentage)
			{
				boosters[position.x,position.y] = Instantiate(booster, new Vector3(position.x + 0.5f,position.y + 0.5f,0), Quaternion.identity);
			}
		}
	}

	public static void CheckForBooster(Vector2Int position)
	{
		if (boosters[position.x,position.y])
		{
			Debug.Log("Picked up booster");
			Booster.OnPickup();
			Destroy(boosters[position.x,position.y].gameObject);
			boosters[position.x,position.y] = null; 
		}
	} 

	//breaks a box from a given direction
	public static void BreakBoxFromDirection(Vector2Int position, int direction)
	{
		switch(direction)
		{
			case 0:
			  BreakBoxAt(new Vector2Int(position.x, position.y + 1)); //up
			  break;
			
			case 1:
			  BreakBoxAt(new Vector2Int(position.x, position.y - 1)); //down
			  break;

			case 2:
			  BreakBoxAt(new Vector2Int(position.x - 1, position.y)); //left
			  break;

			case 3:
			  BreakBoxAt(new Vector2Int(position.x + 1, position.y)); //right
			  break;

			default:
			  Debug.LogError(direction + " is not a valid direction");
			  break;
		}
	}

	static void InstantiateShadow (Vector2Int position)
	{
		shadows.Add(Instantiate(crateShadow ,new Vector3(position.x + 0.5f,position.y + 0.5f , 0),Quaternion.identity));
	}

	
    public static bool DoesCollideWithBox(Vector2 position)
    {
        if (Box.boxes[(int)position.x,(int)position.y] == null) { return false; }

        return true;
    }
}