using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject throwableItem;
    public GameObject brokerHolder;

    private Vector3 spawnPoint;
    private int fixedUpdateModuloCounter = 0;
    private Broker broker;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z - 0.5f);
        broker = brokerHolder.GetComponent<Broker>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        fixedUpdateModuloCounter = (fixedUpdateModuloCounter + 1) % 50;
        if (fixedUpdateModuloCounter == 0)
        {
            if (Random.value < 0.25)
            {
                ThrowTopLeft();
            }
            else if (Random.value < 0.5)
            {
                ThrowBottomLeft();
            }
            else if (Random.value < 0.75)
            {
                ThrowTopRight();
            }
            else
            {
                ThrowBottomRight();
            }
        }
    }

    private void ThrowTopLeft()
    {
        GameObject spawnedObject = Instantiate(throwableItem, spawnPoint, Quaternion.identity);

        broker.Register(ScreenSegment.TOP_LEFT, spawnedObject);
    
        Rigidbody rigidBody = spawnedObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(50 * Vector3.left + 290 * Vector3.up + 300 * Vector3.back);
    }

    private void ThrowTopRight()
    {
        GameObject spawnedObject = Instantiate(throwableItem, spawnPoint, Quaternion.identity);

        broker.Register(ScreenSegment.TOP_RIGHT, spawnedObject);

        Rigidbody rigidBody = spawnedObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(50 * Vector3.right + 290 * Vector3.up + 300 * Vector3.back);
    }

    private void ThrowBottomLeft()
    {
        GameObject spawnedObject = Instantiate(throwableItem, spawnPoint, Quaternion.identity);

        broker.Register(ScreenSegment.BOTTOM_LEFT, spawnedObject);

        Rigidbody rigidBody = spawnedObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(50 * Vector3.left + 0 * Vector3.up + 300 * Vector3.back);
    }

    private void ThrowBottomRight()
    {
        GameObject spawnedObject = Instantiate(throwableItem, spawnPoint, Quaternion.identity);

        broker.Register(ScreenSegment.BOTTOM_RIGHT, spawnedObject);

        Rigidbody rigidBody = spawnedObject.GetComponent<Rigidbody>();
        rigidBody.AddForce(50 * Vector3.right + 0 * Vector3.up + 300 * Vector3.back);
    }
}
