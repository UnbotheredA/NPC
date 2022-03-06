using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public bool isAttacking;
    void Start()
    {
        isAttacking = false;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        var NPCinRange = collision.gameObject.GetComponent<NPC>();
        if (collision != null)
        {
            if (NPCinRange != null && isAttacking == true)
            {
                NPCinRange.TakeDamage();
                isAttacking = false;
            }
        }
    }
    public IEnumerator PlayerSwordAttack(float attackTime) 
    {
        isAttacking = true;
        yield return new WaitForSeconds(attackTime);
        isAttacking = false;
    }
}
