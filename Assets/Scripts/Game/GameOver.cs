using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    
    public void Restart()                   //返回场景0，并暂停游戏，不赞同玩家死亡怪物会继续行动。
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
