using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject obs;
    List<GameObject> obss = new List<GameObject>();
    private void Awake()
    {
        if (obss.Count == 0)
            for (int i = 0; i < 1; i++)
            {
                obs = Instantiate(gameObject, new Vector3(i * 2.0F, 0, 0), Quaternion.identity);
                Debug.Log("happened");
                obss.Add(obs);
            }
        else if (obss.Count < 3)
        {
            Destroy(obs);
        }
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {

    }
}
