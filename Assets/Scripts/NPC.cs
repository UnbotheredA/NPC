using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    // this script needs to be attached to NPC to update state

    //Turn this to protected after debugging;
    public float health = 10;
    public float removeHealthBy = 7;
    public float speed = 3;
    public float patrolSpeed = 1;
    public float hideSpeed = 7;
    public float chaseSpeed = 4;
    public float stopSpeed = 0;
    public float preivousSpeed = 0;
    public float attackSpeed = 5;


    public Vector2 heading;
    public Vector3 targetPos;
    //how far they can detect ahead
    public float lookAhead = 1;

    public Rigidbody2D rb;

    public Waypoint waypoint;

    public List<Waypoint> waypoints;

    //public GameObject playerMove;
    //TODO Instead of creating a target varaible in all the classess that need it. Initialze the value here one time and accesss it using NPC.
    //public GameObject target;
    public StateMachine movementSM;

    public Chase chase;

    public Patrol patrol;

    public Attack attack;

    public Hide hide;

    public bool isRandom;
    public virtual void Start()
    {
        GameObject[] waypointsArray = GameObject.FindGameObjectsWithTag("Waypoint");
        foreach (var item in waypointsArray)
        {
            waypoints.Add(item.GetComponent<Waypoint>());
        }
        rb = gameObject.GetComponent<Rigidbody2D>();
        movementSM = new StateMachine();
        patrol = new Patrol(this, movementSM);
        chase = new Chase(this, movementSM);
        attack = new Attack(this, movementSM);
        hide = new Hide(this, movementSM);
        movementSM.Initialize(patrol);
        InitializeWayPoint();
        //Debug.Log("npc start class");
    }
    public void InitializeWayPoint()
    {
        patrol.waypoint = waypoint;
    }
    public void NewWaypoint(Waypoint wp)
    {
        if (isRandom == true)
        {
            Waypoint currentWaypoint = patrol.waypoint;
            Waypoint newWaypoint = null;
            List<Waypoint> availableWaypoints = new List<Waypoint>(waypoints);
            availableWaypoints.Remove(currentWaypoint);
            int random = Random.Range(0, availableWaypoints.Count);
            newWaypoint = availableWaypoints[random];
            patrol.UpdateWaypoint(newWaypoint);
        }
        else
        {
            patrol.UpdateWaypoint(wp);
        }
    }
    public void TakeDamage()
    {
        health -= removeHealthBy;
    }

    public virtual void Update()
    {
        //heading is where you want to go or where the forces are pushing you
        //direction is what your facing
        heading = targetPos - transform.position;
        //decreased lookahead
        var fwd = transform.right * (lookAhead);

        if (movementSM != null && movementSM.CurrentState != null)
        {
            movementSM.CurrentState.LogicUpdate();
            //Debug.Log(patrol.waypoint + "Waypoints");
            //CheckNPCHealth();
        }
    }
    //point is the end of the line touching the circle
    //circle is the object that you want to avoid
    public bool Intersect(Vector3 point, GameObject circle)
    {
        //How big is the detection area
        float radius = 3f;
        //pos is the middle of the collider 
        //if a point is less than or equal to the radius intersection has happened
        return Vector3.Distance(circle.transform.position, point) <= radius;
    }
    public virtual void FixedUpdate()
    {
        if (movementSM != null && movementSM.CurrentState != null)
        {
            movementSM.CurrentState.PhysicsUpdate();
        }
    }
    private void CheckNPCHealth()
    {
        //TODO on enemy manager class

        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    //TODO add this to the close range npc
    protected virtual void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<PlayerMove>())
        {
            if (preivousSpeed == 0)
            {
                preivousSpeed = speed;
                speed = stopSpeed;
            }

        }
    }
    protected virtual void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerMove>())
        {
            speed = preivousSpeed;
            preivousSpeed = 0;

        }
    }
}

