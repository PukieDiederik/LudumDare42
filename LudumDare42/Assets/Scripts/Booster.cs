using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum BoosterTypes { Speed, SuperBreak, Shield}
public class Booster : MonoBehaviour {
    
    BoosterTypes bt = BoosterTypes.Speed;        //Booster type


    private void OnCollisionEnter2D(Collision2D collision) {
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
        }
    }
    IEnumerator SpeedBooster() {
        PlayerController.speed *= .75f;     //Make te speed slightly faster
        yield return new WaitForSeconds(5); //Wait for 5 seconds
        PlayerController.speed /= .75f;     //reset the player speed
    }
    void SuperBreakerBooster() {
        //TODO:: Implement breaking of boxes around player
        
    }
    void ShieldBooster() {
        //TODO:: Give shield for 15 seconds
    }
}
