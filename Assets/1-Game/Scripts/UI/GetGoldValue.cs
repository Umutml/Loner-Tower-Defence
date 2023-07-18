using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class GetGoldValue : MonoBehaviour
{
    [SerializeField] string _prefix, _lastfix;
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameEventManager.OnChangedGoldEvent += UpdateText;
        UpdateText(GameManager.Instance.GameGold);

    }

    private void OnDisable()
    {
        GameEventManager.OnChangedGoldEvent -= UpdateText;

    }

    void UpdateText(float value)
    {
        _text.transform.DOScale(1.3f, 0.2f).SetLoops(2, LoopType.Yoyo);
        _text.text = _prefix + value.ToString("0") + _lastfix;
    }
}
