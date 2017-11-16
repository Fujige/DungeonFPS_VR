using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolShell : MonoBehaviour
{
    public AudioSource Audio;

    // Use this for initialization
    void Start ()
    {
        Audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
	{
	    
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            Audio.Play();
            Destroy(this.gameObject,3.0f);
        }
    }
}
