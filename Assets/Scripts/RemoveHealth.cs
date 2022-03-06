using UnityEngine;

public class RemoveHealth : MonoBehaviour
{
    public float health = 10;
    public float removeHealthBy = 7;
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Sword") && collider.gameObject.GetComponent<Animation>().isPlaying)
        {
            health -= removeHealthBy;
            if (health <= 0)
            {
                Debug.Log("woops");
                gameObject.SetActive(false);
            }
        }
    }
}
