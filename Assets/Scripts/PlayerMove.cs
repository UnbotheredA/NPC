using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rbody;

    public float speed = 2f;

    public Camera cam;

    public Animation anim;

    public SwordAttack Sword;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Attack();
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 perpendicular = gameObject.transform.position - mousePos;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, perpendicular);
    }

    public void Attack()
    {
        //Maybe call this method on FixedUpdate
        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            if (Sword && anim != null)
            {
                StartCoroutine(Sword.PlayerSwordAttack(1));
                anim.Play("SwingSword");
                //Debug.Log(Sword.isAttacking);
            }
        }
    }
    private void FixedUpdate()
    {
        rbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized * speed;
    }
}


