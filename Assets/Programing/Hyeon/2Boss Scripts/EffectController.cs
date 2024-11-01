using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] float destorytime;

    private void Start()
    {
        Destroy(gameObject, destorytime);
    }
}
