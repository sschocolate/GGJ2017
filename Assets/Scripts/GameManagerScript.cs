using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour 
{

    public GameObject[] playerSpawns;
    public GameObject[] enemySpawns;
    public GameObject PlayerPrefab;
    public GameObject EnemyPrefab;
    public MenuManagerScript mms;
    public Text levelText;
    bool[] enemyInPos;

	// Use this for initialization
	void Start () 
    {
        enemyInPos = new bool[playerSpawns.Length];
        int currentLevel = PlayerPrefs.GetInt("level");
        switch(currentLevel)
        {
            case 1:
            case 2:
            case 3:
                spawnCharacters(1);
                break;
            case 4:
            case 5:
            case 6:
                spawnCharacters(2);
                break;
            case 7:
            case 8:
            case 9:
                spawnCharacters(3);
                break;
            default:
                spawnCharacters(4);
                break;
        }
        levelText.text = "Level: " + PlayerPrefs.GetInt("level");
	}
	
	// Update is called once per frame
	void Update () 
    {
        checkScene();
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
            GameObject clone = Instantiate(EnemyPrefab, spawnLocation, rot);
            Enemy temp = clone.GetComponent<Enemy>();
            enemyDifficulty(temp);
            setEnemySpawnOccupied(spawnChoice, true);
            return true;
        } else
        {
            return false;
        }
    }

    /// <summary>
    /// Sets difficulty depending on level, maxes at 3.
    /// </summary>
    /// <param name="e">Enemy object</param>
    void enemyDifficulty(Enemy e)
    {
        if (PlayerPrefs.GetInt("level") <= 3)
        {
            e.enemyDifficulty = PlayerPrefs.GetInt("level");
            e.enemyShotDifficulty = PlayerPrefs.GetInt("level");
        }
        else if (PlayerPrefs.GetInt("level") >= 10) {
            e.enemyDifficulty = 3;
            e.enemyShotDifficulty = 3;
        }
        else if (PlayerPrefs.GetInt("level") % 3 == 1)
        {
            e.enemyDifficulty = 1;
            e.enemyShotDifficulty = 1;
        }
        else if (PlayerPrefs.GetInt("level") % 3 == 2)
        {
            e.enemyDifficulty = 2;
            e.enemyShotDifficulty = 2;
        }
        else if (PlayerPrefs.GetInt("level") % 3 == 0)
        {
            e.enemyDifficulty = 3;
            e.enemyShotDifficulty = 3;
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
        for (int i = 0; i < numEnemies; i++)
        {
            while (!spawnEnemy(enemyChoice))
            {
                enemyChoice = Random.Range(0, enemySpawns.Length);
            }
        }
    }

    void checkScene()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            mms.changeScene("game_over");
        } 
        else if (GameObject.FindGameObjectsWithTag("Enemy").Length <= 0) 
        {
            changeDifficulty();
        }
    }

    void changeDifficulty()
    {
        // update the level difficulty
        PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
        mms.changeScene("main");
    }
}
