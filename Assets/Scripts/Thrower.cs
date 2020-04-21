using UnityEngine;

public class Thrower : MonoBehaviour
{
    public GameObject[] throwableItems;
    public GameObject brokerHolder;

    private Vector3 spawnPoint;
    private int fixedUpdateModuloCounter = 0;
    private Broker broker;

    private int itemsCount;
    private ImportedItem[] items;
    private float randomMax;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z - 0.5f);
        broker = brokerHolder.GetComponent<Broker>();

        itemsCount = throwableItems.Length;
        items = new ImportedItem[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            items[i] = new ImportedItem(throwableItems[i]);
        }

        InitializeProbabilities();
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

            ImportedItem item = PickItem();
            ScreenSegment segment = PickScreenSegment();

            ThrowItem(item, segment);
        }
    }

    private void InitializeProbabilities()
    {
        float accumulator = 0;

        for (int i = 0; i < itemsCount; i++)
        {
            items[i].probabilityWeight = accumulator;
            accumulator += items[i].itemProperties.relativeFrequency;
        }

        randomMax = accumulator;
    }

    private ImportedItem PickItem()
    {
        float random = Random.Range(0, randomMax);

        for (int i = itemsCount-1; i > -1; i--)
        {
            if (items[i].probabilityWeight < random) return items[i];
        }

        return null;
    }

    private ScreenSegment PickScreenSegment()
    {
        ScreenSegment segment;
        float random = Random.value;

        if (random < 0.25)
        {
            segment = ScreenSegment.TOP_LEFT;
        }
        else if (random < 0.5)
        {
            segment = ScreenSegment.BOTTOM_LEFT;
        }
        else if (random < 0.75)
        {
            segment = ScreenSegment.TOP_RIGHT;
        }
        else
        {
            segment = ScreenSegment.BOTTOM_RIGHT;
        }

        return segment;
    }

    private void ThrowItem(ImportedItem item, ScreenSegment segment)
    {
        GameObject spawnedObject = Instantiate(item.gameObject, spawnPoint, Quaternion.identity);

        broker.Register(segment, spawnedObject);

        ItemProperties properties = item.itemProperties;

        Rigidbody rigidBody = spawnedObject.GetComponent<Rigidbody>();
        rigidBody.mass = properties.mass;

        Vector3 side;
        Vector3 up;
        Vector3 towards;

        switch (segment)
        {
            case ScreenSegment.TOP_LEFT:
                side = properties.forceSide * Vector3.left;
                up = properties.forceUpHigh * Vector3.up;
                towards = properties.forceBack * Vector3.back;
                break;
            case ScreenSegment.BOTTOM_LEFT:
                side = properties.forceSide * Vector3.left;
                up = properties.forceUpLow * Vector3.up;
                towards = properties.forceBack * Vector3.back;
                break;
            case ScreenSegment.TOP_RIGHT:
                side = properties.forceSide * Vector3.right;
                up = properties.forceUpHigh * Vector3.up;
                towards = properties.forceBack * Vector3.back;
                break;
            default:
                side = properties.forceSide * Vector3.right;
                up = properties.forceUpLow * Vector3.up;
                towards = properties.forceBack * Vector3.back;
                break;
        }

        rigidBody.AddForce(side + up + towards);
    }

    internal class ImportedItem
    {
        internal readonly GameObject gameObject;
        internal readonly ItemProperties itemProperties;
        internal float probabilityWeight = 0;

        internal ImportedItem(GameObject gameObject)
        {
            this.gameObject = gameObject;
            itemProperties = gameObject.GetComponent<ItemProperties>();
            if (itemProperties.relativeFrequency <= 0) Debug.LogError("Item with a negative relativeFrequency exist. It will break the game logic.");
        }
    }
}
