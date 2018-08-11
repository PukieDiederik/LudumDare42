using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoardManager : MonoBehaviour {

    public static List<Score> scores = new List<Score>();

    private static void Awake() {
        //Load scores from save
        scores.Add(new Score(PlayerPrefs.GetInt("hs0"), PlayerPrefs.GetString("hsn0")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs1"), PlayerPrefs.GetString("hsn1")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs2"), PlayerPrefs.GetString("hsn2")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs3"), PlayerPrefs.GetString("hsn3")));
        scores.Add(new Score(PlayerPrefs.GetInt("hs4"), PlayerPrefs.GetString("hsn4")));
    }

    public void SaveScore(Score score) {
        if(score.score > scores[4].score) {
            if(score.score > scores[3].score) {
                if (score.score > scores[2].score) {
                    if (score.score > scores[1].score) {
                        if (score.score > scores[0].score) {
                            scores[0] = score;
                        } else {
                            scores[1] = score;
                        }
                    } else {
                        scores[2] = score;
                    }
                } else {
                    scores[3] = score;
                }
            } else {
                scores[4] = score;
            }
        }
    }
    public class Score {
        public int score = 0;
        public string name = "";

        public Score(int score, string name) {
            this.score = score;
            this.name = name;
        }
    }
}
