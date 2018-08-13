using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BoosterTypes { Speed, SuperBreak, Shield}
public class Booster : MonoBehaviour {
    
    static BoosterTypes bt = BoosterTypes.Speed;        //Booster type


    public static void OnPickup() {
        bt = (BoosterTypes) Random.Range(0,2);

        switch (bt) {
            case BoosterTypes.Speed:
                SpeedBooster();
               break;
            case BoosterTypes.SuperBreak:
                SuperBreakerBooster();
                break;
            case BoosterTypes.Shield:
                ShieldBooster();
                break;
            default:
                Debug.LogError(bt);
                break;
        }
    }
    static IEnumerator SpeedBooster() {
        Debug.Log("speedbooster");

        PlayerController.speed *= .75f;     //Make te speed slightly faster
        yield return new WaitForSeconds(5); //Wait for 5 seconds
        PlayerController.speed /= .75f;     //reset the player speed
    }
    void SuperBreakerBooster() {
        //Break crates around the player around player
        int startX = PlayerController.pos.x - 1;
        int startY = PlayerController.pos.y - 1;

        for (int x = startX < 0 ? 0 : startX; x < PlayerController.pos.x + 1 && x < GameManager.width; x++) {
            for(int y = startY < 0 ? 0 : startY; y < PlayerController.pos.y + 1 && x < GameManager.height; y++) {
                if(Box.boxes[x, y]) {
                    Box.BreakBoxAt(new Vector2Int(x, y));
                }
            }
        }
        
    }
    IEnumerator ShieldBooster() {
        //Break crates in a circle around the player
        PlayerController.hasShield = true;
        yield return new WaitForSeconds(20);
        PlayerController.hasShield = false;
    }
}
