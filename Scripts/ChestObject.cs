using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestObject : InteractableObject
{
    ChestState chest;
	// Use this for initialization
	void Start () {
        gameObject.tag = "INTERACTABLE";
        chest = gameObject.GetComponentInParent<ChestState>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void Interact()
    {
            chest.Use(90f);
    }
}
