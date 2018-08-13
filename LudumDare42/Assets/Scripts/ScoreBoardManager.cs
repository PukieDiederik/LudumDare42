using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour {

    public static List<Score> scores = new List<Score>();

    private void Awake() {
        //Load scores from save
        scores.Add(new Score(PlayerPrefs.GetInt("hs0"), PlayerPrefs.GetString("hsn0")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs1"), PlayerPrefs.GetString("hsn1")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs2"), PlayerPrefs.GetString("hsn2")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs3"), PlayerPrefs.GetString("hsn3")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs4"), PlayerPrefs.GetString("hsn4")));
    }

    public static void SaveScore(Score score) {
        if(score.score > scores[4].score) {
            if(score.score > scores[3].score) {
                if (score.score > scores[2].score) {
                    if (score.score > scores[1].score) {
                        if (score.score > scores[0].score) {
                            scores.Insert(0, score);
                            Debug.Log("Inserted score " + score.ToString() + " on spot: 1");
                        } else {
                            scores.Insert(1, score);
                            Debug.Log("Inserted score " + score.ToString() + " on spot: 2");
                        }
                    } else {
                        scores.Insert(2, score);
                        Debug.Log("Inserted score " + score.ToString() + " on spot: 3");
                    }
                } else {
                    scores.Insert(3, score);
                    Debug.Log("Inserted score " + score.ToString() + " on spot: 4");
                }
            } else {
                scores.Insert(4, score);
                Debug.Log("Inserted score " + score.ToString() + " on spot: 5");
            }
        }
        for(int i = 0; i < 5; i++) {
            PlayerPrefs.SetInt("hs" + i, scores[i].score);
            PlayerPrefs.SetString("hsn" + i, scores[i].name);
        }
    }
    public class Score {
        public int score = 0;
        public string name = "";

        public Score(int score, string name) {
            this.score = score;
            this.name = name.ToUpper();
        }
        public override string ToString() {
            return "'score: " + score + "; name: " + name + "'";
        }
    }
}
