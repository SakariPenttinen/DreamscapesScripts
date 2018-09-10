using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("Poll");
	}
	
    IEnumerator Poll()
    {
        while (!Keyring.doorKeys.Contains("WILSON"))
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        Keyring.portalKeys.Add("4");
    }
	// Update is called once per frame
	void Update () {
		
	}
}
