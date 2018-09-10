using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;

public class ShadowBehaviour : MonoBehaviour {

    public float waypointRadius = 0.5f;
    public GameObject lookAtObject;
    public float lookAtDistance = 1.0f;
    public float animSpeed = 1.0f;

    bool animating = false;
    List<Transform> targets = new List<Transform>();
    NavMeshAgent agent;
    Animator aController;
    AimConstraint aimConstraint;
    int targetIndex = -1;

    void Awake()
    {
        gameObject.AddComponent<AimConstraint>();
    }

    void Start ()
    {
        // Aim Constraint
        aimConstraint = gameObject.GetComponent<AimConstraint>();
        aController = GetComponent<Animator>();
        ConstraintSource cc = new ConstraintSource();
        cc.sourceTransform = lookAtObject.transform;
        cc.weight = 1.0f;
        aimConstraint.AddSource(cc);
        aimConstraint.rotationAxis = Axis.Y | Axis.Z;
        // Aim Constraint

        // populating waypoints
        foreach (Transform i in GameObject.Find("targets").GetComponentsInChildren<Transform>())
        {
            targets.Add(i);
        }
        targets.RemoveAt(0); // done populating waypoints

        agent = GetComponent<NavMeshAgent>();
        StartCoroutine("AnimationState");
        NextTarget();
        GotoTargetIndex();
    }

    void GotoTargetIndex()
    {
        agent.SetDestination(targets[targetIndex].position);
    }

    void NextTarget()
    {
        int i = targetIndex;
        while (i == targetIndex)
        {
            i = Random.Range(0, targets.Count);
        }
        targetIndex = i;
    }
	
	// Update is called once per frame
	void Update () {
        if (!animating)
        {
            if ((lookAtObject.transform.position - gameObject.transform.position).magnitude < lookAtDistance)
            {
                StartCoroutine(LookAtObject());
                return;
            }
            if (!agent.pathPending && agent.remainingDistance < waypointRadius)
            {
                NextTarget();
                GotoTargetIndex();
            }
        }
    }

    IEnumerator AnimationState()
    {
        bool idle = true;
        yield return null;
        while (true)
        {
            if (agent.velocity.magnitude > 0.1f && idle)
            {
                idle = false;
                aController.Play("shadow_walk");
            }
            if (agent.velocity.magnitude < 0.1f && !idle)
            {
                idle = true;
                aController.Play("shadow_idle");
            }
            yield return null;
        }
    }

    IEnumerator LookAtObject()
    {
        animating = true;
        aimConstraint.rotationAtRest = gameObject.transform.eulerAngles;
        agent.isStopped = true;
        aimConstraint.constraintActive = true;
        aimConstraint.weight = 0.0f;
        yield return null;
        while (aimConstraint.weight < 0.9f)
        {
            aimConstraint.weight += Time.deltaTime * animSpeed;
            yield return null;
        }
        aimConstraint.weight = 1.0f;
        StartCoroutine(LookingAtObject());
        yield return null;
    }

    IEnumerator LookingAtObject()
    {
        while ((lookAtObject.transform.position - gameObject.transform.position).magnitude < lookAtDistance)
        {
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(BackToNavigating());
        yield return null;
    }

    IEnumerator BackToNavigating()
    {
        while (aimConstraint.weight > 0.1f)
        {
            aimConstraint.weight -= Time.deltaTime * animSpeed;
            yield return null;
        }
        aimConstraint.weight = 0.0f;
        aimConstraint.constraintActive = false;
        animating = false;
        agent.isStopped = false;
        yield return null;
    }
}
