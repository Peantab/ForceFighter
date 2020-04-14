using System.Collections;
using System.Collections.Generic;
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
        textbox.transform.position = new Vector3(Screen.width - 90, Screen.height - 30);
        health = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if(health > 0)
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
