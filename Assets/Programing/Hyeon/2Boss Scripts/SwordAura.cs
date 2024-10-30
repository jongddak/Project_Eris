using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    // 검기의 스피드
    [SerializeField] float swordAuraSpeed;
    // 발사 방향
    private Vector2 direction;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        HomingSwordAura();

        /*
        if (player != null)
        {
            // 플레이어 방향 계산
            direction = ((Vector2)player.transform.position - (Vector2)transform.position).normalized;

            // 방향을 기반으로 각도 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // 검기를 플레이어를 향하도록 회전
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }*/
    }
    /*private void Update()
    {
        // 검기를 플레이어 방향으로 이동
        transform.Translate(direction * swordAuraSpeed * Time.deltaTime);
    }*/
    private void HomingSwordAura()
    {
        Vector2 diretion = (Vector2)player.transform.position - (Vector2)transform.position;
        direction.Normalize();

        float rotate = Vector3.Cross(diretion, transform.up).z;
        transform.Rotate(0, 0, -rotate * 400f * Time.deltaTime);
    }

    private void Update()
    {
        transform.Translate(Vector2.up * 10f * Time.deltaTime);
    }
}
