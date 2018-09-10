using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : InteractableObject
{
    Camera playerCam;
    bool beingCarried = false;
    bool hitObstacle = false;

    // Use this for initialization
    void Start () {
        playerCam = Camera.main;
        gameObject.tag = "INTERACTABLE";
    }
	
	// Update is called once per frame
	void Update () {
        if (hitObstacle) Drop();
    }

    public override void Interact()
    {
        if (!beingCarried)
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.parent = playerCam.transform;
            beingCarried = true;
        }
        else Drop();
    }

    public bool IsCarried()
    {
        return beingCarried;
    }

    public void Drop()
    {
        transform.parent = null;
        GetComponent<Rigidbody>().isKinematic = false;
        beingCarried = false;
        hitObstacle = false;
    }

    void OnTriggerEnter()
    {
        hitObstacle = true;
    }


}
