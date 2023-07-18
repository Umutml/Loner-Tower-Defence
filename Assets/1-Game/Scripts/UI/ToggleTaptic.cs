using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ToggleTaptic : MonoBehaviour
{
    [SerializeField] RectTransform _toggle;
    [SerializeField] GameObject _textOn, _textOff, _bgOn, _bgOff;
   
    void Start()
    {
        SetToggle(Taptic.tapticOn);
        
    }

    public void Reverse()
    {
        SetToggle(!Taptic.tapticOn);
    }

    void SetToggle(bool isOn)
    {
        if (isOn)
        {
            _textOn.SetActive(true);
            _textOff.SetActive(false);
            _bgOn.SetActive(true);
            _bgOff.SetActive(false);
            _toggle.DOAnchorPosX(100, 0.2f);


        }
        else
        {
            _textOn.SetActive(false);
            _textOff.SetActive(true);
            _bgOff.SetActive(true);
            _bgOn.SetActive(false);
            _toggle.DOAnchorPosX(0, 0.2f);
        }
        Taptic.tapticOn = isOn;
    }
}
