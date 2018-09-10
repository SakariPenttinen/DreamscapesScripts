using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSwap : InteractableObject {
    BoatLogic bl;

    public override void Interact()
    {
        bl.Swap();
    }

    // Use this for initialization
    void Start () {
        bl = GameObject.Find("_boatLogic").GetComponent<BoatLogic>();
        tag = "INTERACTABLE";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
