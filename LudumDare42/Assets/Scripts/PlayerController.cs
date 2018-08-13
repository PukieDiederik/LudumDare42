using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Directions { North, East, South, West}
public class PlayerController : MonoBehaviour {
    SpriteRenderer sr;                                      //reference to the sprite renderer
    public Sprite[] sprites = new Sprite[4];                //Directional sprites, can be adjusted for extra player sprites
    float speed = .15f;                                     //MovementSpeed
    float timer = 0;                                        //Timer for the next movement  
    Directions dir = Directions.North;                      //The direction in which the player is facing
    public static Vector2Int pos = new Vector2Int(0, 0);    //Position of the player starting at (0,0)
    public static bool hasShield = false;
    public static bool isDead = false;


    private void Start() {
        sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update() {

        timer += Time.deltaTime;    //Add the time between frames to the timer
        //Move the player if the timer has run out
        if (!isDead){
            if (Input.GetKey(KeyCode.W) && timer >= speed) {
                dir = Directions.North;
                sr.sprite = sprites[0];
                if (this.transform.position.y < GameManager.height - 1 && !Box.DoesCollideWithBox(this.transform.position + Vector3.up)) {
                    this.transform.position += Vector3.up;
                    timer = 0;
                }                
            } else if (Input.GetKey(KeyCode.S) && timer >= speed) {
                dir = Directions.South;
                sr.sprite = sprites[2];  
                if (this.transform.position.y > .5 && !Box.DoesCollideWithBox(this.transform.position + Vector3.down)) {
                    this.transform.position += Vector3.down;
                    timer = 0;
                }
            } else if (Input.GetKey(KeyCode.A) && timer >= speed) {
                dir = Directions.West;
                sr.sprite = sprites[3];
                if (this.transform.position.x > .5 && !Box.DoesCollideWithBox(this.transform.position + Vector3.left)) {
                    this.transform.position += Vector3.left;
                    timer = 0;
                }                
            } else if (Input.GetKey(KeyCode.D) && timer >= speed) {
                dir = Directions.East;
                sr.sprite = sprites[1];
                if (this.transform.position.x < GameManager.width - 1 && !Box.DoesCollideWithBox(this.transform.position + Vector3.right)) {
                    this.transform.position += Vector3.right;
                    timer = 0;
                }                
            }     

            if (Input.GetKey(KeyCode.Space)) 
            {
                if (dir == Directions.North)
                {
                    Box.BreakBoxAt(new Vector2Int((int)(this.transform.position.x - 0.5f), (int)( this.transform.position.y + 0.5f)));
                }
                else if (dir == Directions.South)
                {
                    Box.BreakBoxAt(new Vector2Int((int)(this.transform.position.x - 0.5f), (int)( this.transform.position.y - 1.5f)));
                }
                else if (dir == Directions.East)
                {
                    Box.BreakBoxAt(new Vector2Int((int)(this.transform.position.x + 0.5f), (int)( this.transform.position.y - 0.5f)));
                }
                else if (dir == Directions.West)
                {
                    Box.BreakBoxAt(new Vector2Int((int)(this.transform.position.x - 1.5f), (int)( this.transform.position.y - 0.5f)));
                }
            } 
        }
    }
}
