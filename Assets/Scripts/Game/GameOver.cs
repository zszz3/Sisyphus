using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void Restart()                   //���س���0������ͣ��Ϸ������ͬ����������������ж���
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
