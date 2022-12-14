using System.Security.Cryptography;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using System;


public class LevelUP : MonoBehaviour
{
    [SerializeField] private const int Maxnum =9;

    [SerializeField] private GameObject LevelUPUI;
    [SerializeField] private Image[] images = new Image[3];
    [SerializeField] private Text[] texts = new Text[3];
    [SerializeField] private Button[] buttons = new Button[3];
    [SerializeField] private Sprite[] weaponImage = new Sprite[Maxnum];
    [SerializeField] private Text[] weaponTexts = new Text[Maxnum];
    public static bool flag;
    private bool [] vis = new bool [Maxnum];
    private string[] weapon = { "Sickle", "Bomb", "Missile", "Circle","Laser","Rushshoot","CircleExp","DamagePotion","Freeze"};
    private int level;

    void Start()
    {
        flag = false;
        level = 0;
        for (int i = 0; i < Maxnum; i++) vis[i] = false;
    }

    void Update()
    {
        if (LevelUI.currentLevel > level)
        {
            level += 1;
            Pause();
        }
    }

    public void Pause()
    {
        if (Time.time >= PlayerController.lastDash + PlayerController._dashCoolDown)
            PlayerController.lastDash = Time.time - PlayerController._dashCoolDown + 0.1f;
        CheckLevel();
        RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
        weapon = weapon.OrderBy(x => Next(random)).ToArray();
        for (int i = 0; i < 3; i++)
        {
            UISet(weapon[i], i);
        }
        flag = true;
        LevelUPUI.SetActive(true);
        buttons[0].GetComponent<UnityEngine.UI.Button>().Select();
        Time.timeScale = 0;
    }

    void UISet(string weaponName, int num)
    {
        if (weaponName == "Sickle")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(SickleUp);
            texts[num].text = "回旋镖等级:" + SickleAttack.level.ToString()+"\n角色左右两边各发出一个回旋镖";
            images[num].sprite = weaponImage[0];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (weaponName == "Bomb")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(BombUP);
            texts[num].text = "炸弹等级:" + BombCreator.level.ToString()+"\n在角色脚下放一个炸弹\n对炸弹周围敌人造成范围伤害";
            images[num].sprite = weaponImage[1];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if (weaponName == "Missile")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(MissileUP);
            texts[num].text = "闪电等级:" + MissileCreator.level.ToString() + "\n角色发射一个跟踪敌人的闪电";
            images[num].sprite = weaponImage[2];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(3, 1, 1);
        }
        else if (weaponName == "Circle")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(CircleUP);
            texts[num].text = "魔法圆环等级:" + CircleCreator.level.ToString()+ "\n角色周围生成一个魔法圆环\n圆环中间的敌人受到伤害";
            images[num].sprite = weaponImage[3];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        else if(weaponName== "Laser")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(LaserUP);
            texts[num].text = "剑气等级:" + LaserLauch.level.ToString()+"\n在角色四角方向发射一个剑气";
            images[num].sprite = weaponImage[4];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        if (weaponName == "Rushshoot")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(RushshootUP);
            texts[num].text = "急行之靴等级:" + Rushshoot.level.ToString() + "\n角色移动速度增加";
            images[num].sprite = weaponImage[5];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        if (weaponName == "CircleExp")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(CircleExpUP);
            texts[num].text = "经验之环等级:" + CircleExp.level.ToString() + "\n经验吸收范围增加，经验加成增加";
            images[num].sprite = weaponImage[6];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        if (weaponName == "DamagePotion")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(DamagePotionUP);
            texts[num].text = "力量药水等级:" + DamagePotions.level.ToString() + "\n所有武器伤害增加10%";
            images[num].sprite = weaponImage[7];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
        if (weaponName == "Freeze")
        {
            buttons[num].onClick.RemoveAllListeners();
            buttons[num].onClick.AddListener(FreezeUP);
            texts[num].text = "冰脉光束等级:" + FreezeCreator.level.ToString() + "\n角色朝向释放可以使所有敌人冻结的光束";
            images[num].sprite = weaponImage[8];
            images[num].GetComponent<RectTransform>().sizeDelta = new Vector2(25f, 25f);
            images[num].GetComponent<Transform>().localScale = new Vector3(1, 1, 1);
        }
    }

    void CheckLevel()
    {
        if (SickleAttack.level == 9)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Sickle");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (BombCreator.level == 9)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Bomb");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (MissileCreator.level == 9)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Missile");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (CircleCreator.level == 9)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Circle");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (LaserLauch.level == 9)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Laser");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (CircleExp.level == 6)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("CircleExp");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (Rushshoot.level == 6)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Rushshoot");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (DamagePotions.level == 6)
        {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("DamagePotions");
            weapon = (string[])a1.ToArray(typeof(string));
        }
        if (FreezeCreator.level == 9) {
            ArrayList a1 = new ArrayList(weapon);
            a1.Remove("Freeze");
            weapon = (string[])a1.ToArray(typeof(string));
        }
    }

    public void SickleUp()
    {
        if (SickleAttack.level <= 8)
        {
            SickleAttack.level += 1;
        }
        weaponTexts[0].text = SickleAttack.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;

        flag = false;
    }

    public void BombUP()
    {
        if (BombCreator.level <= 8)
        {
            BombCreator.level += 1;
        }
        weaponTexts[1].text = BombCreator.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void MissileUP()
    {
        if (MissileCreator.level <= 8)
        {
            MissileCreator.level += 1;
        }
        weaponTexts[2].text = MissileCreator.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void CircleUP()
    {
        if (CircleCreator.level <= 8)
        {
            CircleCreator.level += 1;
        }
        weaponTexts[3].text = CircleCreator.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void LaserUP()
    {
        if (LaserLauch.level <= 8)
        {
            LaserLauch.level += 1;
        }
        weaponTexts[4].text = LaserLauch.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void RushshootUP()
    {
        if (Rushshoot.level <= 5)
        {
            Rushshoot.level += 1;
        }
        weaponTexts[5].text = Rushshoot.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void FreezeUP()
    {
        if (FreezeCreator.level <= 8)
        {
            FreezeCreator.level += 1;
        }
        weaponTexts[8].text = FreezeCreator.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void CircleExpUP()
    {
        if (CircleExp.level <= 5)
        {
            CircleExp.level += 1;
        }
        weaponTexts[6].text = CircleExp.level.ToString();
        LevelUPUI.SetActive(false);            //ui����ʾ
        Time.timeScale = 1;
        flag = false;
    }

    public void DamagePotionUP()
    {
        if (DamagePotions.level <= 5)
        {
            DamagePotions.level += 1;
            DamagePotions.Instance.levelUP();
        }
        weaponTexts[7].text = DamagePotions.level.ToString();
        LevelUPUI.SetActive(false);            
        Time.timeScale = 1;
        flag = false;
    }

    static int Next(RNGCryptoServiceProvider random)
    {
        byte[] randomInt = new byte[6];
        random.GetBytes(randomInt);
        return Convert.ToInt32(randomInt[0]);
    }
}
