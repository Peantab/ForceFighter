using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public GameObject health;

    private Text textbox;
    private int score;
    private int fixedUpdateModuloCounter = 0;
    private HealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        textbox = gameObject.GetComponent<Text>();
        textbox.transform.position = new Vector3(90, Screen.height - 30);
        healthController = health.GetComponent<HealthController>();
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthController.IsAlive())
        {
            textbox.color = Color.white;
            textbox.text = "SCORE: " + score;
        }
        else
        {
            textbox.color = Color.green;
            textbox.text = "SCORE: " + score;
        }

    }

    private void FixedUpdate()
    {
        if (healthController.IsAlive())
        {
            fixedUpdateModuloCounter = (fixedUpdateModuloCounter + 1) % 50;
            if (fixedUpdateModuloCounter == 0)
            {
                score += 1;
            }
        }
    }
}
