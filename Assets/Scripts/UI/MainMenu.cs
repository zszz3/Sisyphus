using UnityEngine;
using UnityEngine.SceneManagement;       //调用SceneManager需要这个

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);       //进入场景1，场景在project setting里设置，也可以用“场景名称”来，可读性更高。
    }

    public void QuitGame()
    {
        Application.Quit();              //应用结束
    }
}
