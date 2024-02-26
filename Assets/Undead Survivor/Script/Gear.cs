using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour
{
    public ItemData.ItemType type;
    public float rate;

    public void Init(ItemData data)
    {
        name = "Gear "+data.itemId;
        transform.parent = GameManager.instance.player.transform;
        transform.localScale = Vector3.zero;

        type = data.itemType;
        rate = data.damages[0];
        ApplyGear();
    }

    public void LevelUP(float rate)
    {
        this.rate = rate;
        ApplyGear();
    }

    void ApplyGear()
    {
        switch (type)
        {
            case ItemData.ItemType.Glove:
                RateUP(); 
                break;
            case ItemData.ItemType.Shoe:
                Speedup(); 
                break;
        }
    }


    void RateUP()
    {
        Weapon[] wapons = transform.parent.GetComponentsInChildren<Weapon>();
        foreach (Weapon weapon in wapons)
        {
            switch (weapon.id)
            {
                case 0:
                    float speed = 150 * Character.WeaponSpeed;
                    weapon.speed = 150 + (rate * 150); 
                    break;
                default:
                    speed = 0.5f * Character.WeaponRate;
                    weapon.speed = 0.5f * (1f - rate); 
                    break;
            }
        }
    }
    void Speedup()
    {
        float speed = 3 * Character.Speed;
        GameManager.instance.player.speed = speed + speed * rate;
    }

}
