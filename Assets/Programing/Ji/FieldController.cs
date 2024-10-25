using UnityEngine;

public class FieldController : MonoBehaviour
{
    // BossPattern의 TakeDamage(); - 보스의 체력 감소
    // BossObjact의 이동이 맨 아래 Ground를 벗어날 수 없음
    // PlayerController의 AttackUpdate()와 DieUpdate()를 사용 할 것 같음
    // HealFlat.cs의 DeleteFlat(); - HealFlat과 충돌 시 일정 시간 후 삭제하는 함수

    /// <summary>
    /// 충돌이 발생한 시점에 나타날 행동
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DeadZone")
        {
            /* tag가 DeadZone인 경우 
             * 플레이어의 사망
             * DieUpdate()
             */
        }

        if (collision.gameObject.tag == "HealFlat")
        {
            /* tag가 HealFlat인 경우
             * 플레이어가 밟은 순간 카운팅 활성화
             * 특정 카운트가 지나면 오브젝트 삭제하도록
             * HealFlat.cs에서 함수 작성해 불러오기
             */
            // 충돌한 오브젝트에 있는 HealFlat 컴포넌트 불러오기

        }

    }

    /// <summary>
    /// 지속적으로 충돌 중인동안 지속적으로 발생하는 행동
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        /* if ( collision.gameObject.tag == "SideWall")
         * 플레이어가 SideWall 태그에 지속적으로 붙어있는 동안 계속해서
         * 플레이어의 지속적인 데미지 함수 실행
         */

    }
}
