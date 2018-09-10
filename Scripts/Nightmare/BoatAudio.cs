using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAudio : MonoBehaviour {
    public float noiseFreq = 0.1f;
    AudioSource audioS;
    Rigidbody rb;

	// Use this for initialization
	void Start () {
        audioS = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        if (rb.velocity.magnitude >= 0.1f)
        {
            if (!audioS.isPlaying) audioS.PlayDelayed(noiseFreq);
        }
    }
}
