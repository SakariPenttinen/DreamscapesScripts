using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyObject : InteractableObject
{
    public bool isTeleportKey = false;
    public string key = "";

    public override void Interact()
    {
        if (isTeleportKey) Keyring.portalKeys.Add(key);
        else Keyring.doorKeys.Add(key);
        GameObject.Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        if (key == "") Debug.Log("Please add key string to object " + gameObject.name);
        else gameObject.tag = "INTERACTABLE";
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
