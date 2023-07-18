using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

public class Ghost : Enemy
{
    [SerializeField] [ReadOnly] private Transform _player;
    [SerializeField] private float _distance;
    private bool _move = true;


    void Start()
    {
        _player = Player.Instance.transform;
    }

    void Update()
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
        _move = false;
        SoundManager.PlaySound(SoundManager.Sound.GhostDeath,new Vector2(1.5f,2.5f),0.03f);
        this.transform.DOScale(Vector3.zero, 4.5f).SetDelay(0.8f);
        transform.DOMoveY(-6f, 2f).OnComplete(() => Destroy(this.gameObject)).SetDelay(0.5f);
    }
    private void Movement()
    {
        Look();
        Move();
    }
    
    
    private void Move()
    {
        // Ghost dont have root motion need move method
        if (_move)
        {
            var speed = _moveSpeed * Time.deltaTime;
            var playerVector = _player.position;
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(playerVector.x, transform.position.y, playerVector.z), speed * Time.timeScale);
        }
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
            _move = false;
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
