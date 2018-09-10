using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportObject : InteractableObject
{
    public int level = 0;
    public bool locked = false;
    public String key = "NO_KEY";
     
    public override void Interact()
    {
        if (locked && Keyring.portalKeys.Contains(key)) locked = false;
        if (!locked)
        {
            Keyring.doorKeys.Clear();
            SceneManager.LoadScene(level);
        }
    }

    // Use this for initialization
    void Start () {
        gameObject.tag = "INTERACTABLE";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
