using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private Chord target;
	public GameObject chordSpawn, chordPrefab;

	// Use this for initialization
	void Start () {
		target = new Chord ();
		shoot (3);
	}

	//Use this to set enemy target note
	public void setTarget (Notes first, Notes second, Notes third)
	{
		target.Initialize (first, second, third, Direction.None);
	}

	// Update is called once per frame
	void Update () {
	}

	//Spawn random chord
	void shoot(int size) {

		//Create random chord
		GameObject bullet = (GameObject)Instantiate(chordPrefab, chordSpawn.transform.position, new Quaternion());
		bullet.GetComponent<Chord>().randomChord (size);
		//Spawn the chord
		//Chord x = (Chord)Instantiate (bullet, chordSpawn.transform);
		//x.loadModel (size);
	}
}
