using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinUI : MonoBehaviour
{
    public Text nowCoinCount;
    public static int currentCoinCount;
    
    private int startCoinCount;

    void Start()                         
    {
        startCoinCount = 0;
        currentCoinCount = startCoinCount;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        nowCoinCount.text = currentCoinCount.ToString();
    }
}
