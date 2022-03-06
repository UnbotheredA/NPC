using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomItemNeeded : MonoBehaviour
{
    // Start is called before the first frame update
    //spawn itemss varaible

    public int itemsNeeded;
    public int itemsPlayerHas;
    public int randomSpawn;

    public List<Transform> spawnPoints = new List<Transform>();

    public GameObject player;
    public GameObject itemToCollect;
    public List<GameObject> itemsToCollect; //= new GameObject[4];

    void Start()
    {
        itemsNeeded = Random.Range(2, 5);
        itemsPlayerHas = player.GetComponent<PlayerManager>().itemsCollected;
        SpawnRandomItems();
    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        var playerInRange = collision2D.gameObject.GetComponent<PlayerManager>();
        int itemsPlayerHas = playerInRange.itemsCollected;
        if (itemsPlayerHas == itemsNeeded)
        {
            Debug.Log("player has the right amount of items");
            SceneManager.LoadScene("Game Over");
        }
    }
    void SpawnRandomItems() 
    {
        for (int i = 0; i < itemsNeeded; i++)
        {
            //Item to be spawned to random spawn points.
            randomSpawn = Random.Range(0, spawnPoints.Count);
            int randomObjectNum = Random.Range(0, itemsToCollect.Count);
            //instantiate game object from array to a randomly selected spawnpoint
            Instantiate(itemsToCollect[randomObjectNum], spawnPoints[randomSpawn]);
            //Debug.Log(spawnPoints[randomSpawn]);
            //Debug.Log(itemsToCollect[randomObjectNum]);
            itemsToCollect[randomObjectNum].transform.localPosition = Vector3.zero;
            //remove the randomspawn since it is used now
            spawnPoints.RemoveAt(randomSpawn);
            //This allows some times to be spawned again
            if (!itemsToCollect[randomObjectNum].name.ToLower().Contains("rope"))
            {
                itemsToCollect.RemoveAt(randomObjectNum);
            }
        }
    }
    void SpawnTheSameItem()
    {
        for (int i = 0; i < itemsNeeded; i++)
        {
            randomSpawn = Random.Range(0, spawnPoints.Count);
            //Debug.Log("Random spawn is:" + randomSpawn);
            //Debug.Log("I is equal to: " + i);
            //Debug.Log(spawnPoints[randomSpawn]);
            Instantiate(itemToCollect, spawnPoints[randomSpawn]);
            itemToCollect.transform.localPosition = Vector3.zero;
            spawnPoints.RemoveAt(randomSpawn);
        }
    }
}

