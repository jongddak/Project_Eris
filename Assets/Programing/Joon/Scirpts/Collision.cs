using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Collision")]

    [SerializeField] float collisionRadius = 0.25f;
    public Vector2 bottomOffset, rightOffset, leftOffset;
    private Color debugColor = Color.red;


    [Header("Collision Settings")]

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] public bool onGround;
    [SerializeField] public bool onWall;
    [SerializeField] public bool onRightWall;
    [SerializeField] public bool onLeftWall;
    [SerializeField] public int wallSide;

    private void Update()
    {
        //지정된 범위(collisionRadius)의 원형충돌체로 감지하여 그라운드 레이어에 닿았는지 판별
        onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, collisionRadius, groundLayer);
        onRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, collisionRadius, groundLayer);
        onLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, collisionRadius, groundLayer);

        //오른쪽벽이나 왼쪽벽 둘 중 하나만 닿아도 벽에 닿은 것으로 판별
        onWall = onRightWall || onLeftWall;

        //오른쪽 벽에 닿아있을 경우 -1 / 왼쪽 벽에 닿아있을 경우 1 / 어느 벽에 닿아있는지 판별하는 변수
        wallSide = onRightWall ? -1 : 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = debugColor;

        var positions = new Vector2[] { bottomOffset, rightOffset, leftOffset };

        //충돌 범위 확인용 기즈모 표기
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, collisionRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, collisionRadius);
    }
}
