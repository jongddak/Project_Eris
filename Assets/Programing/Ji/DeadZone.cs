using UnityEngine;

public class DeadZone : MonoBehaviour
{
    /// <summary>
    /// DeadZone에 충돌체가 충돌하는 경우 판정
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            /* tag가 DeadZone인 경우 
             * 플레이어의 사망
             * DieUpdate()
             */
            Debug.Log("플레이어 사망");
        }
    }
}
