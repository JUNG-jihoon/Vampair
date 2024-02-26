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
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Levelup);
        AudioManager.instance.EffectBgm(true);
    }

    public void Hide()
    {
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
        AudioManager.instance.PlaySfx(AudioManager.Sfx.Select);
        AudioManager.instance.EffectBgm(false);
    }
    public void Select(int index)
    {
        itmes[index].OnClick();
    }

    void Next()//��� ������ ��Ȱ��ȭ �� �� �� ���� 3�� ������ Ȱ��ȭ
    {
        foreach(Item item in itmes)
        {
            item.gameObject.SetActive(false);
        }
        int[] ran = new int[3];
        while(true)//3���� ������ Ȱ��ȭ
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

            //���� �������̤� ���� �Һ���������� ��ü
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
