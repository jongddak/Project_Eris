using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LenPlatformLoop : MonoBehaviour
{
    [SerializeField] MoveLenPlatform moveLenPlatform;
    [SerializeField] bool isUpMove;
    [SerializeField] GameObject RiseCreatePos;
    [SerializeField] GameObject DesCreatePos;

    // Respawn 태그와 충돌하는 경우 오브젝트의 위치를 재배치
    // 단, MoveLenPlatform.cs에서 IsUpMove를 받아서
    // IsUpMove == true (올라가는 중) 일때는 UpCreatePos로 재배치
    // IsUpMove == false (내려가는 중) 일때는 DownCreatePos로 재배치

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            if (moveLenPlatform.isUpMove == true)
            {
                gameObject.transform.position = new Vector2(RiseCreatePos.transform.position.x, RiseCreatePos.transform.position.y);
            }
            else if(moveLenPlatform.isUpMove == false)
            {
                gameObject.transform.position = new Vector2(DesCreatePos.transform.position.x, DesCreatePos.transform.position.y);
            }

        }
    }

}

