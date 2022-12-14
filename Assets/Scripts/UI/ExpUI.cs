using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    float[]exp_array = new float[] {1,2,3,4,6,8,10,12,15,20,30,40,50,60,70,90,110,120,130,140,150,175,200,230,245,250,260,280,300,320,335,350,360,380,400,410,430,450,470,480,500,600,600,600,600,600,600,600,1000};
    public float totalExp;                 //×Ü¾­Ñé
    public float startExp;
    public static float currentExp;
    public Image exp;
    public Text expText;

    void Start()
    {
        currentExp = startExp;
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        exp.fillAmount = 1.0f * currentExp / totalExp;
        expText.text = currentExp.ToString() + "/" + totalExp;
        if (LevelUI.currentLevel == 51) return;
        if (currentExp >= totalExp)
        {
            currentExp = currentExp - totalExp;
            totalExp = exp_array[LevelUI.currentLevel];
            LevelUI.currentLevel += 1;
        }
    }
}
