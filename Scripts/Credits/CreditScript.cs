using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditScript : MonoBehaviour {
    AudioSource audioS;

	// Use this for initialization
	void Start () {
        Keyring.doorKeys.Clear();
        Keyring.portalKeys.Clear();
        audioS = GetComponent<AudioSource>();
        audioS.PlayOneShot(audioS.clip);
	}
	
	// Update is called once per frame
	void Update () {
		if (!audioS.isPlaying)
        {
            SceneManager.LoadScene(0);
        }
	}
}
