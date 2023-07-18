using DG.Tweening;
using System;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField] private float currentHealth;
    [SerializeField] private float damage = 10;
    [SerializeField] private float potionHealValue;
    [SerializeField] private Transform towerTransform;
    public Animator playerAnimator;
    public TargetControl targetControl;
    private AudioSource _audioSource;
    [SerializeField] private GameObject takeDamageParticle, healPotionParticle,towerDestroyParticle;
    [SerializeField] private AudioClip[] audioClips;
    private float _regenerationRate;
    private float _regenTimer;
    private bool _isDeath = false;
    private float _firstAttackSpeed;


    public float AttackSpeed
    {
        get
        { 
            return playerAnimator.GetFloat("AttackSpeed");

        }
        set
        {
            playerAnimator.SetFloat("AttackSpeed",value);
        }
    }

    public float CurrentHealth
    {
        get
        {
            return currentHealth;

        }
        set
        {
            currentHealth = value;
            GameEventManager.OnChangedHP(currentHealth);
        }
    }

    private void Awake()
    {
        Instance = this;

        
        playerAnimator = GetComponent<Animator>();
        targetControl = GetComponent<TargetControl>();
        _audioSource = GetComponent<AudioSource>();
        CurrentHealth = AbilityController.Instance.GetTotalValue(Enum_Abilities.Health);
        GameEventManager.OnHealthUpgrade(CurrentHealth);
        //_firstAttackSpeed = AttackSpeed;
    }

    private void Update()
    {
        HealthRegeneration();
    }

    private void OnEnable()
    {
        GameEventManager.OnEnemyAttackedEvent += TakeDamage;
        GameEventManager.OnPotionUsedEvent += UsePotion;
        GameEventManager.OnGamePauseEvent += PlayerPause;
    }

    private void OnDisable()
    {
        GameEventManager.OnEnemyAttackedEvent -= TakeDamage;
        GameEventManager.OnPotionUsedEvent -= UsePotion;
        GameEventManager.OnGamePauseEvent -= PlayerPause;
    }

    private void UsePotion()
    {
        var maxHealth = AbilityController.Instance.GetTotalValue(Enum_Abilities.Health);
        currentHealth = Mathf.Clamp(currentHealth + potionHealValue,0, maxHealth);
        GameEventManager.OnChangedHP(currentHealth);
        Instantiate(healPotionParticle, transform.position+ Vector3.up*2, quaternion.Euler(-90,0,0), this.transform);
    }
    
    public void TakeDamage(float value)
    {
        if (!_isDeath)
        {
            CurrentHealth -= value;
            GameEventManager.OnDamageTaken(this.gameObject, value, Color.red);
            SoundManager.PlaySound(SoundManager.Sound.PlayerTakeDamage, new Vector2(1f,1.25f),0.1f);
            SoundManager.PlaySound(SoundManager.Sound.TowerTakeHit,new Vector2(1,1.5f),0.1f);
            Instantiate(takeDamageParticle, transform.position + Vector3.up*2, Quaternion.identity,this.transform);
            if (currentHealth <= 0)
            {
                Taptic.Warning();
                OnDeath();
            }
            else
            {
                Taptic.Light();
            }
        }
        
    }
    
    public void Attack()                        // AttackAnim event trigger method
    {
        if (targetControl.targetedEnemy != null)
        {
            AttackSound(new Vector2(2,2.5f),0.1f);
            //SoundManager.PlaySound(SoundManager.Sound.PlayerAttack,new Vector2(3f,3.5f),0.05f);
            ArrowPool.Instance.Get();
        }
    }
    private void HealthRegeneration()
    {
        if (!_isDeath)
        {
            _regenTimer += Time.deltaTime;
            _regenerationRate = AbilityController.Instance.GetTotalValue(Enum_Abilities.HealthRegeneration);
            var maxHealth = AbilityController.Instance.GetTotalValue(Enum_Abilities.Health);

            if (_regenTimer >= 1 / _regenerationRate && currentHealth < maxHealth)
            {
                _regenTimer = 0;
                currentHealth += 0.1f;

                if (currentHealth > maxHealth)
                    currentHealth = maxHealth;

                GameEventManager.OnChangedHP(currentHealth);
            }
        }
        
    }

    private void AttackSound(Vector2 pitch, float volume)
    {
        _audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
        _audioSource.pitch = Random.Range(pitch.x, pitch.y);
        _audioSource.volume = volume;
        _audioSource.PlayOneShot(_audioSource.clip);
        
    }

    public void OnDeath()
    {
        _isDeath = true;
        AttackSpeed = 0;
        currentHealth = 0;
        GameEventManager.OnDeathPlayer();
        GameEventManager.ShowPanel(Popup.ScorePanel);
        GameEventManager.OnGamePause(true);
        TowerDestroy();
    }

    void TowerDestroy()
    {
        var towerParticle = Instantiate(towerDestroyParticle, towerTransform.position + Vector3.up*5,Quaternion.Euler(-90,0,0));
        towerParticle.transform.DOScale(Vector3.one * 10, 3f);
        transform.DOMoveY(transform.position.y-15, 2);
        towerTransform.DOMoveY(towerTransform.position.y - 15, 2);
        SoundManager.PlaySound(SoundManager.Sound.CastleDestroy);
    }

    void PlayerPause(bool value)
    {
            if (!value)
            {
                AttackSpeed = AbilityController.Instance.GetTotalValue(Enum_Abilities.AttackSpeed);
                _regenTimer = 0;
            }
            else
            {
                AttackSpeed = 0;
                _regenTimer = -500;
            }
    }
}