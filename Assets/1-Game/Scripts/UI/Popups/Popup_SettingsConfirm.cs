using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Popup_SettingsConfirm : MonoBehaviour
{
    [SerializeField] private Image _dimed;
    [SerializeField] private Transform _popupTransform;
    [SerializeField] private Button _quitButton;
    [SerializeField] private Button _continueButton;

    private void OnEnable()
    {
        Open();
        _continueButton.onClick.AddListener(Close);
        _quitButton.onClick.AddListener(Quit);
    }

    private void OnDisable()
    {
        _continueButton.onClick.RemoveAllListeners();
        _quitButton.onClick.RemoveAllListeners();
    }

    public void Open()
    {
        _dimed.DOFade(0, 0.1f).From().SetUpdate(true);
        _popupTransform.DOScale(0, 0.35f).From().SetUpdate(true);
    }


    public void Close()
        
    {
        GameEventManager.ShowPanel(Popup.Settings);
        Destroy(gameObject);
    }

    private void Quit()
    {
        SceneManager.LoadScene("Menu");
        Destroy(gameObject);
    }
}
