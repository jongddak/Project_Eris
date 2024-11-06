using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAssist : MonoBehaviour
{
    //ĳ������ �������� �ӵ��� ���߽�Ű�� ���� ����
    [SerializeField] float fallMultiplier = 20f;
    //ĳ������ ����Ű �Է��� ���� �� �߷��� ������ Ű�� �� ������ ���������� ����
    [SerializeField] float lowJumpMultiplier = 20f;
    Rigidbody2D rb;
    [SerializeField] Collision coll;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (rb.velocity.y < 0 && coll.onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0); // ���� �� y �ӵ� �ʱ�ȭ
        }

        //ĳ���Ͱ� �ϰ����� ��
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //ĳ���Ͱ� ����������� ���� ��ư�� ���� ��
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.C))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
