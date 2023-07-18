using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDamageIndicator : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI _enemyIndicatorText;
    private float _skeletonDamage = 5;              // Based skeleton script damage
    private float skeletonHealth = 5;
    [SerializeField] private string _preFix, _lastFix;
    private LevelController _levelController;
    private void OnEnable()
    {
        GameEventManager.OnChangedLevelEvent += UpdateText;
    }

    private void OnDisable()
    {
        GameEventManager.OnChangedLevelEvent -= UpdateText;
    }

    private void Awake()
    {
        _levelController = LevelController.Instance;
    }

    private void Start()
    {
        UpdateText(5); // 5 value is just placeholder for event
    }

    void UpdateText(int i)
    {
        var enemyBasedAttack =  _skeletonDamage;
        skeletonHealth = 5;
        if (LevelController.Instance.level > 1)
        {
            skeletonHealth += _levelController.level * _levelController.enemyHpMultiplier;
            enemyBasedAttack += _levelController.level * _levelController.enemyDamageMultiplier;
        }
        _enemyIndicatorText.text = _preFix + enemyBasedAttack + "   |   " + _lastFix + skeletonHealth;
    }
}
