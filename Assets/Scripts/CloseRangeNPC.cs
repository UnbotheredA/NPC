using UnityEngine;

public class CloseRangeNPC : NPC
{
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<PlayerMove>())
        {
            //rbody
            if (rb != null)
            {
                speed = 0;
                Debug.Log(speed);
                Debug.Log("collision has happened");
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerMove>())
        {
            speed = 3;
        }
    }
}
