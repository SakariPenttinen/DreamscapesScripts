using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesertLogic : MonoBehaviour {
    int total = 0;
    int current = 0;

	// Use this for initialization
	void Start () {
        
	}

    public void AddCandle()
    {
        ++total;
    }

    public void Gather()
    {
        ++current;
        if (current == total)
        {
            Keyring.portalKeys.Add("3");
            Keyring.portalKeys.Add("backToLobby");
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
