using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class BearBehaviour : MonoBehaviour {
    public float waypointRadius = 0.5f;
    public float playerSearchDistance = 10.0f;
    public float speedMult = 2f;
    public float attackDistance = 2f;
    public GameObject player;
    AudioSource audioS;

    LureItem honeypot;
    int targetIndex = -1;
    bool patrolling = false;
    bool attacking = false;
    List<Transform> waypoints = new List<Transform>();
    NavMeshAgent agent;
    Animator aController;

    void GoToTarget(Transform loc)
    {
        agent.SetDestination(loc.position);
    }

    void GoToTargetIndex()
    {
        GoToTarget(waypoints[targetIndex]);
    }

    float DistanceToPlayer()
    {
        return (transform.position - player.transform.position).magnitude;
    }

    void NextTarget()
    {
        int i = targetIndex;
        while (i == targetIndex)
        {
            i = Random.Range(0, waypoints.Count);
        }
        targetIndex = i;
    }

    void Patrol()
    {
        GoToTargetIndex();
        StartCoroutine("LookForPlayer");
        StartCoroutine("LookForHoney");
        StartCoroutine("DoPatrol");
    }

    void StopAllBut(string s)
    {
        StopAllCoroutines();
        StartCoroutine(s);
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
                aController.Play("Walking");
            }
            if (agent.velocity.magnitude < 0.1f && !idle)
            {
                idle = true;
                aController.Play("Idle");
            }
            yield return null;
        }
    }

    IEnumerator DoPatrol()
    {
        patrolling = true;
        while (patrolling)
        {
            if (!agent.pathPending && agent.remainingDistance < waypointRadius)
            {
                NextTarget();
                GoToTargetIndex();
            }
            yield return null;
        }
        yield return null;
    }

    IEnumerator LookForHoney()
    {
        while (!honeypot.IsInLureZone())
        {
            yield return new WaitForSecondsRealtime(1.0f);
        }
        StopCoroutine("DoPatrol");
        patrolling = false;
        StopCoroutine("LookForPlayer");
        GoToTarget(honeypot.transform);
        StopAllBut("AnimationState");
        yield return null;
    }

    IEnumerator LookForPlayer()
    {
        while ( DistanceToPlayer() > playerSearchDistance )
        {
            yield return new WaitForSecondsRealtime(1.0f);
        }
        StartCoroutine("AttackPlayer");
        yield return null;
    }

    IEnumerator HasPlayerEscaped()
    {
        while ( DistanceToPlayer() <= playerSearchDistance)
        {
            yield return new WaitForSecondsRealtime(1.0f);
        }
        attacking = false;
        Patrol();
        yield return null;
    }

    IEnumerator AttackPlayer()
    {
        StopCoroutine("DoPatrol");
        patrolling = false;
        StopCoroutine("LookForPlayer");
        StopCoroutine("LookForHoney");
        attacking = true;

        StartCoroutine("HasPlayerEscaped");
        agent.speed *= speedMult;
        audioS.loop = true;
        audioS.Play();
        yield return null;
        while (attacking)
        {
            GoToTarget(player.transform);
            if (Vector3.Distance(transform.position, player.transform.position) <= attackDistance)
            {
                SceneManager.LoadScene(2);
            }
            yield return null;
        }
        audioS.loop = false;
        agent.speed /= speedMult;
        yield return null;
    }

    // Use this for initialization
    void Start ()
    {
        audioS = GetComponentInChildren<AudioSource>();
        honeypot = FindObjectOfType<LureItem>();
        agent = GetComponent<NavMeshAgent>();
        aController = GetComponentInChildren<Animator>();

        foreach (Transform i in GameObject.Find("targets").GetComponentsInChildren<Transform>())
        {
            waypoints.Add(i);
        }
        waypoints.RemoveAt(0); // done populating waypoints

        StartCoroutine("AnimationState");
        NextTarget();
        Patrol();
    }


    // Update is called once per frame
    void Update ()
    {

	}
}
