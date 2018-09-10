using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchObject : InteractableObject {
    public GameObject target;

    // Use this for initialization
    void Start () {
        gameObject.tag = "INTERACTABLE";

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void Interact()
    { 
        target.SetActive(!target.activeSelf);
    }
}
