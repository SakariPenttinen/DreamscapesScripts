using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogLogic : MonoBehaviour {
    public GameObject boat;
    BoatLogic boatLogic;
    GameObject keyDummy;
    GameObject keyReal;
    GameObject doorFrame;
    GameObject tigerblood;
    public float eatDistance = 2f;
    Animator anim;
    bool keyDropped = false;

	// Use this for initialization
	void Start () {
        boatLogic = GameObject.Find("_boatLogic").GetComponent<BoatLogic>();
        anim = GetComponent<Animator>();
        keyDummy = GameObject.Find("key");
        keyReal = GameObject.Find("keyReal");
        keyReal.SetActive(false);
        doorFrame = GameObject.Find("doorFrame");
        tigerblood = GameObject.Find("tigerblood");
        tigerblood.SetActive(false);
    }
	float PDist()
    {
        return Vector3.Distance(transform.position, boat.transform.position);
    }
	// Update is called once per frame
	void Update () {
		if (!keyDropped && Keyring.doorKeys.Contains("fly") && PDist() <= eatDistance)
        {
            Keyring.portalKeys.Add("credits");
            tigerblood.SetActive(true);
            doorFrame.GetComponentInChildren<Animator>().Play("open");
            boatLogic.DisableDummyFly();
            anim.Play("frog_eat");
            keyDropped = true;
            keyReal.SetActive(true);
            keyDummy.SetActive(false);
            StartCoroutine("FrogAnim");
        }
	}

    IEnumerator FrogAnim()
    {
        yield return new WaitForSecondsRealtime(19f);
        while (true)
        {
            yield return new WaitForSecondsRealtime(9.5f);
            anim.Play("frog_idle");
            yield return new WaitForSecondsRealtime(9.5f);
            anim.Play("frog_eat");
        }
    }
}
