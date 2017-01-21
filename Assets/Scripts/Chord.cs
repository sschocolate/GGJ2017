using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour {
	public Notes[] notes;
	private float speed;

	// Use this for initialization
	void Start () {
		notes = new Notes[3];
		//Initially set as empty
		notes[0] = Notes.empty;
		notes[1] = Notes.empty;
		notes[2] = Notes.empty;
	}

	//Use this function when you spawn to set notes
	public void Initialize (Notes first, Notes second, Notes third){
		notes[0] = first;
		notes[1] = second;
		notes[2] = third;
	}

	// Update is called once per frame
	void Update () {
		
	}
}

