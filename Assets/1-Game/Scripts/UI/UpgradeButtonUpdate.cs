using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeButtonUpdate : MonoBehaviour
{
    [SerializeField] Enum_Abilities _ability;
    [SerializeField] TextMeshProUGUI _AbilityNameText;
    [SerializeField] TextMeshProUGUI _descriptionText;
    [SerializeField] TextMeshProUGUI _priceText;
    [SerializeField] CanvasGroup _canvasGroup;
    AbilityController _abilityController;
    GameManager _gameManager;
    Button _button;
    float _price;

    private void Awake()
    {
        _button = _canvasGroup.GetComponent<Button>();
        _abilityController = AbilityController.Instance;
        _gameManager = GameManager.Instance;
    }
    private void OnEnable()
    {
        PriceUpdate();
        _button.onClick.AddListener(Buy);
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            GameEventManager.OnChangedGoldEvent += UpdateButton;
            UpdateButton(_gameManager.BaseGold);
        }
        else
        {
            GameEventManager.OnChangedGameEXPEvent += UpdateButton;
            UpdateButton(_gameManager.GameEXP);

        }
        _descriptionText.text = _abilityController.GetStats(_ability);

    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            GameEventManager.OnChangedGoldEvent -= UpdateButton;
        }
        else
        {
            GameEventManager.OnChangedGameEXPEvent -= UpdateButton;
        }
        _button.onClick.RemoveListener(Buy);

    }

    void UpdateButton(float value)
    {
        if(value < _price)
        {
            _canvasGroup.interactable = false;
            _canvasGroup.alpha = 0.5f;
        }
        else
        {
            _canvasGroup.interactable = true;
            _canvasGroup.alpha = 1f;

        }
    }

    void Buy()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (_gameManager.BaseGold >= _price)
            {
                _abilityController.UpgradeBaseAbility(_ability);
                _gameManager.BaseGold -= _price;
                SoundManager.PlaySound(SoundManager.Sound.Upgrade,0.2f);
                _descriptionText.text = _abilityController.GetStats(_ability);
                PriceUpdate();
            }
            else
            {
                Debug.Log("Error..");
            }
        }
        else
        {
            if (_gameManager.GameEXP >= _price)
            {
                _abilityController.UpgradeGameAbility(_ability);
                _gameManager.GameEXP -= _price;
                SoundManager.PlaySound(SoundManager.Sound.Upgrade,0.2f);
                _descriptionText.text = _abilityController.GetStats(_ability);
                PriceUpdate();
            }
            else
            {
                Debug.Log("Error..");
            }
        }
    }

    void PriceUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            _price = AbilityController.Instance.GetPriceMenu(_ability);
            _priceText.text = "Upgrade<sprite=24> " + _price.ToString("0");

        }
        else
        {
            _price = AbilityController.Instance.GetPrice(_ability);
            _priceText.text = "Upgrade<sprite=46> " + _price.ToString("0");

        }
    }
}
