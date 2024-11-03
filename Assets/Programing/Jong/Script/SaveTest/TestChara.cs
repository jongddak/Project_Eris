using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestChara : MonoBehaviour
{
    [SerializeField] float moveSpeed;
   
    [SerializeField] float Atk;

    [SerializeField] bool firstStage;
    [SerializeField] bool secondStage;
    [SerializeField] bool thridStage;

    [SerializeField] bool ondash;
    [SerializeField] bool onhover;
    [Range(0, 1)]
    [SerializeField] float BgmVol;

    private void Start()
    {
        SaveTest.Instance.LoadGameData();
        Loading();
    }

    private void OnApplicationQuit()
    {
        Saving();
        SaveTest.Instance.SaveGameData();
    }

    public void Saving() 
    {
        SaveTest.Instance.data.isUnlock[0] = firstStage;
        SaveTest.Instance.data.isUnlock[1] = secondStage;
        SaveTest.Instance.data.isUnlock[2] = thridStage;


        SaveTest.Instance.data.vol = BgmVol;
        SaveTest.Instance.data.atk = Atk;
        SaveTest.Instance.data.speed = moveSpeed;

        SaveTest.Instance.data.dash = ondash;
        SaveTest.Instance.data.hover = onhover;
    }
    public void Loading() 
    {
        firstStage = SaveTest.Instance.data.isUnlock[0];
        secondStage = SaveTest.Instance.data.isUnlock[1];
        thridStage = SaveTest.Instance.data.isUnlock[2];


        BgmVol = SaveTest.Instance.data.vol;
        Atk = SaveTest.Instance.data.atk;
        moveSpeed = SaveTest.Instance.data.speed;

        ondash = SaveTest.Instance.data.dash;
        onhover = SaveTest.Instance.data.hover;
    }

}
