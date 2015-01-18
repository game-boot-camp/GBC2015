using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	private AudioSource audioSource;
	public AudioClip audioClip;

	void Awake() {
		DontDestroyOnLoad(this);

		audioSource = gameObject.GetComponent<AudioSource> (); 
		audioSource.clip = audioClip;
		audioSource.Play ();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}
}
