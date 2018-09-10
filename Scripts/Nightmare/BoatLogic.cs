using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatLogic : MonoBehaviour {
    GameObject boatFps;
    GameObject boat;
    GameObject boatReturn;
    public GameObject fly;
    Transform boatTransform;
    Vector3 boatDir;
    GameObject fps;
    public GameObject prompt;
    bool oneShot = false;

    // Use this for initialization
    void Start () {
        fps = GameObject.Find("FPSController");
        boat = GameObject.Find("boat");
        boatFps = GameObject.Find("BoatRigidBodyFPSController");
        DisableDummyFly();
        boatTransform = boatFps.transform;
        boatDir = boatFps.transform.eulerAngles;
        boatFps.SetActive(false);
        boatReturn = GameObject.Find("BoatReturn");
        boatReturn.SetActive(false);
    }

    public void Swap()
    {
        boat.SetActive(!boat.activeSelf);
        boatFps.SetActive(!boatFps.activeSelf);
        fps.SetActive(!fps.activeSelf);
        boatFps.transform.SetPositionAndRotation(boatTransform.position, Quaternion.Euler(boatDir));
        if (Keyring.doorKeys.Contains("fly") && !oneShot)
        {
            oneShot = true;
            fly.SetActive(true);
        }
        if (!boatReturn.activeSelf) StartCoroutine("ReturnDelay");
        else boatReturn.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (boatFps.activeSelf)
        {
            prompt.SetActive(false);
        }
    }

    float BoatDist()
    {
        return Vector3.Distance(boatFps.transform.position, boatReturn.transform.position);
    }

    public void DisableDummyFly()
    {
        fly.SetActive(false);
    }

    IEnumerator ReturnDelay()
    {
        float dist = BoatDist();
        while (dist < 12f)
        {
            yield return new WaitForSecondsRealtime(0.5f);
            dist = BoatDist();
        }
        boatReturn.SetActive(true);
    }
}
