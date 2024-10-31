using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAura : MonoBehaviour
{
    // 플레이어 프리펩
    [SerializeField] GameObject player;
    // 검기의 스피드
    [SerializeField] float swordAuraSpeed;
    public Transform basetransform;
    // 발사 방향
    public int direction;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        Destroy(gameObject, 3f);
    }   

    private void Update()
    {
        basetransform.Translate(transform.forward * direction * swordAuraSpeed * Time.deltaTime);
      
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            // 소멸 애니메이션 적용 가능
            Destroy(gameObject); // 애니메이션 후 소멸
        }
    }
}
