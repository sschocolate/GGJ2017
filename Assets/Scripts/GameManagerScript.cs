using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    // Get the current player score
    int getCurrentScore() {
        return PlayerPrefs.GetInt("score");
    }

    // Add to the score a certain amount
    void incrementScore(int increment) {
        int score = getCurrentScore();
        PlayerPrefs.SetInt("score", score + increment);
    }
}
