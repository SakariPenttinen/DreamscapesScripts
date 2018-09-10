using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatherObject : InteractableObject
{
    CandyLogic cl;
    public override void Interact()
    {
        cl.Gather();
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        gameObject.tag = "INTERACTABLE";
        cl = GameObject.Find("_CandyLogic").GetComponent<CandyLogic>();
        cl.AddCupcake();
	}
	
}
