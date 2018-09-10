using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public GameObject selectedObject;
    EventSystem eventSystem;

    bool buttonSelected;

	// Use this for initialization
	void Start () {
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false)
        {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
    }

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
