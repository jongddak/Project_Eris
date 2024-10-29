using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform[] shootingPoints;

    [SerializeField] GameObject player;



    private void Update()
    {
        
    }



    IEnumerator ShootingLaser() //레이저 패턴
    {
        for (int i = 0; i < 6; i++) 
        {

        }


    }

    IEnumerator ShootingMissile() // 미사일 패턴 
    {
        for (int i = 0; i < 3; i++) 
        {

        }
    }
}
