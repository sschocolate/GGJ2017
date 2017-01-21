using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chord : MonoBehaviour 
{
	/// <summary>
    /// Model of cord to use. Single, double or triple.
    /// </summary>
	public GameObject[] models;
	/// <summary>
    /// Notes of the cord. Can be 1-3
    /// </summary>
	private Notes[] notes = new Notes[3];
	/// <summary>
    /// Speed at which the cord travels
    /// </summary>
	private float speed = 5f;
	/// <summary>
    /// Direction to shoot the cord. Right or left depending on shooter.
    /// </summary>
	private Direction direction = Direction.None;

	/// <summary>
    /// Called on initialization of object
    /// </summary>
	void Awake()
	{
		setupEmptyNoteArray();
	}

	// Use this for initialization
	void Start () 
	{

	}

	// Update is called once per frame
	void Update () 
	{
		if (direction == Direction.Left){
			transform.position += new Vector3 (-speed * Time.deltaTime, 0, 0);
		}
		if (direction == Direction.Right){
			transform.position += new Vector3 (speed * Time.deltaTime, 0, 0);
		}
	}

	//Use this function when you spawn to set notes
	/// <summary>
    /// Use function when spawning to set the notes of the cord.
    /// </summary>
    /// <param name="_n">Array of notes</param>
    /// <param name="dir">Direction of the cord</param>
	public void Initialize (Notes[] _n, Direction dir)
	{
		notes = _n;
		direction = dir;
		chooseNoteModel(_n);
		transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);
		transform.Rotate(new Vector3 (0, 0, 35));
	}

	private void chooseNoteModel(Notes[] _n)
	{
		for (int i = 0; i < _n.Length; i++) 
		{
			if (_n[i] != Notes.Empty) 
			{
				GameObject temp = Instantiate(models[i], models[i].transform.position, models[i].transform.localRotation);
				this.transform.rotation = temp.transform.rotation;
				GetComponent<MeshFilter>().mesh = temp.GetComponent<MeshFilter>().mesh;
				Destroy(temp);
			}
		}
	}

	/// <summary>
    /// Create a cord with random notes
    /// </summary>
    /// <param name="size"></param>
	public void randomChord(int size)
	{
		for (int i = 0; i < 3; i++){
			if (i >= size) {
				notes [i] = Notes.Empty;
			} else {
				notes [i] = (Notes)Random.Range (0, 4);
			}
		}
		chooseNoteModel(notes);
		direction = Direction.Left;
		Debug.Log ("randomChord the Bullet is: " + notes[0] + " " + notes[1] + " " + notes[2]);
	}
		
	/// <summary>
    /// Increase speed of the cord
    /// </summary>
	void increaseSpeed()
	{
		speed = speed * 2;
	}
	/// <summary>
    /// Decrease speed of the cord
    /// </summary>
	void decreaseSpeed()
	{
		speed = speed / 2;
	}
	/// <summary>
    /// Set a direction of the cord
    /// </summary>
    /// <param name="dir"></param>
	public void setDirection(Direction dir)
	{
		direction = dir;
	}
	/// <summary>
    /// Load the model of the cord
    /// </summary>
    /// <param name="size">Given size of the cord (1-3)</param>
	public void loadModel(int size)
	{
		//Instantiate (models [size - 1], Transform.position);
	}

	/// <summary>
    /// Creates an empty note array
    /// </summary>
	private void setupEmptyNoteArray()
	{
		notes[0] = Notes.Empty;
		notes[1] = Notes.Empty;
		notes[2] = Notes.Empty;
	}
}

