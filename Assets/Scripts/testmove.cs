using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testmove : MonoBehaviour
{
    public Transform target;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = GameObject.Find("Trigger").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(Vector2.right * 2);
        Vector3 playerDirection = (target.transform.position - transform.position).normalized;
        rb.MovePosition(transform.position + playerDirection * 4 * Time.deltaTime);
        //rb.MovePosition(transform.position  * Time.deltaTime * 5);
    }
}
