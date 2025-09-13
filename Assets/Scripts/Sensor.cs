using UnityEngine;

public class Sensor : MonoBehaviour
{
    [SerializeField] LineOfSight lineOfSight;
    [SerializeField] Guard guard;
    [SerializeField] Player player;

    private Vector3 lastHeard;

    public Vector3 LastHeard => lastHeard;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Update()
    {
        float diameter = lineOfSight.ViewDistance * 2;
        transform.localScale = new Vector3(diameter, diameter, diameter);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.tag == "Player") && (player.playerState == Player.PlayerStates.Normal))
        {
            lastHeard = player.transform.position;
            guard.guardState = Guard.GuardStates.Investigate;
        }
    }
}
