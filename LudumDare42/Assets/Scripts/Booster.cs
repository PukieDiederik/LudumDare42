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
    static void SuperBreakerBooster() {
        Debug.Log("SuperBreakerBooster");
        //TODO:: Implement breaking of boxes around player
        
    }
    static void ShieldBooster() {
        Debug.Log("ShieldBooster");
        //TODO:: Give shield for 15 seconds
    }
}
