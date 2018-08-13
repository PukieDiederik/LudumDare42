using System.Collections;
using System.Collections.Generic;
using UnityEngine;
enum Directions { North, East, South, West}
public class PlayerController : MonoBehaviour {
    SpriteRenderer sr;      //reference to the sprite renderer
    public Sprite[] sprites = new Sprite[4];   //Directional sprites, can be adjusted for extra player sprites
    float speed = .15f;     //MovementSpeed
    float timer = 0;        //Timer for the next movement  
    Directions dir = Directions.North;         //The direction in which the player is facing
    private void Start() {
        sr = this.GetComponent<SpriteRenderer>();
    }

    private void Update() {
        timer += Time.deltaTime;    //Add the time between frames to the timer
        //Move the player if the timer has run out
        if (Input.GetKey(KeyCode.W) && timer >= speed) {
            if (this.transform.position.y < GameManager.height - 1 && !Box.DoesCollideWithBox(this.transform.position + Vector3.up)) {
                this.transform.position += Vector3.up;
                sr.sprite = sprites[0];
                dir = Directions.North;
                timer = 0;
            }                
        } else if (Input.GetKey(KeyCode.S) && timer >= speed) {
            if (this.transform.position.y > .5 && !Box.DoesCollideWithBox(this.transform.position + Vector3.down)) {
                this.transform.position += Vector3.down;
                sr.sprite = sprites[2];
                dir = Directions.South;
                timer = 0;
            }
        } else if (Input.GetKey(KeyCode.A) && timer >= speed) {
            if (this.transform.position.x > .5 && !Box.DoesCollideWithBox(this.transform.position + Vector3.left)) {
                this.transform.position += Vector3.left;
                sr.sprite = sprites[3];
                dir = Directions.West;
                timer = 0;
            }                
        } else if (Input.GetKey(KeyCode.D) && timer >= speed) {
            if (this.transform.position.x < GameManager.width - 1 && !Box.DoesCollideWithBox(this.transform.position + Vector3.right)) {
                this.transform.position += Vector3.right;
                sr.sprite = sprites[1];
                dir = Directions.East;
                timer = 0;
            }                
        }        
    }
}
