using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Guard : MonoBehaviour
{
    // References
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Player target;
    LineOfSight lineOfSight;
    //Pursue pursue;
    [SerializeField] Sensor sensor;

    // Variables
    [SerializeField] float originalSpeed;

    // Patrolling
    [SerializeField] Transform[] patrolPoints;
    Transform currentPatrolPoint;
    int patrolPointIndex = 0;

    // Investigating
    [SerializeField] float rotationSpeed;

    //UnityEvents
    public UnityEvent OnLose;
    

    public enum GuardStates
    {
        Patrol,
        Investigate,
        Pursue
    }

    public GuardStates guardState;


    void Start()
    {
        lineOfSight = GetComponent<LineOfSight>();

        // Initial patrol behavior
        guardState = GuardStates.Patrol;
        InitialPatrol();
        agent.SetDestination(currentPatrolPoint.position);
    }

    public void InitialPatrol()
    {
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
                Pursuing();
                break;
        }
    }

    public void Patrolling()
    {
        Debug.Log("Patrolling");

        agent.speed = originalSpeed;
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

        agent.speed = originalSpeed;

        Vector3 direction = sensor.LastHeard - transform.position;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        lineOfSight.SightDetection();
    }

    public void Pursuing()
    {
        Debug.Log("Pursuing");

        agent.speed *= 1.3f;

        float distance = Vector3.Distance(transform.position, target.transform.position);
        agent.SetDestination(target.transform.position);

        if (distance > lineOfSight.ViewDistance)
        {
            guardState = GuardStates.Investigate;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Time.timeScale = 0f;
            OnLose.Invoke();
        }
    }
}
