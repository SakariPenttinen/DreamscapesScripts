using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChestState : MonoBehaviour {
    public float doorPosOpenAngle = 90.0f;
    public float doorCloseAngle = 0.0f;
    public float snapAngle = 1f;
    private Quaternion doorPosOpen = Quaternion.identity;
    private Quaternion doorClose = Quaternion.identity;
    bool open = false;
    public String key = "NO_KEY";
    bool moving = false;
    public bool locked = false;
    public float animSpeed = 2f;
    Transform hinge;

    // Use this for initialization
    void Start () {
        hinge = transform.Find("Hinge");
        doorPosOpen = Quaternion.Euler(0f, 0f, hinge.rotation.eulerAngles.z + doorPosOpenAngle);
        doorClose = Quaternion.Euler(0f, 0f, hinge.rotation.eulerAngles.z + doorCloseAngle);
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public IEnumerator animateDoor(Quaternion destination)
    {
        moving = true;
        while (Math.Abs(Quaternion.Angle(hinge.localRotation, destination)) > snapAngle)
        {
            hinge.localRotation = Quaternion.Slerp(hinge.localRotation, destination, Time.deltaTime * animSpeed);
            yield return null;
        }
        open = !open;
        moving = false;

        yield return null;
    }


    public void Use(float flt)
    {
        if (locked && Keyring.doorKeys.Contains(key))
        {
            locked = false;
        }
        if (moving == false && !locked)
        {
            if (open) StartCoroutine(animateDoor(doorClose));
            else
            {
                StartCoroutine(animateDoor(doorPosOpen));
            }
        }
    }
}
