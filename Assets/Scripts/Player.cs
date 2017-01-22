﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	/// <summary>
    /// Lanes for the player to move in.
    /// </summary>
	public GameObject[] lanes;
	/// <summary>
    /// Chord spawn for the players chords.
    /// </summary>
	public GameObject chordSpawn, chordPrefab;
	/// <summary>
    /// User input for chords to be displayed.
    /// </summary>
	public Text userInputText;
	/// <summary>
    /// Location of the user, ie lane they are in.
    /// </summary>
	private int location = 0;
	/// <summary>
    /// Number of notes allowed.
    /// </summary>
	private int setNote = 0;
	/// <summary>
    /// Input for notes from user.
    /// </summary>
	private Notes[] input = new Notes[3];
	/// <summary>
    /// Delay for the fire rate of the player.
    /// </summary>
    private float fireDelay = 1f;
	/// <summary>
    /// Fired boolean for a current shot.
    /// </summary>
    private bool fired = false;
	/// <summary>
	/// Array of sound clips
	/// </summary>
	public AudioClip[] Clips;
	/// <summary>
	/// Array of audio sources used to play sound.
	/// </summary>
	private AudioSource[] audioSources;

	void Awake()
	{
		clearArray(input);
	}

	// Use this for initialization
	void Start () 
	{
		audioSources = new AudioSource[Clips.Length];
		int i = 0;
		while (i < Clips.Length){
			audioSources[i] = gameObject.AddComponent<AudioSource>();
			audioSources[i].clip = Clips[i];
			i++;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown ("up")) 
		{
			moveUp ();
		} else if(Input.GetKeyDown("down"))
		{
			moveDown ();
		}
		if (setNote <= 2) 
		{
			if (Input.GetKeyDown ("a")) 
			{
				input [setNote] = Notes.A;
				audioSources [0].Play();
				setNote++;
			}
			if (Input.GetKeyDown ("s")) 
			{
				input [setNote] = Notes.S;
				audioSources [1].Play ();
				setNote++;
			}
			if (Input.GetKeyDown ("d")) 
			{
				input [setNote] = Notes.D;
				audioSources [2].Play ();
				setNote++;
			}
			if (Input.GetKeyDown ("f")) 
			{
				input [setNote] = Notes.F;
				audioSources [3].Play ();
				setNote++;
			}
			if (Input.GetKeyDown ("g")) 
			{
				input [setNote] = Notes.G;
				audioSources [4].Play ();
				setNote++;
			}
		}
		if (Input.GetKeyDown ("space")) {
            if (!fired)
            {
				int noteSize = setNote;
                /*while (setNote <= 2)
                {
                    input[setNote++] = Notes.Empty;
                }*/
                if (input[0] != Notes.Empty)
				{
					if (noteSize == 1) {
						audioSources [(int)input [0] + 5].Play ();
					} else if (noteSize == 2) {
						audioSources [Random.Range (10, 14)].Play ();
					} else {
						audioSources [Random.Range (14, 19)].Play ();
					}
                    shoot();
						
                    fired = true;
                }
                setNote = 0;
                input = new Notes[3];
				clearArray(input);
			}
		}
        if (fired)
        {
            fireDelay -= Time.deltaTime;
            if(fireDelay <= 0)
            {
                fired = false;
                fireDelay = 1f;
            }
        }
		setTextBar(input, setNote);
	}
	/// <summary>
    /// Move the player up.
    /// </summary>
	void moveUp(){
        //fired = false;
        if (location > 0)
			transform.position = lanes [--location].transform.position;
	}
	/// <summary>
    /// Moves the player down.
    /// </summary>
	void moveDown(){
        //fired = false;
        if (location < lanes.Length - 1)
			transform.position = lanes [++location].transform.position;
	}
	/// <summary>
    /// Shoots the player cord.
    /// </summary>
	void shoot(){
		GameObject bullet = (GameObject)Instantiate(chordPrefab, chordSpawn.transform.position, new Quaternion());
		bullet.GetComponent<Chord>().Initialize (input, Direction.Right);
		bullet.GetComponent<Chord> ().increaseSpeed ();
	}
	void clearArray(Notes[] n)
	{
		for (int i = 0; i < 3; i++) 
		{
			n[i] = Notes.Empty;
		}
	}
	/// <summary>
    /// Sets the text bar with the notes of the object
    /// </summary>
    /// <param name="_n">Notes of the player</param>
	private void setTextBar(Notes[] _n, int notes)
	{
		string o = "";
		if(notes != 0) 
		{
			for (int i = 0; i < _n.Length; i++)
			{
				o += _n[i].ToString() + " ";
			}
		}
		userInputText.text = o;
	}
	/// <summary>
	/// Collision handling for the Player and notes.
    /// </summary>
    /// <param name="col">Collider</param>
	void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Note")
		{
			Destroy (gameObject);
		}	
	}
}
