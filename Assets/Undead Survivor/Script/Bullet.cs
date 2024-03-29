using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage;
    public int per;

    Rigidbody2D rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public void init(float damage, int per, Vector3 dir)
    {
       this.damage = damage;
       this.per = per;
       
        if( per > -1)
        {
            rigid.velocity = dir * 15f;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy")|| per == -1)
            return;
        per--;
        if(per == -1)
        {
            rigid.velocity = Vector3.zero;
            gameObject.SetActive(false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Area"))
            gameObject.SetActive(false);

    }
}
