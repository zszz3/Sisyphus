using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PlayerBlood : MonoBehaviour
{
    [SerializeField] private UnityEvent died;   //������������
    
    public static bool flag;

    public float totalBlood;                                //ͬ��ui
    public Image blood;                                     //ͬ��ui
    public Text bloodText;
    
    private int blinks;                                      //������˸����                                  
    private float time;                                      //������˸ʱ��       
    private Renderer myRenderer;
    private float currentBlood;                                    //���ﵱǰѪ��

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
        blood.fillAmount = 1.0f / totalBlood * currentBlood;      //ͬ��ui
        bloodText.text = currentBlood + "/" + totalBlood;
    }

    public void DamagePlayer(float damage)
    {
        currentBlood -= damage;
  
        if (currentBlood <= 0)                              //������������ͣ����������������
        {
            gameObject.SetActive(false);
            Time.timeScale = 0;
            flag = true;
            bloodText.text = 0 + "/" + totalBlood;
            blood.fillAmount = 0;      //ͬ��ui
            PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
            died.Invoke();
            

        }
        BlinkPlayer(blinks, time);
    }

    public void IncreasePlayer(float blood) //��Ѫ�ĺ���
    {
        if (currentBlood + blood > totalBlood)//����Ѫ�����ڳ�ʼֵ�������Ϊ��ʼֵ
        {
            currentBlood = totalBlood;
        }
        else
        {
            currentBlood += blood; //�������ӹ̶���Ѫ��
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
 