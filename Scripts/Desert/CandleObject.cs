using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandleObject : InteractableObject
{
    DesertLogic dl;
    public override void Interact()
    {
        dl.Gather();
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        gameObject.tag = "INTERACTABLE";
        dl = GameObject.Find("_gameLogic").GetComponent<DesertLogic>();
        dl.AddCandle();
	}
	
}
