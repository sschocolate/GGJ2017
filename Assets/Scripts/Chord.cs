using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour {
	public Notes[] notes;
	private float speed;
	private Direction direction;

	// Use this for initialization
	void Start () {
		notes = new Notes[3];
		//Initially set as empty
		notes[0] = Notes.Empty;
		notes[1] = Notes.Empty;
		notes[2] = Notes.Empty;
		direction = Direction.None;
		speed = 10f;
	}

	//Use this function when you spawn to set notes
	public void Initialize (Notes first, Notes second, Notes third, Direction dir){
		notes[0] = first;
		notes[1] = second;
		notes[2] = third;
		direction = dir;
	}

	// Update is called once per frame
	void Update () {
		if(direction == Direction.Left){
			Transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
		}
		if(direction == Direction.Right){
			Transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
		}
	}

	void increaseSpeed(){
		speed = speed * 2;
	}

	void decreaseSpeed(){
		speed = speed / 2;
	}

	public void setDirection(Direction dir){
		direction = dir;
	}
}

