using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Image cooldown;

    public bool coolingDown;

    public float waitTime = 5f;

    public int itemsCollected = 0;
    void Update()
    {
        Respawn();
    }
    public void Respawn()
    {
        if (cooldown.fillAmount == 0)
        {
            transform.position = new Vector3(47.46f, 0.102f, 0);
            cooldown.fillAmount = 100;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet" && coolingDown == true)
        {
            //Destroy(collision.gameObject);
            cooldown.fillAmount -= 5.0f / waitTime * Time.deltaTime;
            //Debug.Log(cooldown.transform.position);
        }
    }
    //private void OnTriggerStay2D(Collider2D collision)
    //{
    //    if (collision.gameObject.tag == "CloseRangeEnemy" && coolingDown == true)
    //    {
    //        cooldown.fillAmount -= 2.0f / waitTime * Time.deltaTime;
    //        //Consider replacing it with a OnTriggerStay2D(), because OnTriggerEnter2D() is a one - time event
    //        //As an alternative you can create a Coroutine with continuous damage.Then you can start it from OnTriggerEnter2D() and stop when OnTriggerExit2D() happen
    //    }
    //}
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "CloseRangeEnemy" && coolingDown == true) 
        {
            cooldown.fillAmount -= 2.0f / waitTime * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Pickup"))
        {
            itemsCollected += 1;
            Destroy(collision2D.gameObject);
        }
    }
}



