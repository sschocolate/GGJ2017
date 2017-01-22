using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chord : MonoBehaviour 
{
	/// <summary>
    /// Model of cord to use. Single, double or triple.
    /// </summary>
	public GameObject[] models;
	/// <summary>
    /// Text for the buttons the notes need to be destroyed with.
    /// </summary>
	public Text buttonText;
	/// <summary>
    /// Notes of the cord. Can be 1-3
    /// </summary>
	private Notes[] notes = new Notes[3];
	/// <summary>
    /// Speed at which the cord travels
    /// </summary>
	private float speed = 2f;
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

	/// <summary>
    /// Use function when spawning to set the notes of the cord. Player calls this
    /// </summary>
    /// <param name="_n">Array of notes</param>
    /// <param name="dir">Direction of the cord</param>
	public void Initialize (Notes[] _n, Direction dir)
	{
		notes = _n;
		if (dir != Direction.None)
		{
			direction = dir;
			chooseNoteModel(_n);
			setTextBar(_n);
		}
	}

	/// <summary>
    /// Create a meshless chord with a set of notes.
    /// </summary>
    /// <param name="dir">Direction of array. Should be Direction.None</param>
	/// <param name="s">Size of array</param>
	public void Initialize (Direction dir, int s)
	{
		notes = createRandomNotes(s);
		direction = dir;
	}

	/// <summary>
    /// Getter for notes array.
    /// </summary>
    /// <returns>Notes array 1-3</returns>
	public Notes[] getNotes()
	{
		return notes;
	}

	/// <summary>
    /// Picks the note model to display.
    /// </summary>
    /// <param name="_n">Array of notes, 1-3</param>
	private void chooseNoteModel(Notes[] _n)
	{
		for (int i = 0; i < _n.Length; i++) 
		{
			if (_n[i] != Notes.Empty) 
			{
				GameObject temp = Instantiate(models[i], models[i].transform);
				GetComponent<MeshFilter>().mesh = temp.GetComponent<MeshFilter>().mesh;
				Destroy(temp);
			}
		}
	}

	/// <summary>
    /// Create a cord with random notes. Enemy calls this.
    /// </summary>
    /// <param name="size"></param>
	public void randomChord(int size)
	{
		notes = createRandomNotes(size);
		chooseNoteModel(notes);
		setTextBar(notes);
		direction = Direction.Left;
		inverseNote();
	}

	/// <summary>
    /// Create a random array of notes of a set size.
    /// </summary>
    /// <param name="s">Size of array</param>
    /// <returns>Array of notes</returns>
	private Notes[] createRandomNotes(int s)
	{
		Notes[] temp = new Notes[s];
		for (int i = 0; i < 3; i++){
			if (i >= s) {
				temp [i] = Notes.Empty;
			} else {
				temp [i] = (Notes)Random.Range (0, 4);
			}
		}
		return temp;
	}
		
	/// <summary>
    /// Increase speed of the cord
    /// </summary>
	public void increaseSpeed()
	{
		speed = speed * 2;
	}
	/// <summary>
    /// Decrease speed of the cord
    /// </summary>
	public void decreaseSpeed()
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
    /// Compare the notes in the other chord
    /// </summary>
    /// <param name="other">the other chord to compare</param>
	public bool notesAreEqual(Chord other)
	{
		if (notes.Length > other.notes.Length) return false;
		for (int i = 0; i < notes.Length; i++) 
		{
			if (notes [i] != other.notes [i]) 
			{
				return false;
			}
		}
		return true;
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
	/// <summary>
    /// Sets the text bar with the notes of the object
    /// </summary>
    /// <param name="_n"></param>
	private void setTextBar(Notes[] _n)
	{
		string o = "";
		for (int i = 0; i < _n.Length; i++)
		{
			o += _n[i].ToString() + " ";
		}
		buttonText.text = o;
	}
	/// <summary>
    /// Inverse the note object and text UI to face the direction and user.
    /// </summary>
	private void inverseNote()
	{
		// rotates model
		this.transform.Rotate(new Vector3(0f, 180f, 0f));
		// rotate canvas back
		this.GetComponentInChildren<Canvas>().transform.Rotate(new Vector3(0f,-180f, 0f));
	}
	/// <summary>
    /// Collision handling for the notes.
    /// </summary>
    /// <param name="other">Collider object</param>
	void OnTriggerEnter(Collider col) 
	{
		if (col.gameObject.tag == "Note") 
		{
			if (this.notesAreEqual(col.gameObject.GetComponent<Chord>())) 
			{
				Destroy (gameObject);
			} else if (direction == Direction.Right) {
				Destroy (gameObject);
			}
		} 
		else if (col.gameObject.tag == "Enemy")
		{
			Destroy (gameObject);
		} 
		else if (col.gameObject.tag == "Player")
		{
			Destroy (gameObject);
		}
	}
}

