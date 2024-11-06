using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBossState : MonoBehaviour
{   

    // ���� ���¿� ���߻��� 2���� ���󿡼� 2����+ �齺��  ���߿��� 2���� �������Ͽ��� 3������ �ٸ��������� ��ȯ
    // �����϶� �Ÿ�üũ�� �����϶� �Ÿ�üũ �� �̵������ �ٲ��� �� 
    enum BossState
    {
        Idle, Move, Attack, Die
    }
    // ���� ���� ������Ʈ
    [SerializeField] GameObject bossObject;
    [SerializeField] Transform bossTransform; // ������ �̵��� ȸ���� ������ �� Ʈ������ 
    [SerializeField] Rigidbody2D bossRigid;


    // ���� �ִϸ��̼�
    [SerializeField] Animator animator;
    // �÷��̾� ������
    [SerializeField] GameObject player;
    // ��ų ����Ʈ ������



    // ���� ����
    public float bossHP = 10;
    [SerializeField] float attackRange;  // ��Ÿ� 
    [SerializeField] float bossSpeed;    // �̵��ӵ� 
    [SerializeField] float bossCoolTime; // ������ �� Ÿ��
    [SerializeField] bool isOnPattern = false;    // ���� �������� �� 

    // ���� ���� ���� ����
    int bossPatternNum;
    // ���¸� Idle�� ����
    BossState state = BossState.Idle;
    // ����� ������, ���� �߰� �߰��� ������
    // �������� �⺻������ �÷��̾ ���� ���� ������ Attack ��Ȳ���� 

    // ��ų ���� ���� ��ų ����Ʈ ������ ���� 
    [SerializeField] GameObject slashPrefab;
    [SerializeField] Transform slashPoint;

    [SerializeField] GameObject fireballPrefab;

    WaitForSeconds time; // ���� ��Ÿ�� �� �ð� 
    [SerializeField] BossState curBossState; // ������ ���� ���� Ȯ�ο� 

    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        time = new WaitForSeconds(bossCoolTime);// ���� ��Ÿ�� �� �ð� 
    }


    private void Update()
    {
        curBossState = state; // ������ ���� ���� Ȯ�ο� 

        switch (state)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Move:
                Move();
                break;
            case BossState.Die:
                Die();
                break;
        }

    }
    private void Idle()
    {
        // Idle �ִϸ��̼�
        // animator.Play();
        animator.Play("idle");
        // �÷��̾� ��ġ�� �ٶ󺸰�
        Mirrored();

        // �÷��̾���� �Ÿ� distanceToPlayer
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
        Debug.Log(distanceToPlayer);
        // �÷��̾�� �Ÿ��� ����Ͽ� ���� ����
        if (distanceToPlayer > attackRange)
        {
            state = BossState.Move;
        }
        else if (distanceToPlayer < attackRange)
        {
            // �÷��̾�� �ʹ� ����� ���� �ٷ� ����! 
            if (isOnPattern == false) // �������� ������ ������ 
            {
                StartCoroutine("Attack");
            }
        }
    }


    private void Move()
    {
        animator.Play("run");
        Vector2 newPosition = new Vector2(
            Mathf.MoveTowards(transform.position.x, player.transform.position.x, bossSpeed * Time.deltaTime),
            transform.position.y
        );
        transform.position = newPosition;
        // �̵� �߿��� ��������Ʈ�� �÷��̾ �ٶ󺸰� ó��
        Mirrored();

        // ��Ÿ� ���� ������ ���� ���·� ��ȯ
        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (isOnPattern == false) // �������� ������ ������ 
            {
                StartCoroutine("Attack");
            }
        }
    }

    /*if (�÷��̾���� �Ÿ��� �����Ÿ� �̻��̸�)
     *������ ���� Move�� ����
     * state = BossState.Move;
     * 
     * else �ƴ϶��
     * ������ ���� Attack���� ����
     * state = BossState.Attack;
    */


    IEnumerator Attack()
    {
        state = BossState.Attack;

        isOnPattern = true;
        bossPatternNum = Random.Range(3, 4);
        Debug.Log($"{bossPatternNum}�� ���� ����");
        switch (bossPatternNum)
        {
            case 1:
                StartCoroutine("Rush");
                break;
            case 2:
                StartCoroutine("BackStep");
                break;
            case 3:
                StartCoroutine("Slash");
                break;
            case 4:
                StartCoroutine("FireBall");
                break;
            case 5:
                StartCoroutine("MoreSlash");
                break;


        }
        yield return time;
        isOnPattern = false;

    }
    private void Die()
    {
        // hp ���� �Ҹ� �� ��� �ִϸ��̼� ���� �� ������ �Ҹ�

        // ��� �ִϸ��̼� 
        // animator.Play();

        // ������Ʈ ���� ó��
        //Destroy(gameObject, 2f);
    }
    // �齺��(��Ÿ������) , �������� ũ�� �ֵθ�, ���� , �ϴÿ��� ȭ���� �߻� , ���߿��� �����ϸ鼭 ������ ���� , 2������ ���� 
    // ��� ���Ͽ��� ���ð��� �ʿ��ϴϱ� �ڷ�ƾ���� �ۼ��ϴ°� ������!
    IEnumerator Rush()  // ��ȹ���� �Լ��̸��� bodytacle? �����Ÿ� ���ڴٰ� ��
    {
        // �÷��̾� �������� ����
        Vector2 playerDirection = (player.transform.position - transform.position).normalized;
        Vector2 startPosition = transform.position;

        // ���� �Ÿ��� �ӵ� ����
        float targetDistance = 30f;
        float rushSpeed = 70f;
        float currentDistance = 0f;

        // Rigidbody2D�� ���� ���Ͽ� ����
        bossRigid.AddForce(playerDirection * rushSpeed, ForceMode2D.Impulse);

        // ��ǥ �������� �̵�
        while (currentDistance < targetDistance)
        {
            currentDistance = Vector2.Distance(transform.position, startPosition);

            // ��ǥ �Ÿ� ��ó������ drag ����
            if (currentDistance >= targetDistance * 0.9f)
            {
                bossRigid.drag = 2f; // �Ÿ��� 90% ���� �� �ӵ� ���̱�
            }

            // ��ǥ �Ÿ��� �����ϸ� ����
            if (currentDistance >= targetDistance)
            {
                bossRigid.velocity = Vector2.zero;
                break;
            }

            yield return null;
        }

        Debug.Log("���� �Ϸ�!");
        bossRigid.drag = 0; // drag �ʱ�ȭ
        yield return time; // ��Ÿ�� ���

        state = BossState.Idle;
    }
    private IEnumerator BodyTackle()
    {
        // ���� ��ġ�� ����

        // �÷��̾� ��ġ Ž��
        Vector2 playerDirection = (player.transform.position - transform.position).normalized;
        // ���� ���� ��ġ
        Vector2 startPosition = transform.position;

        // �ִϸ��̼� ���
        animator.Play("jump");

        // ���� ��ǥ �Ÿ� ����
        float targetDistance = 30f; // ������ �̵��� �Ÿ� (���ϴ� ������ ����)
        float currentDistance = 0f; // ���� �̵��Ÿ�
        float tackleSpeed = 70f;  // ���� �ӵ�

        // ���� ����
        Debug.Log("���� ����---!");

        // while������ ���� �Ÿ��� ����
        while (currentDistance < targetDistance)
        {
            // �浹 �����ϰ� ��ǥ ��ġ�� �̵�
            bossRigid.MovePosition(bossRigid.position + playerDirection * tackleSpeed * Time.deltaTime);
            currentDistance = Vector2.Distance(transform.position, startPosition);

            if (currentDistance >= targetDistance)
            {
                bossRigid.velocity = Vector2.zero;
                break;
            }

            yield return null;
        }

        yield return new WaitForSeconds(3f);
      
        Debug.Log("���� ��---!");
        state = BossState.Idle;
    }

    IEnumerator BackStep()
    {

        // �齺�ܿ� �ʿ��� �ൿ �ۼ� 
        Debug.Log("�齺��!");
        yield return time; // ��Ÿ�� ��ŭ ��ٷȴٰ� �ٸ� ������ �����Ҽ� �ְ�


        state = BossState.Idle;
    }
    IEnumerator Slash()
    {

        //�ʿ��� �ൿ �ۼ� 
        GameObject obj = Instantiate(slashPrefab, slashPoint.position, slashPoint.rotation);
        Destroy(obj,0.5f);
        Debug.Log("������!");
        yield return time; // ��Ÿ�� ��ŭ ��ٷȴٰ� �ٸ� ������ �����Ҽ� �ְ�



        state = BossState.Idle;
    }
    IEnumerator FireBall()
    {
        Vector2 playerDirection = (player.transform.position - transform.position).normalized;

        transform.position = new Vector2(transform.position.x, transform.position.y + 15f);  // ������ٵ�� �÷������ϱ� �� ����� + ȭ�� �� ��� �ٵ� ?  , 
        animator.Play("jump");
        yield return time;
        GameObject obj1 = Instantiate(fireballPrefab, slashPoint.position, slashPoint.rotation);
        Rigidbody2D obj1rb = obj1.GetComponent<Rigidbody2D>();
        obj1rb.AddForce(Vector2.left*10f,ForceMode2D.Impulse);
        Destroy(obj1, 2f);
        //GameObject obj2 = Instantiate(fireballPrefab, slashPoint.position, slashPoint.rotation);
        //Rigidbody2D obj2rb = obj2.GetComponent<Rigidbody2D>();
        //obj2rb.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
        //Destroy(obj2, 2f);
        //GameObject obj3 = Instantiate(fireballPrefab, slashPoint.position, slashPoint.rotation);
        //Rigidbody2D obj3rb = obj3.GetComponent<Rigidbody2D>();
        //obj3rb.AddForce(Vector2.right * 10f, ForceMode2D.Impulse);
        //Destroy(obj3, 2f);

        // �����ϰ� ȭ���� �߻� 
        Debug.Log("ȭ����!");
        yield return time; // ��Ÿ�� ��ŭ ��ٷȴٰ� �ٸ� ������ �����Ҽ� �ְ� , ���ð��� ª���� ���߿��� move�� �ٲ�ϱ� ���߰��� �����Ŵ� ���ð��� ���� ���� �ҵ� 



        state = BossState.Idle;
    }
    IEnumerator MoreSlash()
    {


        // �齺�ܿ� �ʿ��� �ൿ �ۼ� 
        Debug.Log("���ߺ���!");
        yield return time; // ��Ÿ�� ��ŭ ��ٷȴٰ� �ٸ� ������ �����Ҽ� �ְ�



        state = BossState.Idle;
    }



    // �������� �������� �ַ��� BossPattern bossPattern = boss.GetComponent<BossPattern>();
    // bossPattern.TakeDamage(������);�� �������� �� �� ���� , �̺�Ʈ�� ȣ���ϸ� �ɵ�
    public void TakeDamage(float damage)
    {
        bossHP -= damage;

        // ������ ü���� 0 ���ϰ� �Ǹ� ���¸� Die�� ����
        if (bossHP <= 0)
        {
            state = BossState.Die;
        }
    }

    private void Mirrored()
    {
        // �÷��̾ ������ ���ʿ� ������ ������ ��������, �����ʿ� ������ �������� �ٶ󺸰� ����
        if (player.transform.position.x < transform.position.x)
        {
            // ������ ������ �ٶ󺸵��� ��
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            // ������ �������� �ٶ󺸵��� ��
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
