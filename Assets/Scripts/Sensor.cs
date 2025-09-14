using UnityEngine;
using UnityEngine.Events;

public class Sensor : MonoBehaviour
{
    [SerializeField] LineOfSight lineOfSight;
    [SerializeField] Guard guard;
    [SerializeField] Player player;

    private Vector3 lastHeard;

    public UnityEvent OnInvestigate;
    public Vector3 LastHeard => lastHeard;

    private void Update()
    {
        float diameter = lineOfSight.ViewDistance * 2;
        transform.localScale = new Vector3(diameter, diameter, diameter);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided with something");

        var direction = (player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, direction);

        if (Physics.Raycast(ray, out RaycastHit hit, lineOfSight.ViewDistance))
        {
            Debug.DrawRay(transform.position, (player.transform.position - transform.position) * hit.distance, Color.yellow);
            if (hit.collider.tag == "Wall")
            {
                Debug.Log("Oh, it's just a wall...");
            }
            else if ((other.gameObject.tag == "Player") && (player.playerState == Player.PlayerStates.Normal))
            {
                lastHeard = player.transform.position;
                lineOfSight.InitialInvestigationTime();
                OnInvestigate?.Invoke();
                guard.guardState = Guard.GuardStates.Investigate;
            }
        }
    }
}
