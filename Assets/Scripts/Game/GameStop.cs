using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStop : MonoBehaviour
{
    public static bool GameIsPasued = false;
    public GameObject pasueMenuUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && LevelUP.flag == false && PlayerBlood.flag == false)                 //�������esc����ͣ���Ѿ���ͣ�ͼ�����Ϸ��
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
        pasueMenuUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        GameIsPasued = false;
    }

    public void Pause()
    {
        pasueMenuUI.SetActive(true);             //ui��ʾ
        if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
            PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
        Time.timeScale = 0;
        GameIsPasued = true;
    }

    public void MainMenu()                      //�������˵�
    {
        PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
        GameIsPasued = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

}
