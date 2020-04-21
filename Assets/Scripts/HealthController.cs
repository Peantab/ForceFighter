using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    private Text textbox;
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        textbox = gameObject.GetComponent<Text>();
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        textbox.transform.position = new Vector3(Screen.width - 120, Screen.height - 50);

        if (health > 0)
        {
            textbox.color = Color.white;
            textbox.text = "HEALTH: " + health;
        }
        else
        {
            textbox.color = Color.red;
            textbox.text = "Dead thou are";
        }
    }

    public void AcceptDamage(int damage)
    {
        if (damage > 0) health -= damage;
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
