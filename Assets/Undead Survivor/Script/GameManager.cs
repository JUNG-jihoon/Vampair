using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [Header(" 게임 컨트롤")]
    public bool isLive;
    public float gameTime;
    public float maxGameTime = 2 * 10f;

    [Header("게임 정보")]
    public int health;
    public int maxHealth = 100;
    public int level;
    public int kill;
    public int exp;
    public int[] nextExp = { 3, 5, 60, 100, 150, 210, 280, 360, 450, 600 };

    [Header("게임 오브젝트")]
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

        uiLevelUP.Select(0);
    }
    void Update()//타이머
    {
        if (!isLive)
            return;

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

    public void Stop()
    {
        isLive = false;
        Time.timeScale = 0;
    }
    public void Resume()
    {
        isLive = true;
        Time.timeScale = 1;
    }

}
