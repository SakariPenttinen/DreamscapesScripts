using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatReturn : MonoBehaviour {
    BoatLogic bl;

	// Use this for initialization
	void Start () {
        bl = GameObject.Find("_boatLogic").GetComponent<BoatLogic>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider cl)
    {
        if (cl != null && cl.tag == "Player") bl.Swap();
    }
}
