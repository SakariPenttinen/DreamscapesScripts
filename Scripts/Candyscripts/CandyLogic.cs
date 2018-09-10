using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyLogic : MonoBehaviour {
    int total = 0;
    int current = 0;
    GameObject teleport;

	// Use this for initialization
	void Start () {
        teleport = GameObject.Find("patu");
        teleport.SetActive(false);
	}

    public void AddCupcake()
    {
        ++total;
    }

    public void Gather()
    {
        ++current;
        if (current == total)
        {
            Keyring.portalKeys.Add("2");
            teleport.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
