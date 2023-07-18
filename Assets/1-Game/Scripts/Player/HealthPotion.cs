using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthPotion : MonoBehaviour
{
    Button _button;
    private int potionAmount = 3;
    [SerializeField] private TextMeshProUGUI potionAmountText;
    [SerializeField] GameObject _adsObject;
    bool _isWatch = false;
    private void Awake()
    {
        _button = GetComponent<Button>();
    }
    
    private void OnEnable()
    {
        _button.onClick.AddListener(UsePotion);
        potionAmountText.text = "x" + potionAmount;
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(UsePotion);
    }

    private void UsePotion()
    {
        if(Player.Instance.CurrentHealth == AbilityController.Instance.GetTotalValue(Enum_Abilities.Health))
        {
            return;
        }
        if (potionAmount == 0)
        {
            Advertisements.Instance.ShowRewardedVideo(RewardedComplete);
        }
        else
        {
            potionAmount--;
            potionAmountText.text = "x" + potionAmount;
            SoundManager.PlaySound(SoundManager.Sound.HPPotion, new Vector2(2f, 3f), 0.25f);
            GameEventManager.OnPotionUsed();
            if (potionAmount == 0 && !_isWatch)
            {
                _adsObject.SetActive(true);
            }
            else if (potionAmount == 0 && _isWatch)
            {
                _button.interactable = false;
            }
            else
            {
                
                _adsObject.SetActive(false);
            }
        }
    }

    void RewardedComplete(bool completed)
    {
        if (completed)
        {
            _adsObject.SetActive(false);
            potionAmount = 3;
            UsePotion();
            _isWatch = true;

        }

    }



    
}
