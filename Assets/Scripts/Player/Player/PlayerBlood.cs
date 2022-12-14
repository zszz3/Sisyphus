using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerBlood : MonoBehaviour
{
    [SerializeField] private UnityEvent died;   //触发死亡弹窗
    
    public static bool flag;

    public float totalBlood;                                //同步ui
    public Image blood;                                     //同步ui
    public Text bloodText;
    
    private int blinks;                                      //受伤闪烁次数                                  
    private float time;                                      //受伤闪烁时间       
    private Renderer myRenderer;
    private float currentBlood;                                    //人物当前血量

    void Start()
    {
        myRenderer = GetComponent<Renderer>(); 
        currentBlood = totalBlood;
        blinks = 5;
        time = 0.1f;
        flag = false;
    }

    void Update()
    {
        blood.fillAmount = 1.0f / totalBlood * currentBlood;      //同步ui
        bloodText.text = currentBlood + "/" + totalBlood;
    }

    public void DamagePlayer(float damage)
    {
        currentBlood -= damage;
  
        if (currentBlood <= 0)                              //人物死亡，暂停，并调用死亡弹窗
        {
            gameObject.SetActive(false);
            Time.timeScale = 0;
            flag = true;
            bloodText.text = 0 + "/" + totalBlood;
            blood.fillAmount = 0;      //同步ui
            PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
            died.Invoke();
            

        }
        BlinkPlayer(blinks, time);
    }

    public void IncreasePlayer(float blood) //加血的函数
    {
        if (currentBlood + blood > totalBlood)//人物血量大于初始值，则最高为初始值
        {
            currentBlood = totalBlood;
        }
        else
        {
            currentBlood += blood; //否则增加固定的血量
        }
    }

    void BlinkPlayer(int numBlinks , float seconds)
    {
        StartCoroutine(DoBlinks(numBlinks, seconds));
    }

    IEnumerator DoBlinks(int numBlinks, float seconds)
    {
        for(int i = 0; i < numBlinks * 2; i++)
        {
            myRenderer.enabled = !myRenderer.enabled;
            yield return new WaitForSeconds(seconds);
        }
        myRenderer.enabled = true;
    }
}
 