using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStop : MonoBehaviour
{
    public static bool GameIsPasued = false;
    public GameObject pasueMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LevelUP.flag == false && PlayerBlood.flag == false)                 //如果按下esc，暂停，已经暂停就继续游戏。
        {
            if (GameIsPasued)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pasueMenuUI.SetActive(false);            //ui不显示
        Time.timeScale = 1;
        GameIsPasued = false;
    }

    public void Pause()
    {
        pasueMenuUI.SetActive(true);             //ui显示
        if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
            PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
        Time.timeScale = 0;
        GameIsPasued = true;
    }

    public void MainMenu()                      //返回主菜单
    {
        PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
        GameIsPasued = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

}
