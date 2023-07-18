using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Popup_Upgrade : MonoBehaviour
{
    [SerializeField] Image _dimed;
    [SerializeField] Transform _popupTransform;
    [SerializeField] Button _closeButton;
    private void OnEnable()
    {
        Open();
        _closeButton.onClick.AddListener(Close);
        GameEventManager.OnGamePause(true);

    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
    }

    public void Open()
    {
        _dimed.DOFade(0, 0.1f).From().SetUpdate(true);
        _popupTransform.DOScale(0, 0.35f).From().SetUpdate(true);

    }

    public 
        void Close()
    {
        GameEventManager.OnGamePause(false);
        Destroy(this.gameObject);


    }
}
