using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] Transform[] shootingPoints;

    [SerializeField] GameObject player;


    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject missilePrefab;
    [SerializeField] GameObject bulletPrefab;



    private void Update()
    {
        
    }

    IEnumerator ShootingBullet() //총알 패턴 
    {
        while (true) 
        {
            yield return null;
        }

        
    }


    IEnumerator ShootingLaser() //레이저 패턴
    {
        for (int i = 0; i < 6; i++) 
        {
               
        }

        yield return null;
    }

    IEnumerator ShootingMissile() // 미사일 패턴 
    {
        for (int i = 0; i < 3; i++) 
        {
           int x = Random.Range(0, 7);
        }
        yield return null;
    }
}
