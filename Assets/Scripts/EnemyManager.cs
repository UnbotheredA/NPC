using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> FarRangeNPCPrefab = new List<GameObject>();
    public List<GameObject> CloseRangeNPCPrefabs = new List<GameObject>();
    public Vector3 spawnEnemiesPos = new Vector3(-10, 0, 0); // make one for close range and far range?
    public GameObject closeRangeNPC;
    public Transform target;

    void Start()
    {
        spawnEnemies(3f);
        //closeRangeNPC = GameObject.Find("CloseRangeNPC(Clone)");
        //target = GameObject.Find("Trigger").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //closeRangeNPC.transform.position = Vector3.MoveTowards(closeRangeNPC.transform.position, target.position, 3 * Time.deltaTime);
    }
    //int spawnCount
    void spawnEnemies(float yPos)
    {
        for (int i = 0; i < FarRangeNPCPrefab.Count; i++)
        {
            for (int x = 0; x < CloseRangeNPCPrefabs.Count; x++)
            {
                Instantiate(FarRangeNPCPrefab[i], spawnEnemiesPos, Quaternion.identity);
                Instantiate(CloseRangeNPCPrefabs[x], spawnEnemiesPos, Quaternion.identity);
                spawnEnemiesPos.y += yPos;
            }
        }
    }

}
