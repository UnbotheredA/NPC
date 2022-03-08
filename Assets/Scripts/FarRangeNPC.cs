using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarRangeNPC : NPC
{
    public GameObject bullet;
    public float speedOfBullet = 3f;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //This can be done on the enter method
        //StartCoroutine(Attack());
    }

    //Update is called once per frame
    public override void Update()
    {
        base.Update();

    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    IEnumerator Attack()
    {
        // for debugging purposes
        int x = 1;
        while (x > 0)
        {
            //make it so they only shoot when player is in range intead of the x and while loop
            //bullets needs to aim at the player
            Vector3 position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
            Instantiate(bullet, position, Quaternion.identity);
            //make a varaible that is called speedbullet and pass it here.
            //make it so that it only attacks when the state has been picked attack.
            yield return new WaitForSeconds(1f);
        }
    }
}



