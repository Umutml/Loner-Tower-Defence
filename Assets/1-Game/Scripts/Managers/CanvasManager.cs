using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] GameObject _popupUpgrade;
    [SerializeField] GameObject _popupUpgradeMenu;
    [SerializeField] GameObject _popupScore;
    [SerializeField] GameObject _popupSettings;
    [SerializeField] GameObject _popupSettingsContinue;
    [SerializeField] GameObject _popupSettingsMenu;


    private void OnEnable()
    {
        GameEventManager.ShowPanelEvent += Show;
    }

    private void OnDisable()
    {
        GameEventManager.ShowPanelEvent -= Show;
    }

    void Show(Popup popup)
    {
        GameObject _gameobject;
        switch (popup)
        {
            case Popup.UpgradeGame:
                _gameobject = GameObject.Instantiate(_popupUpgrade, this.transform);
                SoundManager.PlaySound(SoundManager.Sound.UpgradePanel,new Vector2(1f,2f),0.2f);
                _gameobject.SetActive(true);
                break;
            case Popup.UpgradeMenu:
                _gameobject = GameObject.Instantiate(_popupUpgradeMenu, this.transform);
                SoundManager.PlaySound(SoundManager.Sound.UpgradePanel,new Vector2(1f,2f),0.2f);
                _gameobject.SetActive(true);
                break;
            case Popup.ScorePanel:
                _gameobject = GameObject.Instantiate(_popupScore, this.transform);
                SoundManager.PlaySound(SoundManager.Sound.ScorePanel,0.2f);
                _gameobject.SetActive(true);
                break;
            case Popup.Settings:
                _gameobject = GameObject.Instantiate(_popupSettings, this.transform);
                SoundManager.PlaySound(SoundManager.Sound.UpgradePanel,new Vector2(1f,2f),0.2f);
                _gameobject.SetActive(true);
                break;
            case Popup.SettingsContinue:
                _gameobject = GameObject.Instantiate(_popupSettingsContinue, this.transform);
                SoundManager.PlaySound(SoundManager.Sound.UpgradePanel,new Vector2(1f,2f),0.2f);
                _gameobject.SetActive(true);
                break;
            case Popup.SettingsMenu:
                _gameobject = GameObject.Instantiate(_popupSettingsMenu, this.transform);
                SoundManager.PlaySound(SoundManager.Sound.UpgradePanel, new Vector2(1f, 2f), 0.2f);
                _gameobject.SetActive(true);
                break;
        }
    }


}


public enum Popup{
    UpgradeGame,
    UpgradeMenu,
    Settings,
    SettingsContinue,
    ScorePanel,
    SettingsMenu
}