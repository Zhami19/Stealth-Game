using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;


public class Guard : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform target;
    CharacterController characterController;
    NavMeshPath navPath;

    [SerializeField] float speed;
    Vector3 currentTargetPoint;
    Queue<Vector3> remainingPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        navPath = new NavMeshPath();
        remainingPoints = new Queue<Vector3>();

        if (agent.CalculatePath(target.position, navPath))
        {
            Debug.Log("found path to target");
            foreach(Vector3 point in navPath.corners)
            {
                remainingPoints.Enqueue(point);
            }

            currentTargetPoint = remainingPoints.Dequeue();
        }
    }

    // Update is called once per frame
    void Update()
    {
        var new_forward = (currentTargetPoint - transform.position).normalized;
        new_forward.y = 0;
        transform.forward = new_forward;

        float distToPoint = Vector3.Distance(transform.position, currentTargetPoint);

        if(distToPoint < 1)
        {
            currentTargetPoint = remainingPoints.Dequeue();
        }

        characterController.Move(new_forward * speed * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        if (navPath == null)
            return;

        Gizmos.color = Color.red;
        foreach(Vector3 node in navPath.corners)
        {
            Gizmos.DrawWireSphere(node, .5f);
        }
    }
}
