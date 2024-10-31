using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLenPlatform : MonoBehaviour
{
    // 일정한 속도로
    [SerializeField] float moveSpeed;
    // 방향별로 이동
    [SerializeField] public bool isUpMove; // true 이면 위로 이동
    [SerializeField] GameObject DesDelete; // 하강중일 때 삭제 판정 충돌체
    [SerializeField] GameObject RiseDelete; // 상승중일 때 삭제 판정 충돌체

    private void Update()
    {
        if (isUpMove)
        {
            DesDelete.SetActive(false);
            RiseDelete.SetActive(true);
            MoveUp();
        }
        else if (!isUpMove)
        {
            DesDelete.SetActive(true);
            RiseDelete.SetActive(false);
            MoveDown();
        }
    }

    /// <summary>
    /// 발판을 일정한 속도로 위로 계속 이동
    /// </summary>
    public void MoveUp()
    {
        transform.Translate(Vector2.up * moveSpeed*Time.deltaTime);
    }

    public void MoveDown()
    {
        transform.Translate(Vector2.down * moveSpeed * Time.deltaTime);
    }

}
