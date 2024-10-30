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


    Coroutine curRoutine;
    
    private void Start()
    {
        // curRoutine = StartCoroutine(BossDo());
        StartCoroutine(ShootingMissile());
    }

    private void Update()
    {

    }

    // 발판이 움직이는 패턴 



    IEnumerator BossDo()
    {
        while (true)
        {






            yield return null;
        }
    }


    IEnumerator sleeptime() // 몇초동안 딜 패턴 중지하고 딜타임용 발판 생김 
    {

        yield return null;
    }

    IEnumerator ShootingBullet() //총알 패턴 
    {
        while (true)
        {
            yield return null;
        }


    }

    IEnumerator ShootingLaser() //레이저 패턴, 1발씩 6번씩 쏨 
    {
        for (int i = 0; i < 6; i++)
        {
            int rand = Random.Range(0, 6);
            Instantiate(missilePrefab, shootingPoints[rand].position, shootingPoints[rand].rotation);
            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }

    IEnumerator ShootingMissile() // 미사일 패턴 , 한번에 3발을 쏨 
    {
        List<int> randNums = new List<int> { 0, 1, 2, 3, 4, 5 };  //  중복 안되는 n개 뽑는 기능 
        List<int> pickNum= new List<int>();
        while (pickNum.Count < 3) 
        { 
            int rand = Random.Range(0, randNums.Count); 
            if (randNums.Contains(rand) == true) 
            {
                pickNum.Add(rand);
                randNums.Remove(rand);
            }
            if (pickNum.Count == 3) 
            {
                Debug.Log($"{pickNum[0]}{pickNum[1]}{pickNum[2]}");
                break;
                
            } 
        }


;       
        Instantiate(missilePrefab, shootingPoints[pickNum[0]].position, shootingPoints[pickNum[0]].rotation);

        Instantiate(missilePrefab, shootingPoints[pickNum[1]].position, shootingPoints[pickNum[1]].rotation);

        Instantiate(missilePrefab, shootingPoints[pickNum[2]].position, shootingPoints[pickNum[2]].rotation);
        yield return null;
    }
}
