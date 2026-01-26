using UnityEngine;

public class CarSelectManager : MonoBehaviour
{
    public GameObject Body1;
    public GameObject Body2;
    public GameObject Body3;
    public GameObject Body4;
    public GameObject Body5;
    public GameObject CarMenu;

    private void Start()
    {
        Body1.SetActive(true);
        Body2.SetActive(false);
        Body3.SetActive(false);
        Body4.SetActive(false);
        Body5.SetActive(false);
        CarMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void BodyOne()
    {
        Body1.SetActive(true);
        Body2.SetActive(false);
        Body3.SetActive(false);
        Body4.SetActive(false);
        Body5.SetActive(false);
    }
    public void BodyTwo()
    {
        Body1.SetActive(false);
        Body2.SetActive(true);
        Body3.SetActive(false);
        Body4.SetActive(false);
        Body5.SetActive(false);
    }
    public void BodyThree()
    {
        Body1.SetActive(false);
        Body2.SetActive(false);
        Body3.SetActive(true);
        Body4.SetActive(false);
        Body5.SetActive(false);
    }
    public void BodyFour()
    {
        Body1.SetActive(false);
        Body2.SetActive(false);
        Body3.SetActive(false);
        Body4.SetActive(true);
        Body5.SetActive(false);
    }
    public void BodyFive()
    {
        Body1.SetActive(false);
        Body2.SetActive(false);
        Body3.SetActive(false);
        Body4.SetActive(false);
        Body5.SetActive(true);
    }

    public void startRace()
    {
        CarMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
