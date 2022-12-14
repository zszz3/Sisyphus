using UnityEngine;

public class EnemyCreator : MonoBehaviour
{
    
    private bool[] flag = new bool[18];
    [SerializeField] private float maxCircle;                                            //大圆内生成怪物
    [SerializeField] private float minCircle;                                            //小圆内不生怪
    [SerializeField] private float[] cdTime;                                             //生怪频率

    [SerializeField] private GameObject timer;                                           //读取时间
    [SerializeField] private GameObject player;                                          //以玩家为圆心

    [SerializeField] private GameObject[] enemyLevel_1;
    [SerializeField] private GameObject[] enemyLevel_2;
    [SerializeField] private GameObject[] enemyLevel_3;

    private int TheNumberOfMonster = 0;//怪物的数量
    [Header("这里设置每一波怪的最少数量")]
    public int TheNumberOfMonsterIn0_60;
    public int TheNumberOfMonsterIn60_120;
    public int TheNumberOfMonsterIn120_180;
    public int TheNumberOfMonsterIn180_240;
    public int TheNumberOfMonsterIn240_295;
    public int TheNumberOfMonsterIn300_360;
    public int TheNumberOfMonsterIn360_420;
    public int TheNumberOfMonsterIn420_480;
    public int TheNumberOfMonsterIn480_540;
    public int TheNumberOfMonsterIn540_595;
    public int TheNumberOfMonsterIn600_660;
    public int TheNumberOfMonsterIn660_720;
    public int TheNumberOfMonsterIn720_780;
    public int TheNumberOfMonsterIn780_840;
    public int TheNumberOfMonsterIn840_895;
    public float Maxrate;

    private void Start()
    {
        for(int i = 0; i < 18; i++)
        {
            flag[i] = false;
        }
    }

    private void FixedUpdate()
    {
        GameObject[] monster = GameObject.FindGameObjectsWithTag("Enemy");
        TheNumberOfMonster = monster.Length;
        int a = timer.GetComponent<CountdownTimer>().second;
        if (a < 5)
        {
            this.CancelInvoke();
        }
        else if (a>=5 &&a < 60)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn0_60 * Maxrate &&flag[0]==true)
            {
                this.CancelInvoke();
                flag[0] = false;
            }
            else if(TheNumberOfMonster < TheNumberOfMonsterIn0_60 * Maxrate &&flag[0]==false)
            {
                InvokeRepeating("Create_1", 0, cdTime[0]);
                flag[0] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn0_60)
            {
                Create_1();
                TheNumberOfMonster++;
            }
        }

        else if (a < 120)
        {
            //GameObject[] monster = GameObject.FindGameObjectsWithTag("Enemy");
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn60_120 * Maxrate && flag[1] == true)
            {
                this.CancelInvoke();
                flag[1] = false;
            }
            else if(TheNumberOfMonster < TheNumberOfMonsterIn60_120 * Maxrate && flag[1] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_1", 0, cdTime[1]);
                flag[1] = false;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn60_120)
            {
                Create_1();
                TheNumberOfMonster++;
            }
        }

        else if (a < 180)
        {
            //GameObject[] monster = GameObject.FindGameObjectsWithTag("Enemy");
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn120_180 * Maxrate && flag[2] == true)
            {
                this.CancelInvoke();
                flag[2] = false;
            }
            else if(TheNumberOfMonster < TheNumberOfMonsterIn120_180 * Maxrate && flag[2] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_1", 0, cdTime[2]);
                flag[2] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn120_180)
            {
                Create_1();
                TheNumberOfMonster++;
            }
        }

        else if (a < 240)
        {
            //GameObject[] monster = GameObject.FindGameObjectsWithTag("Enemy");
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn180_240 * Maxrate && flag[3] == true)
            {
                this.CancelInvoke();
                flag[3] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn180_240 * Maxrate && flag[3] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_1", 0, cdTime[3]);
                flag[3] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn180_240)
            {
                Create_1();
                TheNumberOfMonster++;
            }
        }

        else if (a < 295)
        {
            //GameObject[] monster = GameObject.FindGameObjectsWithTag("Enemy");
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn240_295 * Maxrate && flag[4] == true)
            {
                this.CancelInvoke();
                flag[4] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn240_295 * Maxrate && flag[4] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_1", 0, cdTime[4]);
                flag[4] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn240_295)
            {
                Create_1();
                TheNumberOfMonster++;
            }
        }

        else if (a < 300)
        {
            this.CancelInvoke();
        }

        else if (a < 360)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn300_360 * Maxrate && flag[6] == true)
            {
                this.CancelInvoke();
                flag[6] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn300_360 * Maxrate && flag[6] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_2", 0, cdTime[0]);
                flag[6] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn300_360)
            {
                Create_2();
                TheNumberOfMonster++;
            }
        }

        else if (a < 420)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn360_420 * Maxrate && flag[7] == true)
            {
                this.CancelInvoke();
                flag[7] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn360_420 * Maxrate && flag[7] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_2", 0, cdTime[1]);
                flag[7] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn360_420)
            {
                Create_2();
                TheNumberOfMonster++;
            }
        }

        else if (a < 480)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn420_480 * Maxrate && flag[8] == true)
            {
                this.CancelInvoke();
                flag[8] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn420_480 * Maxrate && flag[8] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_2", 0, cdTime[2]);
                flag[8] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn420_480)
            {
                Create_2();
                TheNumberOfMonster++;
            }
        }

        else if (a < 540)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn480_540 * Maxrate && flag[9] == true)
            {
                this.CancelInvoke();
                flag[9] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn480_540 * Maxrate && flag[9] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_2", 0, cdTime[3]);
                flag[9] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn480_540)
            {
                Create_2();
                TheNumberOfMonster++;
            }
        }

        else if (a < 595)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn540_595 * Maxrate && flag[10] == true)
            {
                this.CancelInvoke();
                flag[10] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn540_595 * Maxrate && flag[10] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_2", 0, cdTime[4]);
                flag[10] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn540_595)
            {
                Create_2();
                TheNumberOfMonster++;
            }
        }

        else if (a < 600)
        {
            this.CancelInvoke();
        }

        else if (a < 660)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn600_660 * Maxrate && flag[12] == true)
            {
                this.CancelInvoke();
                flag[12] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn600_660 * Maxrate && flag[12] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_3", 0, cdTime[0]);
                flag[12] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn600_660)
            {
                Create_3();
                TheNumberOfMonster++;
            }
        }

        else if (a < 720)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn660_720 * Maxrate && flag[13] == true)
            {
                this.CancelInvoke();
                flag[13] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn660_720 * Maxrate && flag[13] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_3", 0, cdTime[1]);
                flag[13] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn660_720)
            {
                Create_3();
                TheNumberOfMonster++;
            }
        }

        else if (a < 780)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn720_780 * Maxrate && flag[14] == true)
            {
                this.CancelInvoke();
                flag[14] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn720_780 * Maxrate && flag[14] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_3", 0, cdTime[2]);
                flag[14] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn720_780)
            {
                Create_3();
                TheNumberOfMonster++;
            }
        }

        else if (a < 840)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn780_840 * Maxrate && flag[15] == true)
            {
                this.CancelInvoke();
                flag[15] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn780_840 * Maxrate && flag[15] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_3", 0, cdTime[3]);
                flag[15] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn780_840)
            {
                Create_3();
                TheNumberOfMonster++;
            }
        }

        else if (a < 895)
        {
            TheNumberOfMonster = monster.Length;
            if (TheNumberOfMonster > TheNumberOfMonsterIn840_895 * Maxrate && flag[16] == true)
            {
                this.CancelInvoke();
                flag[16] = false;
            }
            else if (TheNumberOfMonster < TheNumberOfMonsterIn840_895 * Maxrate && flag[16] == false)
            {
                this.CancelInvoke();
                InvokeRepeating("Create_3", 0, cdTime[4]);
                flag[16] = true;
            }
            while (TheNumberOfMonster < TheNumberOfMonsterIn840_895)
            {
                Create_3();
                TheNumberOfMonster++;
            }
        }

        else if (a < 900)
        {
            this.CancelInvoke();
        }
    }

    private Vector2 RandomEnemyPosition()
    {
        Vector2 randomPosition = player.transform.position;
        Vector2 vector2;
        while (true)
        {
            vector2 = Random.insideUnitCircle * maxCircle;
            float dis = (vector2 - new Vector2(0, 0)).sqrMagnitude;
            if (dis >= minCircle)
            {
                break;
            }
        }
        randomPosition += vector2;
        return randomPosition;
    }

    void Create_1()
    {
        Vector2 vector2 = RandomEnemyPosition();
        int a = Random.Range(0, enemyLevel_1.Length);
        GameObject nemey = enemyLevel_1[a];
        if (Mathf.Abs(vector2.x - player.transform.position.x) <= 3 &&Mathf.Abs(vector2.y - player.transform.position.y) <= 3)
        {
            float flag1 = 4;
            float flag2 = 4;
            if (vector2.x - player.transform.position.x < 0) flag1 = -4;
            if (vector2.y - player.transform.position.y < 0) flag2 = -4;
            vector2.x += flag1;
            vector2.y += flag2;
        }
        Instantiate(nemey, vector2, player.transform.rotation);
    }

    void Create_2()
    {
        Vector2 vector2 = RandomEnemyPosition();
        int a = Random.Range(0, enemyLevel_2.Length);
        GameObject nemey = enemyLevel_2[a];
        if (Mathf.Abs(vector2.x - player.transform.position.x) <= 3 && Mathf.Abs(vector2.y - player.transform.position.y) <= 3)
        {
            float flag1 = 4;
            float flag2 = 4;
            if (vector2.x - player.transform.position.x < 0) flag1 = -4;
            if (vector2.y - player.transform.position.y < 0) flag2 = -4;
            vector2.x += flag1;
            vector2.y += flag2;
        }
        Instantiate(nemey, vector2, player.transform.rotation);
    }

    void Create_3()
    {
        Vector2 vector2 = RandomEnemyPosition();
        int a = Random.Range(0, enemyLevel_3.Length);
        GameObject nemey = enemyLevel_3[a];
        if (Mathf.Abs(vector2.x - player.transform.position.x) <= 3 && Mathf.Abs(vector2.y - player.transform.position.y) <= 3)
        {
            float flag1 = 4;
            float flag2 = 4;
            if (vector2.x - player.transform.position.x < 0) flag1 = -4;
            if (vector2.y - player.transform.position.y < 0) flag2 = -4;
            vector2.x += flag1;
            vector2.y += flag2;
        }
        Instantiate(nemey, vector2, player.transform.rotation);
    }
}
