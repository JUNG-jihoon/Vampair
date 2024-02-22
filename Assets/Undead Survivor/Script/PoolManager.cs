using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] Prefabs;

    private List<GameObject>[] pools;


    void Awake()//Ǯ ���� �� ������ ����
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


        foreach(GameObject item in pools[index])//������ Ȱ��ȭ
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if(!select)//�����ִ� ������ ���ٸ� ����
        {
            select = Instantiate(Prefabs[index], transform);
            pools[index].Add(select);
            Debug.Log(GetComponentsInChildren<Transform>().Length);
        }


        return select;
    }

}
