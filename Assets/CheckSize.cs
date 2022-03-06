using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSize : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider2D m_Collider;
    public GameObject player;
    Vector2 playerCollider;
    public Bounds Bounds { get { return m_Collider.bounds; } }
    void Start()
    {
        m_Collider = GetComponent<BoxCollider2D>();
        player = GetComponent<GameObject>();
        playerCollider =  player.GetComponent<BoxCollider2D>().size;

        Debug.Log("Current BoxCollider Size : " + m_Collider.size);

    }
    // Update is called once per frame
    void Update()
    {
        Bounds bounds = m_Collider.bounds;
        Collider2D[] childColliders = GetComponentsInChildren<Collider2D>();
        foreach (Collider2D child in childColliders)
        {
            bounds.Encapsulate(child.bounds);
        }

        m_Collider.size = bounds.size;
        m_Collider.offset = bounds.center - m_Collider.transform.position;
        //Debug.Log("Current BoxCollider Size : " + m_Collider.size);
        if (player != null)
        {
            if (m_Collider.size.magnitude > playerCollider.magnitude)
            {
                //Debug.Log(m_Collider.size.magnitude);
                //Debug.Log(m_Collider.size.magnitude);
                Debug.Log("player can hide");
            }
            else
            {
                Debug.Log("player can't hide");

            }
        }
    }
}
