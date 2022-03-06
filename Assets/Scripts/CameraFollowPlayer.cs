using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    //public Rigidbody2D Trigger;

    public Transform Trigger;
   
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Trigger.position.x + offset.x, Trigger.position.y + offset.y, offset.z); // Camera follows the player with specified offset position
        if (transform.position.x > Screen.width) 
        {
            Debug.Log("player left");
        }
    }
}



//Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, gameObject.transform.position + Vector3(offsetX, offsetY, offsetZ), Time.deltaTime * Speed);
//https://answers.unity.com/questions/169346/limit-the-player-so-he-cant-go-off-screen.html



