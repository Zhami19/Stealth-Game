using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Player target;
    [SerializeField] Sensor sensor;

    [SerializeField] float speed;

    [SerializeField] Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int patrolPointIndex = 0;

    [SerializeField] float rotationSpeed;

    public enum GuardStates
    {
        Patrol,
        Investigate,
        Pursue
    }

    public GuardStates guardState;


    void Start()
    {
        // Initial patrol behavior
        guardState = GuardStates.Patrol;
        currentPatrolPoint = patrolPoints[0];
        agent.SetDestination(currentPatrolPoint.position);
    }

    // Update is called once per frame
    void Update()
    {
        switch(guardState)
        {
            case GuardStates.Patrol:
                Patrolling();
                break;
            case GuardStates.Investigate:
                Investigating();
                break;
            case GuardStates.Pursue:
                break;
        }
    }

    public void Patrolling()
    {
        float distance = Vector3.Distance(transform.position, currentPatrolPoint.position);

        if (distance < 1)
        {
            patrolPointIndex++;

            if (patrolPointIndex >= patrolPoints.Length)
            {
                patrolPointIndex = 0;
            }

            currentPatrolPoint = patrolPoints[patrolPointIndex];
            agent.SetDestination(currentPatrolPoint.position);
        }

        Debug.DrawLine(transform.position, currentPatrolPoint.position, Color.yellow);
    }

    public void Investigating()
    {
        Debug.Log("Investigating");
        Vector3 direction = sensor.LastHeard - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
