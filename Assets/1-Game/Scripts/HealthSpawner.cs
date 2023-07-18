using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] GameObject _healthPrefab;

    private void OnEnable()
    {
        GameEventManager.OnSpawnEnemyEvent += OnSpawn;
    }

    private void OnDisable()
    {
        GameEventManager.OnSpawnEnemyEvent -= OnSpawn;
    }
    void OnSpawn(Enemy enemy)
    {
        HealthController healthPrefab = GameObject.Instantiate(_healthPrefab, enemy.transform.position,Quaternion.identity,this.transform).GetComponent<HealthController>();
        healthPrefab.TargetTransform = enemy.transform;
        enemy.healthController = healthPrefab;
        healthPrefab.MaxHealth = enemy.MaxHealth;
    }
}
