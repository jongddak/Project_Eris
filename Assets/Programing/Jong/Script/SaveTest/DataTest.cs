using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    // 각 챕터의 잠금여부를 저장할 배열
    public bool[] isUnlock = new bool[5];

    public float vol;
    public float atk;
    public float speed;

    public bool dash;
    public bool hover;
}

