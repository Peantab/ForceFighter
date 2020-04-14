using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    public GameObject brokerHolder;

    private Broker broker;

    // Start is called before the first frame update
    void Start()
    {
        broker = brokerHolder.GetComponent<Broker>();
    }

    void FixedUpdate()
    {
        ScreenSegment segment;
        if (gameObject.transform.position.x < Screen.width / 2)
        {
            if (gameObject.transform.position.y > Screen.height / 2)
            {
                segment = ScreenSegment.TOP_LEFT;
            }
            else
            {
                segment = ScreenSegment.BOTTOM_LEFT;
            }
        }
        else
        {
            if (gameObject.transform.position.y > Screen.height / 2)
            {
                segment = ScreenSegment.TOP_RIGHT;
            }
            else
            {
                segment = ScreenSegment.BOTTOM_RIGHT;
            }
        }

        broker.ApplyPlayersForce(segment);
    }
}
