using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	public static GameObject[] boxTypes = Resources.LoadAll<GameObject>("Boxes"); //loads all boxes at assets/Resources/Boxes, this makes it easier to add diferent types of boxes
	public static GameObject[,] boxes = new GameObject[GameManager.width, GameManager.height];

	//will spawn a box at the given position with the given prefab, also have a parent for sorting reasons
	public static void SpawnAt(Vector2Int position, GameObject box, Transform parentTransform)
	{
		//checks if there is already a box at the given position, if not one will be instantiated
		if (boxes[position.x,position.y] == null){
		Debug.Log("[Spawnbox] spawning box at: " + position);

		//instantiates the box and fills it in in the boxes array
		boxes[position.x, position.y] = Instantiate(box, new Vector3(position.x + 0.5f, position.y + 0.5f, 0), Quaternion.identity, parentTransform);
		}
	}

	//breaks a box at a given position and a certain direction
	public static void BreakBoxAt(Vector2Int position)
	{
	Debug.Log("[BreakCrate] breaking box at: " + position);

		//actually destroys the box gameobject
		Destroy(boxes[position.x, position.y]);

		//makes sure that boxes[x,y] is empty
		boxes[position.x, position.y] = null;
        GameManager.Score += 10;
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

}
