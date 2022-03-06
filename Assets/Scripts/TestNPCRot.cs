using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNPCRot : MonoBehaviour
{
    //private Transform target;
    public int MyRandomNum; 
    void Start()
    {
        //target = GameObject.Find("Trigger").transform;
        MyRandomNum = Random.Range(-45, 45);
        float randomRot = transform.rotation.z - MyRandomNum;


    }

    // Update is called once per frame
    void Update()
    {
       // transform.LookAt(target, Vector3.up);
    }
}
