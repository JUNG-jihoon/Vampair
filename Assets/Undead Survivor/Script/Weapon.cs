using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http.Headers;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Weapon : MonoBehaviour
{
    public int id;
    public int prefabId;
    public float damage;
    public int count;
    public float speed;
    float timer;
    Player player;
    private void Awake()
    {
        player = GameManager.instance.player;
    }
    private void Update()
    {
        if (!GameManager.instance.isLive)
            return;
        switch (id)
        {
            case 0:
                transform.Rotate(Vector3.back * speed * Time.deltaTime);
                break;
            default:
                timer += Time.deltaTime;

                if(timer> speed)
                {
                    timer = 0f;
                    Fire();
                }
                break;
        }

    }

    public void Init(ItemData data)//배치 시키기
    {
        name = "Weapon" + data.itemId;
        transform.parent = player.transform;
        transform.localPosition = Vector3.zero;

        id = data.itemId;
        damage = data.baseDamaged * Character.Damage;
        count = data.baseCount + Character.Count;

        for(int i = 0; i < GameManager.instance.pool.Prefabs.Length; i++)
        {
            if(data.projectile == GameManager.instance.pool.Prefabs[i])
            {
                prefabId = i;
                break;
            }
        }

        switch (id)
        {
            case 0:
                speed = 150 * Character.WeaponSpeed;
                Batch();
                break;
            default:
                speed = 0.4f* Character.WeaponRate;
                break;
        }

        Hand hand = player.hands[(int)data.itemType];
        hand.spriter.sprite = data.hand;
        hand.gameObject.SetActive(true);


        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }

    public void Levelup(float damage, int count)//레벨 업시 무기 갯수 및 데미지 증가
    {
        this.damage += damage * Character.Damage;
        this.count += count;

        if (id == 0)
            Batch();
        player.BroadcastMessage("ApplyGear", SendMessageOptions.DontRequireReceiver);
    }


    void Batch()//근접공격 회전 및 배치 
    {
        for(int index = 0; index < count; index++)
        {
            UnityEngine.Transform bullet;
            if(index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GameManager.instance.pool.Get(prefabId).transform;
                bullet.parent = transform;
            }

            bullet.localPosition = Vector3.zero;
            bullet.localRotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * 360 * index / count;
            bullet.Rotate(rotVec);
            bullet.Translate(bullet.up * 1.5f, Space.World);
            bullet.GetComponent<Bullet>().init(damage, -1, Vector3.zero);// -1은 관통되는 근접공격
        }
    }

    void Fire()//원거리 무기 발사
    {
        if (!player.scanner.nearestTarget)
            return;

        Vector3 targetpos = player.scanner.nearestTarget.position;
        Vector3 dir = targetpos - transform.position;
        dir = dir.normalized;


        UnityEngine.Transform bullet = GameManager.instance.pool.Get(prefabId).transform;
        bullet.position = transform.position;
        bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        bullet.GetComponent<Bullet>().init(damage, count, dir);

        AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
    }
        
}
