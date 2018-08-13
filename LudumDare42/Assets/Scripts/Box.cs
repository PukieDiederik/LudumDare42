using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public static GameObject[] boxTypes;//loads all boxes at assets/Resources/Boxes, this makes it easier to add diferent types of boxes
	public static GameObject[,] boxes = new GameObject[GameManager.width, GameManager.height];

	static List<GameObject> toInstantiate = new List<GameObject>();
	public static List<GameObject> shadows = new List<GameObject>();
	public GameObject crateShadowInspector;
	public static GameObject crateShadow;

	public static float spawnDelay;

	public static float breakDelay = 1f;
	public static float breakTimer = 1f;
	
    public GameManager gm;  //THe gamemanager needed for the death method

	void Start()
	{
		crateShadow = crateShadowInspector;
		boxTypes = Resources.LoadAll<GameObject>("Boxes"); 
	}

	public void Update()
	{
		breakTimer -= Time.deltaTime;
		spawnDelay -= Time.deltaTime;
		WaitForSpawning();
	}


	//will spawn a box at the given position with the given prefab, also have a parent for sorting reasons
	public static void SpawnAt(Vector2Int position, GameObject box, float delay)
	{
		//checks if there is already a box at the given position, if not one will be instantiated
		if (boxes[position.x,position.y] == null)
		{
			spawnDelay = delay;
			Debug.Log("Spawning box at: " + position);

			toInstantiate.Add(box);
			toInstantiate[toInstantiate.Count - 1].transform.position = new Vector3(position.x,position.y,0); 

			Debug.Log(toInstantiate[toInstantiate.Count - 1].transform.position);

			InstantiateShadow(position);
		}
	}

	void WaitForSpawning()
	{
		if (spawnDelay < 0 && toInstantiate.Count != 0)
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
		if (breakTimer < 0 && boxes[position.x,position.y] != null)
		{		
			//actually destroys the box gameobject
			Destroy(boxes[position.x, position.y]);

			//makes sure that boxes[x,y] is empty
			boxes[position.x, position.y] = null;
			GameManager.Score += 10;
			Debug.Log(GameManager.Score);
			Debug.Log("added +10 score");

			breakTimer = breakDelay;
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