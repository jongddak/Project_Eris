using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] float damagetoPl;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            Debug.Log($"{damagetoPl}�÷��̾� ����");
            PlayerRPG player = collision.GetComponent<PlayerRPG>();
            player.TakeDamage(damagetoPl);
            
            
        }
    }
}
