using UnityEngine;

public class TipsUI : MonoBehaviour
{
    [SerializeField] private GameObject[] objects = new GameObject[3];

    public static bool exp;
    public static bool coin;
    public static bool cherry;

    private bool[] flag = new bool[3];

    private void Start()
    {
        exp = false;
        coin = false;
        cherry = false;
        for(int i = 0; i<3; i++)
        {
            flag[i] = false;
        }
    }

    private void FixedUpdate()
    {
        if (flag[0] == false && exp == true)
        {
            if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
                PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
            objects[0].SetActive(true);
            Time.timeScale = 0;
            flag[0] = true;
        }
        if (flag[1] == false && coin == true)
        {
            if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
                PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
            objects[1].SetActive(true);
            Time.timeScale = 0;
            flag[1] = true;
        }
        if (flag[2] == false && cherry == true)
        {
            if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
                PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
            objects[2].SetActive(true);
            Time.timeScale = 0;
            flag[2] = true;
        } 
    }

    public void Exp()
    {
        objects[0].SetActive(false);
        Time.timeScale = 1;
        PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
    }

    public void Coin()
    {
        objects[1].SetActive(false);
        Time.timeScale = 1;
        PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
    }

    public void Cherry()
    {
        objects[2].SetActive(false);
        Time.timeScale = 1;
        PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
    }
}
