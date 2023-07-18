using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeController : MonoBehaviour
{
    public static RangeController Instance;
    
    [SerializeField] float radiusRange;
    [SerializeField] Transform particleZone;
    float rangeMultiplier = 6.64f;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiusRange*rangeMultiplier);
    }

    private void SetRadius()
    {
        
        particleZone.localScale = Vector3.one * radiusRange;
    }

    public float RadiusRange
    {
        get
        {
            return radiusRange * rangeMultiplier;
        }
        set
        {
            radiusRange = value;
            SetRadius();
        }
    }

}
