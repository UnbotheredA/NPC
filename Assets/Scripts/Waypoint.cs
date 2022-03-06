using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public Waypoint nextWaypoint;
    private void Awake()
    {

    }
    void Start()
    {

    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        var npcs = collider.gameObject.GetComponent<NPC>();
        if (npcs != null)
        {
            npcs.NewWaypoint(nextWaypoint);
        }
    }
}

