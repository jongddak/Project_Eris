using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //플레이어가 가질 수 있는 상태
    enum PlayerState {Idle, Run, Jump, Fall, Grab, Dash, Attack, Die}; 
    [SerializeField] PlayerState curState;     // 플레이어의 현재 상태

    private Rigidbody2D rb;

    [SerializeField] Collision coll;

    //private SpriteRenderer spriteRenderer;


    [Header("PlayerInfo")]
    [SerializeField] float maxSpeed = 10f;      // 최대 이동 속도
    [SerializeField] float maxFallSpeed = 10f;  // 최대 낙하 속도
    [SerializeField] float moveAccel = 30f;     // 이동 가속도
    [SerializeField] float jumpSpeed = 15f;     // 점프 속도
    [SerializeField] bool canMove = true;       // 이동 가능 여부(스턴용)

    [Header("PlayerStat")]
    [SerializeField] float attackPoint;
    [SerializeField] float curHp;
    [SerializeField] float maxHp;

    [Header("DashInfo")]
    [SerializeField] float dashSpeed = 25f;     // 대시 속도
    [SerializeField] float dashTime = 0.2f;     // 대시 지속 시간
    private float dashTimeLeft;                 // 대시 남은 시간
    [SerializeField] bool isDashing = false;    // 대시 중인지 여부
    [SerializeField] bool canDash = true;       // 대시 가능 여부

    [Header("GrapInfo")]
    [SerializeField] float SlipSpeed = 1f;      //벽을 붙잡고 있을 때 떨어지는 속도


    [SerializeField] Animator playerAnimator;  
    private int curAniHash;                     //현재 진행할 애니메이션의 해쉬를 담는 변수

    //플레이어 애니메이션의 파라미터 해시 생성
    private static int idleHash = Animator.StringToHash("Idle");
    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");
    private static int grabHash = Animator.StringToHash("Grab");
    private static int attackHash = Animator.StringToHash("Attack");
    private static int dieHash = Animator.StringToHash("Die");

    //자연스러운 회전을 위해 플레이어의 기준점을 바꾼 게임오브젝트
    [SerializeField] private GameObject gameObject;

    [SerializeField] bool isAttack = false;
    [SerializeField] private Collider2D attackSpot;          //공격이 진행된 곳
    [SerializeField] private GameObject attackEffectPrefabs; //공격 이펙트

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if (!canMove) return;

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
                AttackUpdate();
                break;
            case PlayerState.Die:
                DieUpdate();
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

        //좌우 입력값이 있을 때
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            curState = PlayerState.Run;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.V) && !isAttack)
        {
            StartCoroutine(Attack());        // 공격 코루틴 호출
        }
    }

    private void RunUpdate()
    {
        Move();

        //플레이어의 속도가 거의 0일 때
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
        if (Input.GetKeyDown(KeyCode.V) && !isAttack)
        {
            StartCoroutine(Attack());        // 공격 코루틴 호출
        }
    }

    private void JumpUpdate()
    {
        Move();

        //땅에 붙어 있으며 y축 속도의 변화가 거의 없을 때
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
        if (Input.GetKeyDown(KeyCode.V) && !isAttack)
        {
            StartCoroutine(Attack());        // 공격 코루틴 호출
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
        if (Input.GetKeyDown(KeyCode.V) && !isAttack)
        {
            StartCoroutine(Attack());        // 공격 코루틴 호출
        }
    }

    private void GrabUpdate()
    {
        //GrabMove();

        if (Input.GetKeyUp(KeyCode.Z))
        {
            UnGrab();
        }

        if (!coll.onWall) // 벽에 붙어있지 않을 때
        {
            rb.gravityScale = 0f; // 중력 비활성화
        }
        else
        {
            rb.gravityScale = 1f; // 중력 활성화
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -SlipSpeed)); // 서서히 떨어짐
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

    /*private void GrabMove()
    {
        rb.velocity = Vector2.up * Input.GetAxisRaw("Vertical") * 3f;
    }*/

    private void GrabJump()
    {
        curState = PlayerState.Jump;
        rb.gravityScale = 1f;
        
        if (coll.onLeftWall)
        {
            rb.velocity = new Vector2(13f, 10f);
            //붙잡은 벽이 왼쪽벽일 때 벽점프시 기본방향인 오른쪽을 보도록
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        
        else if (coll.onRightWall)
        {
            rb.velocity = new Vector2(-13f, 10f);
            //붙잡은 벽이 오른쪽벽일 때 벽점프시 반대방향인 왼쪽을 보도록
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void Dash()
    {
        isDashing = true;
        curState = PlayerState.Dash;
        dashTimeLeft = dashTime;

        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");

        //대시 방향 결정
        Vector2 dashDirection = new Vector2(xInput, yInput).normalized;

        if (dashDirection == Vector2.zero)
        {
            // 플레이어의 회전 값에 따라 대시 방향 결정
            if (Mathf.Approximately(transform.rotation.eulerAngles.y, 0f)) // 오른쪽을 바라볼 때
            {
                dashDirection = Vector2.right; // 오른쪽으로 대시
            }
            else if (Mathf.Approximately(transform.rotation.eulerAngles.y, 180f)) // 왼쪽을 바라볼 때
            {
                dashDirection = Vector2.left; // 왼쪽으로 대시
            }
        }

        rb.velocity = dashDirection * dashSpeed;
        canDash = false;
    }

    private IEnumerator Attack()
    {
        isAttack = true;
        curState = PlayerState.Attack;  // 공격 상태로 전환

        // 공격 이펙트 생성
        GameObject attackEffect = Instantiate(attackEffectPrefabs, transform.position, Quaternion.identity);

        // 1초 대기
        yield return new WaitForSeconds(1f);

        // 이펙트 및 콜라이더 삭제
        Destroy(attackEffect);

        // 공격 상태를 Idle로 전환
        curState = PlayerState.Idle;

        isAttack = false;
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

            //애니메이션을 현재 해시값을 통해 플레이하며 전환시간을 0.1초로 두고 기본 레이어의 애니메이션을 전환한다.
            playerAnimator.CrossFade(curAniHash, 0.1f, 0);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")) // 보스와 충돌 시
        {
            StartCoroutine(StunTime(1f));            // 1초 동안 입력 비활성화
        }
    }

    private IEnumerator StunTime(float duration)
    {
        canMove = false;          // 입력 비활성화
        playerAnimator.speed = 0; // 모든 애니메이션 일시 정지

        yield return new WaitForSeconds(duration); // 1초 대기

        canMove = true;           // 입력 다시 활성화
        playerAnimator.speed = 1; // 애니메이션 재활성화
    }
}
