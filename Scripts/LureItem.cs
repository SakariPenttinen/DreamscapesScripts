using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LureItem : MonoBehaviour {
    public float zoneSize = 1.0f;
    List<Transform> lureZones = new List<Transform>();
    GrabObject grabObject;
    bool inTheZone = false;

    public bool IsInLureZone()
    {
        return inTheZone;
    }

    IEnumerator CheckInTheZone()
    {
        while (!inTheZone)
        {
            foreach (Transform i in lureZones)
            {
                if (!grabObject.IsCarried() && LureIsCloseEnough(i))
                {
                    inTheZone = true;
                }
            }
            //yield return new WaitForSecondsRealtime(2.0f);
            yield return null;
        }
        yield return null;
    }

    bool LureIsCloseEnough(Transform i)
    {
        return (i.position - transform.position).magnitude <= zoneSize;
    }

    // Use this for initialization
    void Start () {
        grabObject = gameObject.GetComponent<GrabObject>();

        foreach (Transform i in GameObject.Find("lures").GetComponentsInChildren<Transform>())
        {
            lureZones.Add(i);
        }
        // done populating waypoints
        StartCoroutine(CheckInTheZone());
    }
	
	// Update is called once per frame
	void Update () {
	}
}
