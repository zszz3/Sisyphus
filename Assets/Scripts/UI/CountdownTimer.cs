using UnityEngine;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public static CountdownTimer Instance { get; private set; }
    public int second;

    [SerializeField] private GameObject[] tilemap = new GameObject[3];
    [SerializeField] private UnityEngine.UI.Text txtimer;
    [SerializeField] private GameObject CircleExp;
    [SerializeField] private GameObject gamepass;

    private float nextime = 1;  //下次修改的时间

    public void Start()
    {
        Instance = this;
        second = 0;
    }

    private void FixedUpdate()
    {   //如果到了修改时间  (用来修改原来的每帧改变，换成每秒改变)
        if (Time.time >= nextime)
        {
            second++;
            txtimer.text = string.Format("{0:d2}"+"s", second);
            nextime = Time.time + 1;

            if (second == 240)
            {
                GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemy.Length; i++)
                {
                    Destroy(allEnemy[i]);
                }
            }
            if (second == 295)
            {
                GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemy.Length; i++)
                {
                    Destroy(allEnemy[i]);
                }
                CircleExp.GetComponent<CircleExp>().MaxCircleExp();
            }
            if (second == 300)
            {
                tilemap[0].SetActive(false);
                tilemap[1].SetActive(true);
            }

            if (second == 540)
            {
                GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemy.Length; i++)
                {
                    Destroy(allEnemy[i]);
                }
            }
            if (second == 595)
            {
                GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemy.Length; i++)
                {
                    Destroy(allEnemy[i]);
                }
                CircleExp.GetComponent<CircleExp>().MaxCircleExp();
            }
            if (second == 600)
            {
                tilemap[1].SetActive(false);
                tilemap[2].SetActive(true);
            }

            if (second == 840)
            {
                GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemy.Length; i++)
                {
                    Destroy(allEnemy[i]);
                }
            }
            if (second == 895)
            {
                GameObject[] allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
                for (int i = 0; i < allEnemy.Length; i++)
                {
                    Destroy(allEnemy[i]);
                }
                CircleExp.GetComponent<CircleExp>().MaxCircleExp();
            }
            if (second == 900)
            {
                PlayerBlood.flag = true;
                gamepass.SetActive(true);
                if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
                    PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
                Time.timeScale = 0;
            }
        }
    }

    public void Restart()                 
    {
        gamepass.SetActive(false);
        PlayerBlood.flag = false;
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}