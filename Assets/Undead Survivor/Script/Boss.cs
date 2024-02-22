using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float speed;
    public float health;
    public float maxHealth;
    public RuntimeAnimatorController animcon;
    public Rigidbody2D target;
    [SerializeField]
    public EnemySetting enemy;

    bool isLive;

    Rigidbody2D rigid;
    Collider2D coll;
    Animator anim;
    SpriteRenderer spriter;
    WaitForFixedUpdate wait;

    // Start is called before the first frame update
    void Awake()
    {
        init(enemy);
        rigid = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriter = GetComponent<SpriteRenderer>();
        wait = new WaitForFixedUpdate();
        isLive = true;
        coll.enabled = true;
        rigid.simulated = true;
        spriter.sortingOrder = 2;
        anim.SetBool("Dead", false);
        health = maxHealth;//�׾��� ����� �� ü�� �ʱ�ȭ
    }

    private void FixedUpdate()
    {
        if (!isLive || anim.GetCurrentAnimatorStateInfo(0).IsName("Hit"))//�׾����� ����
            return;

        Vector2 dirVec = target.position - rigid.position; //�÷��̾���� �Ÿ�
        Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVec);//�÷��̾� ���� �̵�
        rigid.velocity = Vector2.zero;
    }

    private void LateUpdate()//���� ������ ���� ������
    {
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }


    public void init(EnemySetting data)//��Ƴ��� �� �� �ʱ�ȭ
    {
        
            speed = data.speed;
            health = data.Health;
            maxHealth = data.Health;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Bullet") || !isLive)
            return;

        health -= collision.GetComponent<Bullet>().damage;  
        if (health > 0)
        {
            anim.SetTrigger("Hit");
        }
        else
        {
            isLive = false;
            coll.enabled = false;
            rigid.simulated = false;
            spriter.sortingOrder = 1;
            anim.SetBool("Dead", true);
            GameManager.instance.kill++;
            GameManager.instance.GetExp();
        }

/*        IEnumerator KnockBack()
        {
            yield return wait;
            Vector3 playerPos = GameManager.instance.player.transform.position;
            Vector3 dirVec = transform.position - playerPos;
            rigid.AddForce(dirVec.normalized , ForceMode2D.Impulse);
        }*/
    }
    void Dead()
    {
        gameObject.SetActive(false);
    }
}