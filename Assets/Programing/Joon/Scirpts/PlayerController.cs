using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum PlayerState {Idle, Run, Jump, Fall, Grab, Dash, Attack, Die};
    [SerializeField] PlayerState curState;

    private Rigidbody2D rb;

    [SerializeField] Collision coll;

    //private SpriteRenderer spriteRenderer;


    [Header("PlayerInfo")]
    [SerializeField] float maxSpeed = 10f;      // 최대 이동 속도
    [SerializeField] float maxFallSpeed = 10f;  // 최대 낙하 속도
    [SerializeField] float moveAccel = 30f;     // 이동 가속도
    [SerializeField] float jumpSpeed = 15f;     // 점프 속도

    [Header("DashInfo")]
    [SerializeField] float dashSpeed = 25f;     // 대시 속도
    [SerializeField] float dashTime = 0.2f;     // 대시 지속 시간
    private float dashTimeLeft;                 // 대시 남은 시간
    [SerializeField] bool isDashing = false;    // 대시 중인지 여부
    [SerializeField] bool canDash = true;       // 대시 가능 여부

    [SerializeField] Animator playerAnimator;
    private int curAniHash;

    private static int idleHash = Animator.StringToHash("Idle");
    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");
    private static int grabHash = Animator.StringToHash("Grab");
    private static int attackHash = Animator.StringToHash("Attack");
    private static int dieHash = Animator.StringToHash("Die");

    [SerializeField] private GameObject gameObject;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        //상태에 따른 업데이트 함수 호출
        switch (curState)
        {
            case PlayerState.Idle:
                IdleUpdate();
                break;
            case PlayerState.Run:
                RunUpdate();
                break;
            case PlayerState.Jump:
                JumpUpdate();
                break;
            case PlayerState.Fall:
                FallUpdate();
                break;
            case PlayerState.Grab:
                GrabUpdate();
                break;
            case PlayerState.Dash:
                DashUpdate();
                break;
            case PlayerState.Attack:
                DashUpdate();
                break;
            case PlayerState.Die:
                DashUpdate();
                break;
        }
    }

    private void FixedUpdate()
    {
        AnimatorPlay();
    }

    private void IdleUpdate()
    {
        Move();

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            curState = PlayerState.Run;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Jump();
        }
    }

    private void RunUpdate()
    {
        Move();

        if (rb.velocity.sqrMagnitude < 0.01f)
        {
            curState = PlayerState.Idle;
        }
        if (rb.velocity.y < -0.01f)
        {
            curState = PlayerState.Fall;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            Dash();
        }
    }

    private void JumpUpdate()
    {
        Move();

        if (coll.onGround && rb.velocity.y < 0.01f)
        {
            curState = PlayerState.Idle;
            canDash = true;
        }
        else if (rb.velocity.y < -0.01f)
        {
            curState = PlayerState.Fall;  // 낙하 상태로 전환
        }

        if (Input.GetKeyDown(KeyCode.Z) && coll.onWall)
        {
            Grab();
        }

        if (Input.GetKeyDown(KeyCode.C) && coll.onWall)
        {
            GrabJump();
        }
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            Dash();
        }
    }

    private void FallUpdate()
    {
        Move();

        // 착지하면 Idle 상태로 전환
        if (coll.onGround)
        {
            curState = PlayerState.Idle;
            canDash = true;
        }

        if (Input.GetKeyDown(KeyCode.Z) && coll.onWall)
        {
            Grab();
        }

        if (Input.GetKeyDown(KeyCode.C) && coll.onWall)
        {
            GrabJump();
        }
        if (Input.GetKeyDown(KeyCode.X) && canDash)
        {
            Dash();
        }
    }

    private void GrabUpdate()
    {
        GrabMove();

        if (Input.GetKeyUp(KeyCode.Z))
        {
            UnGrab();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            GrabJump();
        }
    }

    private void DashUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && coll.onWall)
        {
            Grab();
        }

        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            isDashing = false;
            if (coll.onGround)
            {
                curState = PlayerState.Idle;
            }
            curState = PlayerState.Fall;  // 대시 종료 후 낙하 상태로 전환
        }
    }

    private void AttackUpdate()
    {

    }

    private void DieUpdate()
    {

    }

    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        /*if (xInput < 0)
        {
            spriteRenderer.flipX = true; // 왼쪽 방향으로 이동 시 스프라이트를 뒤집음
        }
        else if (xInput > 0)
        {
            spriteRenderer.flipX = false; // 오른쪽 방향으로 이동 시 스프라이트를 정상으로
        }*/
        if (xInput > 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (xInput < 0)
        {
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //float xSpeed = Mathf.Lerp(rb.velocity.x, xInput * maxSpeed, moveAccel);
        float xSpeed = Mathf.MoveTowards(rb.velocity.x, xInput * maxSpeed, Time.deltaTime * moveAccel);
        float ySpeed = Mathf.Max(rb.velocity.y, -maxFallSpeed);

        rb.velocity = new Vector2(xSpeed, ySpeed);
    }

    private void Jump()
    {
        curState = PlayerState.Jump;
        rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
    }

    private void Grab()
    {
        if (!coll.onWall)
            return;

        curState = PlayerState.Grab;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 0f;
    }

    private void UnGrab()
    {
        curState = PlayerState.Jump;
        rb.velocity = Vector2.zero;
        rb.gravityScale = 1f;
    }

    private void GrabMove()
    {
        rb.velocity = Vector2.up * Input.GetAxisRaw("Vertical") * 3f;
    }

    private void GrabJump()
    {
        curState = PlayerState.Jump;
        rb.gravityScale = 1f;
        if (coll.onLeftWall)
        {
            rb.velocity = new Vector2(13f, 10f);
        }
        else if (coll.onRightWall)
        {
            rb.velocity = new Vector2(-13f, 10f);
        }
    }

    private void Dash()
    {
        isDashing = true;
        curState = PlayerState.Dash;
        dashTimeLeft = dashTime;

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        Vector2 dashDirection = new Vector2(xInput, yInput).normalized;
        if (dashDirection == Vector2.zero)
        {
            dashDirection = new Vector2(transform.localScale.x, 0);  // 입력이 없으면 바라보는 방향으로 대시
        }

        rb.velocity = dashDirection * dashSpeed;
        canDash = false;
    }

    public void StopMovement()
    {
        // 플레이어의 속도를 0으로 설정하여 움직임을 멈춤
        rb.velocity = Vector2.zero;

        // 중력 스케일을 0으로 설정하여 중력 영향을 받지 않도록 함
        rb.gravityScale = 0f;

        // 플레이어의 현재 상태를 Idle로 전환하여 움직임 상태 초기화
        curState = PlayerState.Idle;
    }

    private void AnimatorPlay()
    {
        int temp = idleHash;
        if (curState == PlayerState.Idle)
        {
            temp = idleHash;
        }
        if (curState == PlayerState.Run)
        {
            temp = runHash;
        }    
        if (curState == PlayerState.Jump)
        {
            temp = jumpHash;
        }
        /*if (curState == PlayerState.Fall)
        {
            temp = fallHash;
        }
        if (curState == PlayerState.Grab)
        {
            temp = grabHash;
        }*/

        if (curAniHash != temp)
        {
            curAniHash = temp;
            //playerAnimator.Play(curAniHash);
            playerAnimator.CrossFade(curAniHash, 0.1f, 0);
        }
    }
}
