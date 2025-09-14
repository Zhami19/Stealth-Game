using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class Visuals : MonoBehaviour
{
    [SerializeField] LineOfSight lineOfSight;
    [SerializeField] Transform guard;
    [SerializeField] Light spotLight;

    private void Update()
    {
        transform.position = guard.position;
        float diameter = lineOfSight.ViewDistance * .11f;
        transform.localScale = new Vector3(diameter, diameter, diameter);
        spotLight.range = lineOfSight.ViewDistance;
    }
}
