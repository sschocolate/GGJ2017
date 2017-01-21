using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
	private Chord target;
	public float bulletSpeed;
	public GameObject bulletSpawn;

	// Use this for initialization
	void Start () {
		target = new Chord ();
	}

	void setTarget (Notes first, Notes second, Notes third)
	{
		target.Initialize (first, second, third);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void shoot(int size) {
		Chord bullet = new Chord ();
		for (int i = 0; i < 3; i++){
			if(i > size)
				bullet.notes[i] = Notes.empty;
			else
				bullet.notes[i] = (Notes)Random.Range(0, 4);
		}

	}
}
