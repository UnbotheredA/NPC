using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveNPCCol : MonoBehaviour
{
    public Transform target;
    Rigidbody2D rb;
    // Start is called before the first frame update

    public bool landing = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        landing = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Trigger").transform;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void FixedUpdate()
    {
        Vector3 playerDirection = (target.transform.position - gameObject.transform.position).normalized;
        rb.MovePosition(gameObject.transform.position + playerDirection * 3 * Time.deltaTime);

       // if () 
       // {
        
       // }
        if (landing)
        {
            this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
            landing = false;
        }
    }
}
