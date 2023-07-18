using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;
    [SerializeField] int _requiredEXP;
    [SerializeField] [ReadOnly] float _EXP;
    public int level { get; private set; }
    public float enemyHpMultiplier = 0.25f;
    public float enemyDamageMultiplier = 1.1f;
    [SerializeField] Slider _levelSlider;
    [SerializeField] CanvasGroup _stageCanvasGroup;
    [SerializeField] TextMeshProUGUI _currentLevelText, _nextLevelText, _stageTitleText;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    void OnEnable()
    {
        level = 1;
        _levelSlider.maxValue = _requiredEXP;
        _levelSlider.value = _EXP;
        _currentLevelText.text = level.ToString();
        _nextLevelText.text = (level+1).ToString();
        GameEventManager.OnDeathEnemyEvent += AddEXP;
    }

    private void OnDisable()
    {
        GameEventManager.OnDeathEnemyEvent -= AddEXP;
    }
    

    public float EXP
    {
        set
        {
            if (value <= 0)
            {
                return;
            }
            _EXP = value;
            if (_EXP >= _requiredEXP)
            {
                _EXP = _EXP - _requiredEXP;
                Level=level+1;
            }
            _levelSlider.DOValue(_EXP, 0.25f);
        }
    }

    public int Level
    {
        set
        {
            level = value;
            _currentLevelText.text = level.ToString();
            _nextLevelText.text = (level+1).ToString();
            _stageTitleText.text = string.Format("Level {0}", level);
            // Sequance
            Sequence sequance = DOTween.Sequence();
            sequance.Append(_stageCanvasGroup.DOFade(1, 0.75f));
            sequance.AppendInterval(2f);
            sequance.Append(_stageCanvasGroup.DOFade(0, 0.25f));
            // Sequance End
            GameEventManager.OnChangedLevel(level);
            if(GameManager.Instance.HighScore < level)
            {
                GameManager.Instance.HighScore = level;
            }
        }
    }


    void AddEXP(Enemy e)
    {
        EXP = _EXP + 1;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.PageUp))
        {
            EXP = _EXP+1;
        }

        if (Input.GetKeyDown(KeyCode.PageDown))
        {
            EXP = _EXP - 1;
        }
    }


}
