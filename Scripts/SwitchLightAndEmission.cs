using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLightAndEmission : InteractableObject {
    public string emissionMaterialName = "";
    public bool state = true;
    Material eMat;
    GameObject bulb;

    public override void Interact()
    {
        state = !state;
        bulb.SetActive(state);
        EmissionState(state);
    }

    // Use this for initialization
    void Start () {
        gameObject.tag = "INTERACTABLE";
        Renderer[] rs = gameObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rs)
        {
            if (r.material.name == emissionMaterialName + " (Instance)")
            {
                eMat = r.material;
            }
        }
        bulb = gameObject.transform.Find("Bulb").gameObject;
        bulb.SetActive(state);
        EmissionState(state);
	}

    void EmissionState(bool st)
    {
        if (st) eMat.EnableKeyword("_EMISSION");
        else eMat.DisableKeyword("_EMISSION");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
