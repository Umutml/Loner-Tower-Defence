using System;
using DG.Tweening;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] protected string _enemyName;
    [SerializeField] protected float _maxHealth;
    [SerializeField] protected float _currentHealth;
    [SerializeField] protected float _damage;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackSpeed;
    [SerializeField] protected float _destroyCooldown;
    [SerializeField] protected SkinnedMeshRenderer meshRenderer;
    public float levelMultiplier; // Enemy gain power multiplier
    private Color popupColor = new Color(1,0.5f,0,1);
    bool isDeath = false;
    float firstAttackSpeed;
    float firstMoveSpeed;

    public HealthController healthController;

    Animator _animator;

    protected bool _isAlive = true;
    
    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float CurrentHealth { get { return _currentHealth; } set { _currentHealth = value; } }
    public float MoveSpeed { 
        get 
        { 
            return _moveSpeed; 
        } 
        set 
        { 
            _moveSpeed = value;
            _animator.SetFloat("m_MoveSpeed", _moveSpeed);
        }
    }

    public float AttackSpeed
    {
        get
        {
            return _attackSpeed;
        }
        set
        {
            _attackSpeed = value;
            _animator.SetFloat("m_AttackSpeed", _attackSpeed);
        }
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetFloat("m_MoveSpeed", _moveSpeed);
        _animator.SetFloat("m_AttackSpeed", _attackSpeed);
        firstAttackSpeed = _attackSpeed;
        firstMoveSpeed = _moveSpeed;
    }

    private void OnEnable()
    {
        GameEventManager.OnGamePauseEvent += EnemyPause;
    }

    private void OnDisable()
    {
        GameEventManager.OnGamePauseEvent -= EnemyPause;
    }

    public virtual void TakeDamage(float value)
    {
        _currentHealth -= value;
        healthController.SetSliderValue(_currentHealth);
        GameEventManager.OnDamageTaken(this.gameObject,value,popupColor); // I Take damage spawn popup
        SoundManager.PlaySound(SoundManager.Sound.TakeDamageEnemy,new Vector2(1.5f,2.5f),0.2f);
        transform.DOShakePosition(0.1f,0.75f,5).SetEase(Ease.Linear);
        meshRenderer.materials[1].DOFade(0.75f, 0.15f).OnComplete(() =>
        {
            meshRenderer.materials[1].color = new Color(1, 0, 0, 0);
        });
        if (_currentHealth <= 0)
            OnDeath();
    }

    public virtual void Attack(float value)
    {
        if (LevelController.Instance.level > 1)
        {
            float damageCalculated = value + LevelController.Instance.level * levelMultiplier;      // Enemies gain power based gamelevel
            GameEventManager.OnEnemyAttacked(damageCalculated);
            Debug.Log(damageCalculated + " hitted " + name);
            return;
        }
        GameEventManager.OnEnemyAttacked(value);
    }


    public virtual void OnDeath()
    {
        if (isDeath)
        {
            return;
        }
        isDeath = true;
        GameEventManager.OnDeathEnemy(this);
        GameEventManager.OnExpEarned(this.gameObject,AbilityController.Instance.GetTotalValue(Enum_Abilities.GoldPerEnemy));
        _isAlive = false;
        this.GetComponent<Animator>().SetBool("isAttack", false);
        this.GetComponent<Animator>().SetBool("isDeath", true);
        this.transform.DOMoveY(-7f, 4f).OnComplete(() => Destroy(this.gameObject)).SetDelay(_destroyCooldown);
        RemoveMaterials();


    }

    void RemoveMaterials()
    {
        Material[] mats =
        {
                    meshRenderer.materials[0],
                    meshRenderer.materials[1]
         };
        meshRenderer.materials = mats;
    }

    void EnemyPause(bool value)
    {
        if (!value)
        {
            AttackSpeed = firstAttackSpeed;
            MoveSpeed = firstMoveSpeed;
        }
        else
        {
            MoveSpeed = 0;
            AttackSpeed = 0;
        }
    }

}