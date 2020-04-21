using UnityEngine;

public class FloorLanding : MonoBehaviour
{
    public GameObject brokerHolder;

    private Broker broker;

    // Start is called before the first frame update
    void Start()
    {
        broker = brokerHolder.GetComponent<Broker>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject thrownObject = collision.gameObject;
        if (thrownObject.CompareTag("ThrownObject"))
        {
            broker.Land(thrownObject);
        }
    }
}
