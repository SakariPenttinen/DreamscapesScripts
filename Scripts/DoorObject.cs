using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObject : InteractableObject
{
    DoorState door;
	// Use this for initialization
	void Start () {
        gameObject.tag = "INTERACTABLE";
        door = gameObject.GetComponentInParent<DoorState>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public override void Interact()
    {
        if (gameObject.name == "pos")
            door.Use(90f);
        else
            door.Use(-90f);
    }
}
