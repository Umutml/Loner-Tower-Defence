using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GetHPValue : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Slider _slider;
    float _currentHP, _maxHP;



    private void OnEnable()
    {
        GameEventManager.OnHealthUpgradeEvent += UpdateMaxHP;
        GameEventManager.OnChangedHPEvent += UpdateCurrentHP;
        UpdateMaxHP(AbilityController.Instance.GetTotalValue(Enum_Abilities.Health));
        UpdateCurrentHpFirst(Player.Instance.CurrentHealth);
    }

    private void OnDisable()
    {
        GameEventManager.OnHealthUpgradeEvent -= UpdateMaxHP;
        GameEventManager.OnChangedHPEvent -= UpdateCurrentHP;
    }

    void UpdateCurrentHP(float value)
    {
        _currentHP = value;
        _slider.DOValue(value, 0.2f);
        UpdateText();
    } 
    void UpdateCurrentHpFirst(float value)
    {
        _currentHP = value;
        _slider.value = value;
        UpdateText();
    }
 
    void UpdateMaxHP(float value)
    {
        _maxHP = value;
        _slider.maxValue = value;
        UpdateText();
    } 

    void UpdateText()
    {
        _text.text = _currentHP.ToString("0.#") + "/" + _maxHP.ToString("0.#");
    }
}
