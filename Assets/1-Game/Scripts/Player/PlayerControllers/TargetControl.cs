using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Animations;

public class TargetControl : MonoBehaviour
{
    [SerializeField] [ReadOnly] public Enemy targetedEnemy;
    [SerializeField] LookAtConstraint _lookAtConstraint;
    private readonly List<Enemy> targetList = new();
    private Animator playerAnimator;

    private void Awake()
    {
    }

    private void Start()
    {
        StartCoroutine(IEFindTarget());
        playerAnimator = GetComponent<Animator>();
    }



    private void OnEnable()
    {
        // PlayerRadiusCheck listener if radius changed from upgrade
        GameEventManager.OnSpawnEnemyEvent += AddEnemy;
        GameEventManager.OnDeathEnemyEvent += RemoveEnemy;
        GameEventManager.OnDeathEnemyEvent += FindTarget;
    }

    private void OnDisable()
    {
        // PlayerRadiusCheck listener if radius changed from upgrade
        GameEventManager.OnSpawnEnemyEvent -= AddEnemy;
        GameEventManager.OnDeathEnemyEvent -= RemoveEnemy;
        GameEventManager.OnDeathEnemyEvent -= FindTarget;
    }


    private void FindTarget(Enemy e)
    {
        if (targetList.Count > 0) 
        {
            foreach (var enemy in targetList)
            {
                if (Vector3.Distance(RangeController.Instance.transform.position, enemy.transform.position) > RangeController.Instance.RadiusRange)
                {
                    continue;
                }
                if (targetedEnemy == null)
                {
                    targetedEnemy = enemy;
                }
                else
                {
                    var targetDistance = Vector3.Distance(transform.position, targetedEnemy.transform.position);
                    var newDistance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (targetDistance > newDistance) targetedEnemy = enemy;
                }
            }
            if (targetedEnemy != null)
            {
                GameEventManager.OnChangedTarget(targetedEnemy);
                var cs = new ConstraintSource();
                cs.sourceTransform = targetedEnemy.transform;
                cs.weight = 1;
                _lookAtConstraint.SetSource(0, cs);
                playerAnimator.SetBool("Attack", true);
                transform.DOLookAt(targetedEnemy.transform.position, 0.5f, AxisConstraint.Y);
            }
            else
            {
                StartCoroutine(IEFindTarget());
                playerAnimator.SetBool("Attack", false);
            }
            
        }
        else
        {
            StartCoroutine(IEFindTarget());
            playerAnimator.SetBool("Attack",false);
        }
    }

    private IEnumerator IEFindTarget()
    {
        yield return new WaitForFixedUpdate();
        FindTarget(null);
    }

    private void AddEnemy(Enemy enemy)
    {
        targetList.Add(enemy);
    }

    private void RemoveEnemy(Enemy enemy)
    {
        targetList.Remove(enemy);
        if (enemy == targetedEnemy) targetedEnemy = null;
    }
}