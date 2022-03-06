using UnityEngine;

public class CloseRangeNPC : NPC
{
    Rigidbody2D rbody;
  
    NPC npc;
  
    private void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<PlayerMove>())
        {
            if (rbody && npc != null)
            {
                npc.speed = 0;
                Debug.Log(speed);
                Debug.Log("collision has happened");
                rbody.bodyType = RigidbodyType2D.Kinematic;
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.GetComponent<PlayerMove>())
        {
            if (rbody && npc != null)
            {
                rbody.bodyType = RigidbodyType2D.Dynamic;
                npc.speed = 3;
            }
        }
    }
}
