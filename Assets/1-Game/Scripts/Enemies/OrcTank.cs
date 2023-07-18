using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class OrcTank : Enemy
{
    [SerializeField] [ReadOnly] private Transform _player;
    [SerializeField] private float _distance;
    

    private void Start()
    {
        _player = Player.Instance.transform;
    }

    private void Update()
    {
        Movement();
        DistanceCheck();
    }

    public override void TakeDamage(float value)
    {
        base.TakeDamage(value);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        SoundManager.PlaySound(SoundManager.Sound.OrcDeath,new Vector2(1f,1.5f),0.15f);
    }
    
    private void Movement()
    {
        Look();
    }

    private void Look()
    {
        var lookPos = _player.transform.position;
        lookPos.y = transform.position.y;
        transform.LookAt(lookPos);
    }

    private void DistanceCheck()
    {
        var _playerPos = _player.position;
        _playerPos.y = transform.position.y;
        if (Vector3.Distance(transform.position, _playerPos) < _distance)
        {
            this.GetComponent<Animator>().SetBool("isAttack",true);
        }
    }
  
    
    private void AttackTrigger()
    {
        if (_isAlive)
        {
            Attack(_damage);
        }
    }
}