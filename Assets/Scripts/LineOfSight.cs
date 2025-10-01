using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    [SerializeField] Transform target;
    Guard guard;

    Vector3 directionToTarget;
    Vector3 forwardDirection;
    float dot;

    [SerializeField] float viewDistance = 5f;
    [SerializeField, Range(-1f, 1f)] float ordinate = .5f;

    [SerializeField] float investigationTime = 3f;
    float originalInvestigationTime;
    public float ViewDistance => viewDistance;
    
    void Start()
    { 
        guard = GetComponent<Guard>();  
        Gizmos.color = Color.red;
        originalInvestigationTime = investigationTime;
    }

    public void InitialInvestigationTime()
    {
        investigationTime = originalInvestigationTime;
    }

    public void SightDetection()
    {
        investigationTime -= Time.deltaTime;

        directionToTarget = (target.position - transform.position).normalized;
        forwardDirection = transform.forward;

        dot = Vector3.Dot(forwardDirection, directionToTarget);
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        if ((dot > ordinate) && (distanceToTarget <= viewDistance))
        {
            Debug.Log("He is in front");
            guard.guardState = Guard.GuardStates.Pursue;
        }
        else if (investigationTime <= 0f)
        {
            investigationTime = originalInvestigationTime;
            guard.InitialPatrol();
            guard.guardState = Guard.GuardStates.Patrol;
        }

        if ((dot < -ordinate) && (distanceToTarget <= viewDistance))
            Debug.Log("He is behind");
    }

    private void OnDrawGizmos()
    {
        // Calculate viewAngle from ordinate
        float viewAngle = Mathf.Acos(ordinate) * Mathf.Rad2Deg;

        // Draw range circle
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewDistance);

        // Unit forward vector
        Vector3 forward = transform.forward;

        // Rotate first
        Vector3 leftDir = Quaternion.Euler(0, -viewAngle, 0) * forward;
        Vector3 rightDir = Quaternion.Euler(0, viewAngle, 0) * forward;

        // Then scale by viewDistance
        leftDir *= viewDistance;
        rightDir *= viewDistance;
        Vector3 centerDir = forward * viewDistance;

        // Draw lines
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + leftDir);
        Gizmos.DrawLine(transform.position, transform.position + rightDir);
        Gizmos.DrawLine(transform.position, transform.position + centerDir);
    }
}

