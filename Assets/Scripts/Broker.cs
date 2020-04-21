using System;
using System.Collections.Generic;
using UnityEngine;


public class Broker : MonoBehaviour
{
    private Dictionary<ScreenSegment, List<GameObject>> spawnedObjects;
    private Dictionary<GameObject, ScreenSegment> inverseDictionary;

    // Start is called before the first frame update
    void Start()
    {
        spawnedObjects = new Dictionary<ScreenSegment, List<GameObject>>();
        inverseDictionary = new Dictionary<GameObject, ScreenSegment>();

        foreach (ScreenSegment segment in Enum.GetValues(typeof(ScreenSegment)))
        {
            spawnedObjects.Add(segment, new List<GameObject>());
        }
    }

    public void Register(ScreenSegment segment, GameObject gameObject)
    {
        spawnedObjects[segment].Add(gameObject);
        inverseDictionary[gameObject] = segment;
    }

    public void Deregister(GameObject gameObject)
    {
        inverseDictionary.Remove(gameObject);
        foreach (ScreenSegment segment in Enum.GetValues(typeof(ScreenSegment)))
        {
            spawnedObjects[segment].Remove(gameObject);
        }
    }

    public void ApplyPlayersForce(ScreenSegment segment)
    {
        foreach (GameObject gameObject in spawnedObjects[segment])
        {
            gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 14));
        }
    }

    public void Land(GameObject gameObject)
    {
        ScreenSegment segment = inverseDictionary[gameObject];
        if (segment == ScreenSegment.TOP_LEFT)
        {
            inverseDictionary[gameObject] = ScreenSegment.BOTTOM_LEFT;
            spawnedObjects[ScreenSegment.BOTTOM_LEFT].Add(gameObject);
            spawnedObjects[ScreenSegment.TOP_LEFT].Remove(gameObject);
        }
        else if (segment == ScreenSegment.TOP_RIGHT)
        {
            inverseDictionary[gameObject] = ScreenSegment.BOTTOM_RIGHT;
            spawnedObjects[ScreenSegment.BOTTOM_RIGHT].Add(gameObject);
            spawnedObjects[ScreenSegment.TOP_RIGHT].Remove(gameObject);
        }
    }
}
