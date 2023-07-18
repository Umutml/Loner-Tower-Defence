using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShowPanel : MonoBehaviour
{
    [SerializeField] Popup _popup;
    Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Show);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Show);
    }

    private void Show()
    {
        GameEventManager.ShowPanel(_popup);
    }
}
