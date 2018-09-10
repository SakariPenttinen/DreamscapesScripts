using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRayInteraction : MonoBehaviour {
    Camera playerCam;
    public float rayDistance = 1f;
    GameObject promptObject;
    bool inMenu = false;

    // Use this for initialization
    void Start () {
        playerCam = Camera.main;
        if (playerCam == null) Debug.Log("cam not found");
        promptObject = GameObject.Find("Prompt");
        if (promptObject == null) Debug.Log("prompt not found");
        if (promptObject) promptObject.SetActive(false);
    }

    public void SetMenu(bool b)
    {
        inMenu = b;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit hitObject;
        Ray camRay = playerCam.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(camRay, out hitObject, rayDistance);
        if (!inMenu && hitObject.collider != null && string.Equals("INTERACTABLE", hitObject.collider.gameObject.tag))
        {
            promptObject.SetActive(true);
            if (Input.GetButtonDown("Use") )
            {
                InteractableObject obj = hitObject.collider.gameObject.GetComponent<InteractableObject>();
                obj.Interact();
            }
        }
        else promptObject.SetActive(false);
        

    }
}
