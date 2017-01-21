using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour 
{

    public GameObject[] playerSpawns;
    public GameObject[] enemySpawns;

    bool[] enemyInPos;

    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;

	// Use this for initialization
	void Start () 
    {
        enemyInPos = new bool[playerSpawns.Length];
        spawnCharacters(2);
	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    // Get the current player score
    int getCurrentScore() 
    {
        return PlayerPrefs.GetInt("score");
    }

    // Add to the score a certain amount
    void incrementScore(int increment) 
    {
        int score = getCurrentScore();
        PlayerPrefs.SetInt("score", score + increment);
    }

    // Spawn player at the default position
    void spawnPlayer() 
    {
        Vector3 initSpawn = playerSpawns[0].transform.position;
		Quaternion rot = playerSpawns [0].transform.rotation;
        Instantiate(PlayerPrefab, initSpawn, rot);
    }

    // Spawns enemy at specified location
    // Returns true if successful, false if the enemy is not spawned
    bool spawnEnemy(int spawnChoice)
    {
        if (!enemyInPos[spawnChoice])
        {
            Vector3 spawnLocation = enemySpawns[spawnChoice].transform.position;
			Quaternion rot = enemySpawns [spawnChoice].transform.rotation;
			Instantiate(EnemyPrefab, spawnLocation, rot);
            setEnemySpawnOccupied(spawnChoice, true);
            return true;
        } else
        {
            return false;
        }
    }

    // Sets array of enemy locations to to occupied or not occupied
    void setEnemySpawnOccupied(int spawnChoice, bool isOccupied)
    {
        enemyInPos[spawnChoice] = isOccupied;
    }

    void spawnCharacters(int numEnemies)
    {
        int enemyChoice = Random.Range(0, enemySpawns.Length);
        spawnPlayer();

        for (int i = 0; i < numEnemies; i++)
        {
            while (!spawnEnemy(enemyChoice))
            {
                enemyChoice = Random.Range(0, enemySpawns.Length);
            }
        }
    }
}
