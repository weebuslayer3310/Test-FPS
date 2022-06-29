using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SnowMonsterController : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Animator anim = null;
    [SerializeField] private Transform target;
    private float stoppingDistance = 3.0f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        MoveToTarget();
    }

    /// <summary>
    /// Moving the monster to the player
    /// created by: NghiaDC (26/6/2022)
    /// </summary>
    private void MoveToTarget()
    {
        agent.SetDestination(target.position);

        anim.SetFloat("Speed", 1.0f, 0.3f, Time.deltaTime);

        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if(distanceToTarget <= stoppingDistance)
        {
            RotateToTarget();
            anim.SetFloat("Speed", 0.0f);
        }
    }

    /// <summary>
    /// rotate the monster to look at the direction of the player.
    /// Created by: NghiaDC (26/6/2022)
    /// </summary>
    private void RotateToTarget()
    {
        //transform.LookAt(target);

        Vector3 direction = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = rotation;
    }

    private void GetReferences()
    {
        
    }
}
