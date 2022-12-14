using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUI : MonoBehaviour
{
    public static int currentLevel;
    
    public Text nowCoinCount;
    
    private int startLevel;

    void Start()
    {
        startLevel = 0;
        currentLevel = startLevel;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        nowCoinCount.text = "Level:"+currentLevel.ToString();
    }
}
