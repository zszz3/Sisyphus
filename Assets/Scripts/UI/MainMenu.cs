using UnityEngine;
using UnityEngine.SceneManagement;       //����SceneManager��Ҫ���

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);       //���볡��1��������project setting�����ã�Ҳ�����á��������ơ������ɶ��Ը��ߡ�
    }

    public void QuitGame()
    {
        Application.Quit();              //Ӧ�ý���
    }
}
