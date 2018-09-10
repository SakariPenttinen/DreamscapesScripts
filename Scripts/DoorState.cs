using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DoorState : MonoBehaviour {
    public float doorPosOpenAngle = 90.0f;
    public float doorNegOpenAngle = -90.0f;
    public float doorCloseAngle = 0.0f;
    public float snapAngle = 4f;
    private Quaternion doorPosOpen = Quaternion.identity;
    private Quaternion doorNegOpen = Quaternion.identity;
    private Quaternion doorClose = Quaternion.identity;
    bool open = false;
    public String key = "NO_KEY";
    bool moving = false;
    public bool locked = false;
    public float animSpeed = 2f;

    // Use this for initialization
    void Start () {
        doorPosOpen = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + doorPosOpenAngle, 0f);
        doorNegOpen = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + doorNegOpenAngle, 0f);
        doorClose = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + doorCloseAngle, 0f);
	}
	
	// Update is called once per frame
	void Update () {
	}
    
    public IEnumerator animateDoor(Quaternion destination)
    {
        moving = true;
        while (Math.Abs(Quaternion.Angle(gameObject.transform.localRotation, destination)) > snapAngle)
        {
            gameObject.transform.localRotation = Quaternion.Slerp(transform.localRotation, destination, Time.deltaTime * animSpeed);
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
                if (flt > 0) StartCoroutine(animateDoor(doorPosOpen));
                else StartCoroutine(animateDoor(doorNegOpen));
            }
        }
    }
}
