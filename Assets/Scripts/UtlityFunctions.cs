using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtlityFunctions 
{

    public static GameObject FindNearestObject(GameObject[] allObjects, GameObject obj)
    {
        GameObject nearest = null;
        float minDist = Mathf.Infinity;

        for (int i = 0; i < allObjects.Length; i++)
        {
            Vector3 distanceToObstacle = obj.gameObject.transform.position - allObjects[i].transform.position;

            if (distanceToObstacle.sqrMagnitude < minDist)
            {
                minDist = (obj.gameObject.transform.position - allObjects[i].transform.position).sqrMagnitude;
                nearest = allObjects[i];
                //Debug.Log(currentObstacle.name);
            }
        }
        return nearest;
    }


    public static GameObject FindFurthestObject(GameObject[] allObjects,GameObject obj)
    {
        GameObject furthest = null;
        float maxDist = 0;

        for (int i = 0; i < allObjects.Length; i++)
        {
            Vector3 distanceToObstacle = obj.gameObject.transform.position - allObjects[i].transform.position;

            if (distanceToObstacle.sqrMagnitude > maxDist)
            {
                maxDist = (obj.gameObject.transform.position - allObjects[i].transform.position).sqrMagnitude;
                furthest = allObjects[i];
                //Debug.Log(currentObstacle.name);
            }
        }
        return furthest;
    }
    public static Vector3 FindSpotBehind(GameObject awayFrom, GameObject obj, float dist)
    {
        // the point between the objand the targetand making sure to always be further from the target
        Vector3 direction = obj.transform.position - awayFrom.transform.position;
        direction.Normalize();
        // to go behind the object obstacle keeping the distance from the obj
        return obj.transform.position + direction * dist;
    }
}
