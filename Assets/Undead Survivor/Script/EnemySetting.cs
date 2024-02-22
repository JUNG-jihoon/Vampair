using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Enemy Setting", menuName = "EnemySetting")]
public class EnemySetting : ScriptableObject
{
    public int sprtieType;
    public float spawnTime;
    public int Health;
    public float speed;

}
