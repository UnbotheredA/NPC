using UnityEngine;

public class bullet : MonoBehaviour
{
    private float bulletLifeSpan;
    private float speedOfBullet = 20;
    //Rigidbody2D bulletRigid;
    private Transform target;
    Vector2 targetVector;
    private GameObject farRangeNPC;

    void Start()
    {
        //TODO bullets can not be shot if the player is further away from close range
        //bulletRigid = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Trigger").transform;
        targetVector = new Vector2(target.position.x, target.position.y);
    }
    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetVector, speedOfBullet * Time.deltaTime);
        // Using raycast so we know the bullet actually touched an object
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, fwd, 5);
        if (hit)
        {
            //print("There is something in front of the object!");
            //Debug.DrawRay(transform.position, fwd, Color.red);
            //|| 
            if (hit.collider.gameObject.name == "Trigger" || hit.collider.transform.position.x == targetVector.x && transform.position.y == targetVector.y)
            {
                // the first statement checks if the bullet hit the player then delete bullet 
                //second arg is if the bullet has been launched then delete
                    //Debug.Log("bullet hits player");
                    Destroy(gameObject);
            }
            else if (hit.collider.gameObject.tag == "Obstacle")
            {
                //Debug.Log("bullets hit Obstacle");
                Destroy(gameObject);
            }
            else if (hit.collider.gameObject.tag == "CloseRangeEnemy")
            {
                //Debug.Log("bullet hit close range npc");
                //So that the bullet does not hit the close range npc and make it move??
            }
        }
    }
}
