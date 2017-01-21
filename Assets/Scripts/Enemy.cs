using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour 
{
	/// <summary>
    /// Enemies cord to hit them with.
    /// </summary>
	private Chord target;
	/// <summary>
    /// Spawn of the cord shot from the enemy.
    /// </summary>
	public GameObject chordSpawn, chordPrefab;

	// Use this for initialization
	void Start () 
	{
		//target = new Chord ();
		shoot (3);
	}

	// Update is called once per frame
	void Update () 
	{

	}

	//Use this to set enemy target note
	/// <summary>
    /// The enemies cord to hit them with.
    /// </summary>
    /// <param name="_n">Array of notes</param>
	public void setTarget (Notes[] _n)
	{
		target.Initialize (_n, Direction.None);
	}

	/// <summary>
    /// Spawn random cords with given size notes.
    /// </summary>
    /// <param name="size">Number to shoot</param>
	void shoot(int size) 
	{
		GameObject bullet = (GameObject)Instantiate(chordPrefab, chordSpawn.transform.position, new Quaternion());
		bullet.GetComponent<Chord>().randomChord (size);
	}
}
