using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Broker : MonoBehaviour
{
    private Dictionary<ScreenSegment, List<GameObject>> spawnedObjects;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects = new Dictionary<ScreenSegment, List<GameObject>>();

        foreach (ScreenSegment segment in Enum.GetValues(typeof(ScreenSegment)))
        {
            spawnedObjects.Add(segment, new List<GameObject>());
        }
    }

    public void Register(ScreenSegment segment, GameObject gameObject)
    {
        spawnedObjects[segment].Add(gameObject);
    }

    public void Deregister(GameObject gameObject)
    {
        foreach (ScreenSegment segment in Enum.GetValues(typeof(ScreenSegment)))
        {
            spawnedObjects[segment].Remove(gameObject);
        }
    }

    public void ApplyPlayersForce(ScreenSegment segment)
    {
        foreach (GameObject gameObject in spawnedObjects[segment])
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 10));
        }
    }
}
