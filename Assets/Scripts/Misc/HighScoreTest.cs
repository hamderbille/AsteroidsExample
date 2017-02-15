using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using dk.Billekode.Asteroids.HighScores;

public class HighScoreTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        HighScoreController.Instance.PrintHighScoresToLog();

        for (int i = 0; i < 20; i++)
        {
            //HighScoreController.Instance.TryAdd("HS " + i, i);

            HighScore highScore = new HighScore();
            highScore.score = Random.Range(0, 100);
            highScore.name = "HS " + i;
            HighScoreController.Instance.TryAdd(highScore );
            highScore.PrintHighScore();
        }
        HighScoreController.Instance.PrintHighScoresToLog();
	}

    // Update is called once per frame
    void Update()
    {
		
	}
}
