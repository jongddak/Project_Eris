using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] float speed = 10;
    [SerializeField] float jumpForce = 10;
    private bool isGrounded = false; // 점프 가능 여부를 위한 변수

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 dir = new Vector2(x, 0); // 2D 환경에서는 z 대신 y를 사용
        Walk(dir);

        // C 키를 눌렀을 때, 땅에 있을 때만 점프 가능
        if (Input.GetKey(KeyCode.C) && isGrounded)
        {
            Jump();
        }
    }

    private void Walk(Vector2 dir)
    {
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * jumpForce;
        isGrounded = false; // 점프 후에는 공중에 있으므로 false로 설정
    }

    // 바닥에 닿았는지 체크하기 위해 콜리전 감지
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ground라는 태그가 있는 오브젝트와 충돌하면 땅에 있다고 판단
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // 바닥에서 떨어지면 다시 false로 설정
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
}
