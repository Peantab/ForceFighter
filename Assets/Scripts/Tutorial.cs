using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject GameTitle;
    public GameObject MainMenu;
    public GameObject Tutorial1;
    public GameObject Tutorial2;
    public GameObject[] RotatingObjects;

    private bool rotateObjects = false;

    public void ShowTutorial1()
    {
        MainMenu.SetActive(false);
        GameTitle.SetActive(false);
        Tutorial1.SetActive(true);
    }

    public void ShowTutorial2()
    {
        Tutorial1.SetActive(false);
        Tutorial2.SetActive(true);
        rotateObjects = true;
    }

    public void EndTutorial()
    {
        rotateObjects = false;
        Tutorial2.SetActive(false);
        MainMenu.SetActive(true);
        GameTitle.SetActive(true);
    }

    private void FixedUpdate()
    {
        if (rotateObjects)
        {
            foreach (GameObject rotable in RotatingObjects)
            {
                rotable.transform.Rotate(0, 1, 0);
            }
        }
    }
}
