using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header(" ���� ��Ʈ��")]
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("���� ����")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("���� ������Ʈ")]
    public PoolManager pool;
    public Player player;
    public LevelUP uiLevelUP;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        health = maxHealth;
    }
    void Update()//Ÿ�̸�
    {
        gameTime += Time.deltaTime;

        if (gameTime > maxGameTime)
        {
            gameTime = maxGameTime;
        }

    }

    public void GetExp()
    {
        exp++;
        if(exp == nextExp[level])
        {
            level++;
            exp = 0;
            uiLevelUP.show();
        }
    }

}
