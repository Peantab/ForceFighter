using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public GameObject BloodSplash;

    private Text textbox;
    private int health;
    private int splashCounter;
    private const int splashCounterMax = 15;

    private Image bloodSplashImage;

    // Start is called before the first frame update
    void Start()
    {
        textbox = gameObject.GetComponent<Text>();
        health = 100;

        bloodSplashImage = BloodSplash.GetComponent<Image>();
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

    private void FixedUpdate()
    {
        if (splashCounter > 0)
        {
            splashCounter--;
            bloodSplashImage.color = new Color(bloodSplashImage.color.r, bloodSplashImage.color.g, bloodSplashImage.color.b, 1.0f * splashCounter/splashCounterMax);
            if (splashCounter == 0) bloodSplashImage.enabled = false;
            else bloodSplashImage.enabled = true;
        }
    }

    public void AcceptDamage(int damage)
    {
        if (damage > 0)
        {
            health -= damage;
            splashCounter = splashCounterMax;
        }
    }

    public bool IsAlive()
    {
        return health > 0;
    }
}
