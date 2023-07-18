using System;
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 _radiusRange;
    [SerializeField] private float _spawnTime;
    [SerializeField] private EnemyChance[] enemies;
    [SerializeField] private Transform enemyTransformParent;
    private float _time;
    bool _pause;
    private LevelController _levelController;

    
    
    private void OnEnable()
    {
        GameEventManager.OnGamePauseEvent += PauseTime;
        GameEventManager.OnChangedLevelEvent += SpawnTimeDecrease;
        
    }

    private void OnDisable()
    {
        GameEventManager.OnGamePauseEvent -= PauseTime;
        GameEventManager.OnChangedLevelEvent -= SpawnTimeDecrease;

    }

    void PauseTime(bool value)
    {
        _pause = value;
    }

    private void Start()
    {
        CreateNullEnemyObject();
        _levelController = LevelController.Instance;
        
    }

    private void Update()
    {
        Timer();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _radiusRange.x);
        Gizmos.DrawWireSphere(transform.position, _radiusRange.y);
    }

    private void CreateNullEnemyObject()
    {
        var go = new GameObject();
        go.name = "EnemyList";
        go.transform.SetSiblingIndex(transform.GetSiblingIndex() + 1);
        enemyTransformParent = go.transform;
    }

    private void Timer()
    {
        if (!_pause)
        {
            _time += Time.deltaTime;
            if (_time > _spawnTime)
            {
                Spawn();
                _time = 0;
            }
        }
        
    }

    private void Spawn()
    {
        Enemy enemy = Instantiate(GetRandomEnemy().gameObject, RandomPosition(), Quaternion.identity, enemyTransformParent).GetComponent<Enemy>();
        //float defaultSpeed = enemy.MoveSpeed;
        //enemy.MoveSpeed = 0;
        enemy.transform.DOScale(0, 0.3f).From().OnComplete(()=>
        {
            //enemy.GetComponent<Enemy>().MoveSpeed = defaultSpeed;
        });
        if (LevelController.Instance.level > 1) // After level 1 scale enemies hp * levelmultiplier
        {
            enemy.MaxHealth += _levelController.level * _levelController.enemyHpMultiplier;
            enemy.CurrentHealth = enemy.MaxHealth;
            enemy.levelMultiplier = _levelController.enemyDamageMultiplier;
        }
        GameEventManager.OnSpawnEnemy(enemy.GetComponent<Enemy>());
    }

    private Vector3 RandomPosition()
    {
        var randomPos = Random.insideUnitCircle.normalized * Random.Range(_radiusRange.x, _radiusRange.y);
        var pos = new Vector3(randomPos.x, 0, randomPos.y);
        return pos;
    }

    Enemy GetRandomEnemy()
    {
        Dictionary<int,Enemy> chances = new Dictionary<int, Enemy>();
        int nextChance = 0;
        for(int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].minLevel <= _levelController.level)
            {
                nextChance += enemies[i].chance;
                chances.Add(nextChance, enemies[i].enemy);
            }
        }
        if (chances.Count == 0)
        {
            Debug.LogError("Eklenebilecek dusman yok!");
            return enemies[0].enemy;
        }
        int r = Random.Range(0, nextChance+1);
        foreach (KeyValuePair<int, Enemy> veri in chances)
        {
            if (veri.Key >= r)
            {
                return veri.Value;
            }
        }
        return null;
    }

    private void SpawnTimeDecrease(int i)
    {
        if (_spawnTime > 0.5f)
        {
            _spawnTime -= 0.075f;
        }
    }
}
