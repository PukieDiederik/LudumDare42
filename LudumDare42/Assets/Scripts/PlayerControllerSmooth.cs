using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControllerSmooth : MonoBehaviour {

	public int speed = 10; //this will be in forces so no presise stuff is needed

	Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//the movement of the player
		rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Input.GetAxisRaw("Vertical") * speed * Time.deltaTime);
		destroyBoxes();
	}

	void destroyBoxes()
	{
		if (Input.GetAxisRaw("Attack") == 1){
			float mouseHorizontal = Input.mousePosition.x / Screen.width * 2 - 1;
			float mouseVertical = Input.mousePosition.y / Screen.height * 2 - 1;

			if (mouseVertical > 0 && Mathf.Abs(mouseVertical) > Mathf.Abs(mouseHorizontal)) { Box.BreakBoxFromDirection(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), 0); } //up
			if (mouseVertical < 0 && Mathf.Abs(mouseVertical) > Mathf.Abs(mouseHorizontal)) { Box.BreakBoxFromDirection(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), 1); } //down
			if (mouseHorizontal < 0 && Mathf.Abs(mouseHorizontal) > Mathf.Abs(mouseVertical)) { Box.BreakBoxFromDirection(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)), 2); } //left
			if (mouseHorizontal > 0 && Mathf.Abs(mouseHorizontal) > Mathf.Abs(mouseVertical)) { Box.BreakBoxFromDirection(new Vector2Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y)) , 3); } //right
		}
	}
}
