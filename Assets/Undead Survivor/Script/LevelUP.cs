using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class LevelUP : MonoBehaviour
{

    RectTransform rect;
    Item[] itmes;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
        itmes = GetComponentsInChildren<Item>(true);
    }

    public void show()
    {
        Next();
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }
    public void Select(int index)
    {
        itmes[index].OnClick();
    }

    void Next()//모든 아이템 비활성화 후 그 중 랜덤 3개 아이템 활성화
    {
        foreach(Item item in itmes)
        {
            item.gameObject.SetActive(false);
        }
        int[] ran = new int[3];
        while(true)//3가지 아이템 활성화
        {
            ran[0] = Random.Range(0, itmes.Length);
            ran[1] = Random.Range(0, itmes.Length);
            ran[2] = Random.Range(0, itmes.Length);
            if (ran[0] != ran[1] && ran[1] != ran[2] && ran[0] != ran[2])
                break;
        }

        for( int index=0; index< ran.Length; index++)
        {
            Item ranItem = itmes[ran[index]];

            //만렙 아이템이ㅡ 경우는 소비아이템으로 대체
            if(ranItem.level == ranItem.data.damages.Length)
            {
                itmes[4].gameObject.SetActive(true);
            }
            else
            {
                ranItem.gameObject.SetActive(true);
            }

        }
    }
}
