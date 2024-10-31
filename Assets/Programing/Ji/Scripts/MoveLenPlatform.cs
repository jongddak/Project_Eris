using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLenPlatform : MonoBehaviour
{
    // 일정한 속도로
    [SerializeField] public float moveSpeed;

    // 방향별로 이동
    // PlatformAttackPattern에서 라인별로 받아오도록 구현하기
    [SerializeField] PatternController patternController;

    private void Update()
    {
        if (patternController.isUpMove)
        {
            MoveUp();
        }
        else if (!patternController.isUpMove)
        {
            MoveDown();
        }
    }

    /// <summary>
    /// 발판을 일정한 속도로 위로 계속 이동
    /// </summary>
    public void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
    }

    public void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

}
