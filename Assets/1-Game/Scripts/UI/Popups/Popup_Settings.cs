using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Popup_Settings : MonoBehaviour
{
    [SerializeField] Image _dimed;
    [SerializeField] Transform _popupTransform;
    [SerializeField] Button _closeButton;
    [SerializeField] Button _quitButton;

    private void OnEnable()
    {
        Open();
        _closeButton.onClick.AddListener(Close);
        _quitButton.onClick.AddListener(Quit);
        GameEventManager.OnGamePause(true);
    }

    private void OnDisable()
    {
        _closeButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();

    }

    public void Open()
    {
        _dimed.DOFade(0, 0.1f).From().SetUpdate(true);
        _popupTransform.DOScale(0, 0.35f).From().SetUpdate(true);

    }

    private void Quit()
    {
        Destroy(this.gameObject);
        GameEventManager.ShowPanel(Popup.SettingsContinue);   
    }

    public void Close()
    {
        GameEventManager.OnGamePause(false);
        Destroy(this.gameObject);


    }
}
