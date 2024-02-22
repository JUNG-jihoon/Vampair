using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;

    private List<GameObject>[] pools;


    void Awake()//풀 생성 및 프리팹 저장
    {
        pools = new List<GameObject>[Prefabs.Length];


        for(int i =0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        }
    }

    public GameObject Get(int index)
    {
        GameObject select = null;


        foreach(GameObject item in pools[index])//프리팹 활성화
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(!select)//남아있는 프리팹 없다면 생성
        {
            select = Instantiate(Prefabs[index], transform);
            pools[index].Add(select);
            Debug.Log(GetComponentsInChildren<Transform>().Length);
        }


        return select;
    }

}
