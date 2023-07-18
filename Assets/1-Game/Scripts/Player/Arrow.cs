using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
using Random = UnityEngine.Random;

public class Arrow : MonoBehaviour
{
    [ReadOnly] public float arrowSpeed;
    private float firstArrowSpeed;
    Vector3 targetPos;
    [SerializeField] private float arrowFinalScale;
    private Enemy targetEnemy;
    [SerializeField] private GameObject[] hitParticles;

    private float lookTimer;
    private float lookCooldown = 0.2f;
    private AbilityController abilityController;

    private void Awake()
    {
        abilityController = AbilityController.Instance;
        
    }

    private void OnEnable()
    {
        firstArrowSpeed = arrowSpeed;
        targetEnemy = Player.Instance.targetControl.targetedEnemy;
        GameEventManager.OnGamePauseEvent += PauseArrow;
        if (targetEnemy == null)
        {
            ArrowPool.Instance.ReturnToPool(this);
        }
        else
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(arrowFinalScale, 0.25f);
        }
        
    }

    private void OnDisable()
    {
        GameEventManager.OnGamePauseEvent -= PauseArrow;
    }

    private void Update()
    {
        if (targetEnemy != null)
        {
            targetPos = targetEnemy.transform.position;
        }
        MoveTarget();
        DistanceCheck();
        LookEnemy();
    }

    private void LookEnemy()
    {
        lookTimer += Time.deltaTime;
        if (lookTimer > lookCooldown)
        {
            lookTimer = 0;
            transform.LookAt(targetPos + new Vector3(0,2f,0));
        }
    }
    

    private void MoveTarget()
    {
        float step = arrowSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPos + new Vector3(0,2f,0), step);
        
    }

    private void DistanceCheck()
    {
        if (Vector3.Distance(transform.position, targetPos + new Vector3(0,2f,0)) < 0.1f)
        {
            if (targetEnemy != null && targetEnemy.CurrentHealth > 0)
            {
                targetEnemy.TakeDamage(abilityController.GetTotalValue(Enum_Abilities.Damage));
                int rndParticle = Random.Range(0, hitParticles.Length);
                Instantiate(hitParticles[rndParticle], targetEnemy.transform.position + Vector3.up*5, quaternion.identity);
            }
            ArrowPool.Instance.ReturnToPool(this);
        }
    }

    private void PauseArrow(bool value)
    {
        if (!value)
        {
            arrowSpeed = firstArrowSpeed;
        }
        else
        {
            arrowSpeed = 0;
        }
    }
}
