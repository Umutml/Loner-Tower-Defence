using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
[RequireComponent(typeof(TextMeshProUGUI))]
public class GetEXPValue : MonoBehaviour
{
    [SerializeField] string _prefix, _lastfix;
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void OnEnable()
    {
        GameEventManager.OnChangedGameEXPEvent += UpdateText;
        UpdateText(GameManager.Instance.GameEXP);
    }

    private void OnDisable()
    {
        GameEventManager.OnChangedGameEXPEvent -= UpdateText;

    }

    void UpdateText(float value)
    {
        _text.transform.DOScale(1.3f, 0.2f).SetLoops(2, LoopType.Yoyo);
        _text.text = _prefix + value.ToString("0") + _lastfix;
    }
}
