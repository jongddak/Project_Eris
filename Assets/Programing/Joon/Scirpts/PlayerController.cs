using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{

    //�ڿ������� ȸ���� ���� �÷��̾��� �������� �ٲ� ���ӿ�����Ʈ
    [SerializeField] private GameObject gameObjectRotationPoint;

    //�׾��� �� ��� UI ������Ʈ
    public GameObject uiDead;

    //�÷��̾ ���� �� �ִ� ����
    public enum PlayerState { Idle, Run, Jump, Fall, Grab, GrabJump, 
        Dash, Attack, DashAttack, Die };
    [SerializeField] public PlayerState curState;     // �÷��̾��� ���� ����

    private Rigidbody2D rb;

    [SerializeField] Collision coll;

    [Header("MoveInfo")]
    [SerializeField] float maxSpeed = 10f;      // �ִ� �̵� �ӵ� 
    [SerializeField] float moveAccel = 30f;     // �̵� ���ӵ�
    [SerializeField] bool canMove = true;       // �̵� ���� ����(���Ͽ�)

    [Header("Jump&Fall")]
    [SerializeField] float jumpSpeed = 30f;     // ���� �ӵ�
    [SerializeField] float maxFallSpeed = 10f;  // �ִ� ���� �ӵ�
    [SerializeField] float jumpRemainTime;      // ���� ���� �ð�
    [SerializeField] float maxJumpUpTime = 0.5f;// �ִ� ���� ���� �ӵ�
    [SerializeField] bool isJumping = false;
    [SerializeField] Coroutine jumpCoroutine;

    private BoxCollider2D boxCollider;
    private Vector2 originalColliderSize;
    private Vector2 reducedColliderSize;
    //public event EventHandler OnJumpDown;

    [Header("DashInfo")]
    [SerializeField] float dashSpeed = 25f;     // ��� �ӵ�
    [SerializeField] float dashTime = 0.4f;     // ��� ���� �ð�
    private float dashTimeLeft;                 // ��� ���� �ð�
    [SerializeField] bool canDash = true;       // ��� ���� ����
    [SerializeField] bool isinvincible = false; // ��� ���� ���� ����

    [Header("GrapInfo")]
    [SerializeField] float SlipSpeed = 1f;      // ���� ����� ���� �� �������� �ӵ�
    [SerializeField] bool isGrabJumping = false;

    [Header("AnamationInfo")]
    [SerializeField] Animator playerAnimator;
    private int curAniHash;                     // ���� ������ �ִϸ��̼��� �ؽ��� ��� ����
    [SerializeField] GameObject GFX;            // ĳ���� ȸ���� ���� �θ� ������Ʈ
    [SerializeField] GameObject[] attackParticle;

    //�÷��̾� �ִϸ��̼��� �Ķ���� �ؽ� ����
    private static int idleHash = Animator.StringToHash("Idle");
    private static int runHash = Animator.StringToHash("Run");
    private static int jumpHash = Animator.StringToHash("Jump");
    private static int fallHash = Animator.StringToHash("Fall");
    private static int grabHash = Animator.StringToHash("Grab");
    private static int attack1Hash = Animator.StringToHash("Attack1");
    private static int attack2Hash = Animator.StringToHash("Attack2");
    private static int attack3Hash = Animator.StringToHash("Attack3");
    private static int dashHash = Animator.StringToHash("Dash");
    private static int dashAttackHash = Animator.StringToHash("DashAttack");
    private static int landingHash = Animator.StringToHash("Landing");
    private static int dieHash = Animator.StringToHash("Die");

    [Header("AttackInfo")]
    [SerializeField] GameObject attackPoint;                   //������ ������ ��
    [SerializeField] bool isDead = false;                      //�÷��̾��� ���� �Ǻ�
    [SerializeField] int currentAttackCount = 0;               //���� ���� Ƚ��
    private float lastAttackTime;                              //������ ���� �ð�
    public float comboResetTime = 1.5f;                        //���� �޺��� �ʱ�ȭ �Ǵ� �ð�
    [SerializeField] bool isAttacking = false;
    //private PlayerRPG playerRPG;

    [Header("HoveringInfo")]
    //�÷��̾ �������� �� ����(���� ���� õõ�� ������)
    [SerializeField] float hoverForce = 10f;                   

    [Header("StageInfo")]
    [SerializeField] public bool canUseDashAttack = false;     //1�������� Ŭ����� Ȱ��ȭ
    [SerializeField] public bool canUseHovering = false;       //2�������� Ŭ����� Ȱ��ȭ

    [Header("CameraInfo")]
    [SerializeField] CameraController CameraController;

    [Header("AudioInfo")]
    [SerializeField] PlayerSoundController PlayerSoundController;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        originalColliderSize = boxCollider.size;
        reducedColliderSize = new Vector2(originalColliderSize.x, originalColliderSize.y * 0.5f);
        uiDead.SetActive(false);

        DataManager.Instance.LoadGameData();
        if (DataManager.Instance == null)  // ���� ����
        {
            Debug.Log("�ҷ����� �ʾƵ� ��");
        }
        else if (DataManager.Instance != null) // �̾��ϱ� 
        {
            canUseDashAttack = DataManager.Instance.data.isUnlock[0];
            canUseHovering = DataManager.Instance.data.isUnlock[1];
            Debug.Log(canUseDashAttack);
            Debug.Log(canUseHovering);
        }
    }

    private void Update()
    {
        if (!canMove) return;

        ComboUpdate(); //������ �ð� ���� ������ �̷������ ������ �����޺� �ʱ�ȭ

        //���¿� ���� ������Ʈ �Լ� ȣ��
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
            case PlayerState.GrabJump:
                GrapJumpUpdate();
                break;
            case PlayerState.Dash:
                DashUpdate();
                break;
            case PlayerState.Attack:
                AttackUpdate();
                break;
            case PlayerState.DashAttack:
                DashAttackUpdate();
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
        //�¿� �Է°��� ���� ��
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            curState = PlayerState.Run;
        }

        //�÷��� ���̾ �ؿ� ���� �� ���� �߰�
        /*if (Input.GetKey(KeyCode.DownArrow) &&
            Input.GetKeyDown(KeyCode.C))
        {
            LowJump();
        }*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpCoroutine = StartCoroutine(JumpRoutine());

        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());        // ���� �ڷ�ƾ ȣ��
        }
    }

    private void RunUpdate()
    {
        Move();
        PlayerSoundController.StartRunSound();
        //�÷��̾��� �ӵ��� ���� 0�� ��
        if (rb.velocity.sqrMagnitude < 0.01f)
        {
            curState = PlayerState.Idle;
            PlayerSoundController.StopRunSound();
        }
        if (rb.velocity.y < -0.01f && !coll.onGround)
        {
            curState = PlayerState.Fall;
            PlayerSoundController.StopRunSound();
        }

        /*if (Input.GetKey(KeyCode.DownArrow) &&
            Input.GetKeyDown(KeyCode.C))
        {
            LowJump();
        }*/

        if (Input.GetKey(KeyCode.Space))
        {
            jumpCoroutine = StartCoroutine(JumpRoutine());
            PlayerSoundController.StopRunSound();
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            Dash();
            PlayerSoundController.StopRunSound();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());        // ���� �ڷ�ƾ ȣ��
            PlayerSoundController.StopRunSound();
        }
    }

    private void JumpUpdate()
    {
        Move();

        //���� �پ� ������ y�� �ӵ��� ��ȭ�� ���� ���� ��
        if (coll.onGround && rb.velocity.y < 0.01f)
        {
            curState = PlayerState.Idle;
            canDash = true;
        }
        else if (rb.velocity.y < -0.01f && !coll.onGround)
        {
            curState = PlayerState.Fall;  // ���� ���·� ��ȯ
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
            }
            if (rb.velocity.y > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
            }
        }


        if ((coll.onLeftWall && Input.GetKey(KeyCode.LeftArrow)) || (coll.onRightWall && Input.GetKey(KeyCode.RightArrow)))
        {
            if (jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
            }
            Grab();
        }


        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            if (jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
            }
            Dash();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());        // ���� �ڷ�ƾ ȣ��
        }
    }

    private void FallUpdate()
    {
        Move();

        // �����ϸ� Idle ���·� ��ȯ
        if (coll.onGround || coll.onPlatform)
        {
            curState = PlayerState.Idle;
            canDash = true;
            PlayerSoundController.PlayLandingSound();
        }

        if ((coll.onLeftWall && Input.GetKey(KeyCode.LeftArrow)) || (coll.onRightWall && Input.GetKey(KeyCode.RightArrow)))
        {
            Grab();
        }

        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());        // ���� �ڷ�ƾ ȣ��
        }
        if (Input.GetKey(KeyCode.LeftShift) && canUseHovering)
        {
            Hovering();
        }
    }

    private void GrabUpdate()
    {
        //�÷��̾��� ���� ���̳� �÷����� ��� ���� ��
        if (coll.onGround || coll.onPlatform)
        {
            curState = PlayerState.Idle;
            canDash = true;
        }
        //���� ����ְ� �� ���� �������� ����Ű�� �Է��ϰ� ���� ��
        if ((coll.onLeftWall && Input.GetKey(KeyCode.LeftArrow)) || (coll.onRightWall && Input.GetKey(KeyCode.RightArrow)))
        {
            rb.AddForce(-Physics2D.gravity, ForceMode2D.Force);
            // y �ӵ��� -SlipSpeed�� �����Ͽ� õõ�� �������� ��
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -SlipSpeed));
        }
        else
        {
            UnGrab(); // ����Ű�� ���� ����� ���� ����
            return;
        }

        // ����� ���¿��� ���� �Է� �� ������ ����
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(GrabJumpCoroutine());
        }
    }
    private void GrapJumpUpdate()
    {
        Move();

        //���� �پ� ������ y�� �ӵ��� ��ȭ�� ���� ���� ��
        if (coll.onGround && rb.velocity.y < 0.01f)
        {
            curState = PlayerState.Idle;
            canDash = true;
        }
        else if (rb.velocity.y < -0.01f && !coll.onGround)
        {
            curState = PlayerState.Fall;  // ���� ���·� ��ȯ
        }

        if (isGrabJumping)
        {
            return;
        }
        //���� ����ְ� �� ���� �������� ����Ű�� �Է��ϰ� ���� ��
        if ((coll.onLeftWall && Input.GetKey(KeyCode.LeftArrow)) || (coll.onRightWall && Input.GetKey(KeyCode.RightArrow)))
        {
            Grab();
        }
        if (Input.GetKeyDown(KeyCode.Z) && canDash)
        {
            Dash();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(Attack());        // ���� �ڷ�ƾ ȣ��
        }
    }


    private void DashUpdate()
    {
        gameObject.layer = LayerMask.NameToLayer("God");
        //���� ����ְ� �� ���� �������� ����Ű�� �Է��ϰ� ���� ��
        if ((coll.onLeftWall && Input.GetKey(KeyCode.LeftArrow)) || (coll.onRightWall && Input.GetKey(KeyCode.RightArrow)))
        {
            Grab();
            rb.gravityScale = 5f;
        }
        //�뽬 ���ӽð��� ���� ���� ��
        if (dashTimeLeft > 0)
        {
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            rb.gravityScale = 5f;
            curState = PlayerState.Fall;  // ��� ���� �� ���� ���·� ��ȯ
            gameObject.layer = LayerMask.NameToLayer("Player");
        }

        if (Input.GetKeyDown(KeyCode.X) && canUseDashAttack)
        {
            StartCoroutine(DashAttack());
        }
    }

    private void AttackUpdate()
    {
        if (rb.velocity.y < 0 && !isAttacking) // ĳ���Ͱ� �ϰ� ���� ���
        {
            curState = PlayerState.Fall; // Fall ���·� ��ȯ
        }
        else if((coll.onGround || coll.onPlatform) && !isAttacking)
        {
            curState = PlayerState.Idle; // Idle ���·� ��ȯ
        }
    }
    private void DashAttackUpdate()
    {

    }

    private void DieUpdate()
    {

    }
    private void HoveringUpdate()
    {

    }

    private void ComboUpdate()
    {
        //������ ���ݽð��� �޺����� �ð����� ������� ���� ���� Ƚ�� �ʱ�ȭ
        if (Time.time - lastAttackTime > comboResetTime)
        {
            currentAttackCount = 0;
        }
    }
    private void Move()
    {
        float xInput = Input.GetAxisRaw("Horizontal");

        if (xInput > 0)
        {
            GFX.transform.localScale = new Vector3(1, 1, 1);
            CameraController.isLeft = false;
        }
        else if (xInput < 0)
        {
            GFX.transform.localScale = new Vector3(-1, 1, 1);
            CameraController.isLeft = true;
        }

        //float xSpeed = Mathf.Lerp(rb.velocity.x, xInput * maxSpeed, moveAccel);
        float xSpeed = Mathf.MoveTowards(rb.velocity.x, xInput * maxSpeed, Time.deltaTime * moveAccel);
        float ySpeed = Mathf.Max(rb.velocity.y, -maxFallSpeed);

        rb.velocity = new Vector2(xSpeed, ySpeed);
        //PlayerSoundController.PlayRunSound();
    }

    private IEnumerator JumpRoutine()
    {
        curState = PlayerState.Jump;
        jumpRemainTime = maxJumpUpTime;
        while (jumpRemainTime >= 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpRemainTime -= Time.deltaTime;
            yield return null;
        }

    }

    /*private void LowJump()
    {
        curState = PlayerState.Fall;
        OnJumpDown?.Invoke(this, EventArgs.Empty);
    }*/

    private void Grab()
    {
        curState = PlayerState.Grab;
        PlayerSoundController.PlayLandingSound();
        //�׷� �� �÷��̾��� �ӵ��� ���� ���� �ö󰡴� ���� ����
        rb.velocity = Vector2.zero;
        canDash = true;
    }

    private void UnGrab()
    {
        curState = PlayerState.Fall;
    }

    /*private void GrabMove()
    {
        rb.velocity = Vector2.up * Input.GetAxisRaw("Vertical") * 3f;
    }*/

    private IEnumerator GrabJumpCoroutine()
    {
        isGrabJumping = true;
        curState = PlayerState.GrabJump;
        rb.gravityScale = 5f;

        if (coll.onLeftWall)
        {
            rb.velocity = new Vector2(65f, 60f);
            //������ ���� ���ʺ��� �� �������� �⺻������ �������� ������
            GFX.transform.localScale = new Vector3(1, 1, 1);
        }

        else if (coll.onRightWall)
        {
            rb.velocity = new Vector2(-65f, 60f);
            //������ ���� �����ʺ��� �� �������� �ݴ������ ������ ������
            GFX.transform.localScale = new Vector3(-1, 1, 1);
        }

        yield return new WaitForSeconds(0.3f);
        isGrabJumping = false;
    }

    private void Dash()
    {
        curState = PlayerState.Dash;
        dashTimeLeft = dashTime;

        float xInput = Input.GetAxisRaw("Horizontal");

        // ��� ���� ����
        Vector2 dashDirection;

        if (xInput != 0)
        {
            // ���� �Է��� ���� ��, �ش� �������� ���
            dashDirection = new Vector2(xInput, 0).normalized;
        }
        else
        {
            // ���� �Է��� ���� �� �÷��̾��� ȸ�� ���� ���� ��� ���� ����
            if (GFX.transform.localScale.x == 1f) // �������� �ٶ� ��
            {
                dashDirection = Vector2.right; // ���������� ���
            }
            else if (GFX.transform.localScale.x == -1f) // ������ �ٶ� ��
            {
                dashDirection = Vector2.left; // �������� ���
            }
            else
            {
                dashDirection = Vector2.zero; // ��� ������ �������� ���� ���
            }
        }

        rb.velocity = dashDirection * dashSpeed;
        if (!coll.onGround)
        {
            rb.gravityScale = 0f; // �߷� ����
        }
        PlayerSoundController.PlayDashSound();
        canDash = false;
    }

    private IEnumerator Attack()
    {
        curState = PlayerState.Attack;  // ���� ���·� ��ȯ
        isAttacking = true;

        currentAttackCount++;
        lastAttackTime = Time.time;

        // ���� ����Ʈ ���� ��ġ ����
        Vector2 effectPosition = attackPoint.transform.position;

        // ����Ʈ ����
        if (currentAttackCount <= attackParticle.Length)
        {
            // ���� ����Ʈ ���� ����
            Quaternion effectRotation = Quaternion.identity;

            //���⿡ ���� ����Ʈ ȸ��
            if (GFX.transform.localScale.x == -1f) // �������� �ٶ� ��
            {
                effectRotation = Quaternion.identity; // �⺻ ȸ�� ����
            }
            else if (GFX.transform.localScale.x == 1f) // ������ �ٶ� ��
            {
                effectRotation = Quaternion.Euler(0f, 180f, 0f); // z�� ���� 180�� ȸ��
            }

            GameObject attackEffect = Instantiate(attackParticle[currentAttackCount - 1], effectPosition, effectRotation);
            /*AttackT attackT = attackEffect.GetComponent<AttackT>();

            if (attackT != null)
            {
                attackT.playerRPG = playerRPG; // ������ ����Ʈ�� AttackT�� playerRPG ����
            }*/
            Destroy(attackEffect, 0.3f); // ���� �ð��� ���� �� �ı�
        }

        switch (currentAttackCount)
        {
            case 1:
                PlayerSoundController.PlayAttack1Sound();
                break;
            case 2:
                PlayerSoundController.PlayAttack2Sound();
                break;
            case 3:
                PlayerSoundController.PlayAttack3Sound();
                break;
        }

        // @�� ���
        yield return new WaitForSeconds(0.3f);

        if (currentAttackCount >= 3)
        {
            currentAttackCount = 0;
        }

        isAttacking = false;
    }

    private IEnumerator DashAttack()
    {
        curState = PlayerState.DashAttack;
        gameObject.layer = LayerMask.NameToLayer("God");
        // ���� ����Ʈ ���� ��ġ ����
        Vector2 effectPosition = attackPoint.transform.position;
        // ���� ����Ʈ ���� ����
        Quaternion effectRotation = Quaternion.identity;

        //���⿡ ���� ����Ʈ ȸ��
        if (GFX.transform.localScale.x == -1f) // �������� �ٶ� ��
        {
            effectRotation = Quaternion.identity; // �⺻ ȸ�� ����
        }
        else if (GFX.transform.localScale.x == 1f) // ������ �ٶ� ��
        {
            effectRotation = Quaternion.Euler(0f, 180f, 0f); // z�� ���� 180�� ȸ��
        }

        GameObject attackEffect = Instantiate(attackParticle[3], effectPosition, effectRotation);
        Destroy(attackEffect, 0.3f); // ���� �ð��� ���� �� �ı�

        yield return new WaitForSeconds(0.3f);
        gameObject.layer = LayerMask.NameToLayer("Player");

        rb.gravityScale = 5f;
        curState = PlayerState.Fall;
    }

    private void Hovering()
    {
        rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -hoverForce));
    }

    public async void Die()
    {
        isDead = true;
        curState = PlayerState.Die;
        await Utility.DelayAction(1.5f);
        PlayerSoundController.PlayDieSound();

        await Utility.DelayAction(1f);
        uiDead.SetActive(true);
        PlayerSoundController.PlayDefeatSound();

        await Utility.DelayAction(2.5f);
        GameManager.Instance.LoadSceneByName("Lobby");
    }

    public void StopMovement()
    {
        // �÷��̾��� �ӵ��� 0���� �����Ͽ� �������� ����
        rb.velocity = Vector2.zero;

        // �߷� �������� 0���� �����Ͽ� �߷� ������ ���� �ʵ��� ��
        rb.gravityScale = 0f;

        // �÷��̾��� ���� ���¸� Idle�� ��ȯ�Ͽ� ������ ���� �ʱ�ȭ
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
        if (curState == PlayerState.Fall)
        {
            temp = fallHash;
        }
        if (curState == PlayerState.Grab)
        {
            temp = grabHash;
        }
        if (curState == PlayerState.GrabJump)
        {
            temp = jumpHash;
        }
        if (curState == PlayerState.Dash)
        {
            temp = dashHash;
        }


        if (curState == PlayerState.Attack)
        {
            switch (currentAttackCount)
            {
                case 1:
                    temp = attack1Hash;
                    break;
                case 2:
                    temp = attack2Hash;
                    break;
                case 3:
                    temp = attack3Hash;
                    break;
            }
        }

        if(curState == PlayerState.DashAttack)
        {
            temp = dashAttackHash;
        }

        if (curState == PlayerState.Die)
        {
            temp = dieHash;
        }


        if (curAniHash != temp)
        {
            curAniHash = temp;
            //playerAnimator.Play(curAniHash);

            //�ִϸ��̼��� ���� �ؽð��� ���� �÷����ϸ� ��ȯ�ð��� 0.1�ʷ� �ΰ� �⺻ ���̾��� �ִϸ��̼��� ��ȯ�Ѵ�.
            playerAnimator.CrossFade(curAniHash, 0.1f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Boss")) // ������ �浹 ��
        {
            StartCoroutine(StunTime(1f));            // 1�� ���� �Է� ��Ȱ��ȭ
        }
    }

    private IEnumerator StunTime(float duration)
    {
        canMove = false;          // �Է� ��Ȱ��ȭ
        playerAnimator.speed = 0; // ��� �ִϸ��̼� �Ͻ� ����
        PlayerSoundController.StopRunSound();

        yield return new WaitForSeconds(duration); // 1�� ���

        canMove = true;           // �Է� �ٽ� Ȱ��ȭ
        playerAnimator.speed = 1; // �ִϸ��̼� ��Ȱ��ȭ
    }
}
