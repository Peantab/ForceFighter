using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public DestructionType destructionType = DestructionType.FAILURE;
    public GameObject brokerHolder;
    public GameObject healthText;
    public GameObject effect;

    private Broker broker;
    private HealthController health;

    // Start is called before the first frame update
    void Start()
    {
        broker = brokerHolder.GetComponent<Broker>();

        if (destructionType == DestructionType.FAILURE)
        {
            health = healthText.GetComponent<HealthController>();
        }
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
            broker.Deregister(thrownObject);
            Destroy(thrownObject);

            if (destructionType == DestructionType.FAILURE)
            {
                health.AcceptDamage(20);
            }
            else
            {
                var particleSystem = effect.GetComponent<ParticleSystem>();
                GameObject gameObject = Instantiate(effect, thrownObject.transform.position, Quaternion.identity);
                Destroy(gameObject, particleSystem.main.duration);
            }
        }
    }

    public enum DestructionType {
        SUCCESS, FAILURE
    }
}
