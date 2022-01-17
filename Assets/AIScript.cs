using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIScript : MonoBehaviour
{
    private NavMeshAgent nm;

    public Transform target;

    public float distanceAware = 50f;
    public float distanceUnAware = 100f;
    public enum AIState
    {
        idle,
        chasing
        
    }

    public AIState aiState = AIState.idle;

    // Start is called before the first frame update
    void Start()
    {
        nm = GetComponent<NavMeshAgent>();

        StartCoroutine(Think());

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    public void Death()
    {
        nm.speed = 0.0f;
            
    }
    
    IEnumerator Think()
    {
        while (true)
        {
            switch (aiState)
            {
                case AIState.idle:
                    float dist = Vector3.Distance(target.position, transform.position);
                    if (dist < distanceAware)
                    {
                        aiState = AIState.chasing;
                    }
                    nm.SetDestination(transform.position);
                    break;
                case AIState.chasing:
                    dist = Vector3.Distance(target.position, transform.position);
                    if (dist > distanceUnAware)
                    {
                        aiState = AIState.idle;
                    }
                    nm.SetDestination(target.position);
                    break;
                default:
                    break;
                    
            }
            
            
            
            yield return new WaitForSeconds(1f);
        }
    }
}
