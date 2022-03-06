using UnityEngine;

public class NPC : MonoBehaviour
{
    // this script needs to be attached to NPC to update state
    //These NPC do not have to have actual bars.

    //Turn this to protected after debugging
    public float health = 10;
    public float removeHealthBy = 7;
    public float speed = 3;

    private Rigidbody2D rb;

    public Waypoint waypoint;

    //public GameObject playerMove;
    //TODO Instead of creating a target varaible in all the classess that need it. Initialze the value here one time and accesss it using NPC.
    //public GameObject target;
    public StateMachine movementSM;

    public Chase chase;
    
    public Patrol patrol;
    
    public Attack attack;

    public Hide hide;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        movementSM = new StateMachine();
        patrol = new Patrol(this, movementSM);
        chase = new Chase(this, movementSM);
        attack = new Attack(this, movementSM);
        hide = new Hide(this, movementSM);
        movementSM.Initialize(patrol);
        InitializeWayPoint();
    }
    public void InitializeWayPoint()
    {
        patrol.waypoint = waypoint;
    }
    public void NewWaypoint(Waypoint wp)
    {
        patrol.UpdateWaypoint(wp);
    }
    public void TakeDamage()
    {
        health -= removeHealthBy;
    }

    private void Update()
    {
        if (movementSM != null && movementSM.CurrentState != null)
        {
            movementSM.CurrentState.LogicUpdate();
            //Debug.Log(patrol.waypoint);
            //CheckNPCHealth();
        }
    }
    private void FixedUpdate()
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
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<PlayerMove>())
        {
            speed = 0;
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerMove>())
        {
            if (rb != null)
            {
                rb.bodyType = RigidbodyType2D.Dynamic;
                speed = 3;
            }
        }
    }
}
