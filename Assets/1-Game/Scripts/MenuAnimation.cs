using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class MenuAnimation : MonoBehaviour
{
    [SerializeField] GameObject _camera, _settings, _status, _upgrade, _playbutton;

    
    private void Start()
    {
        Sequence seq = DOTween.Sequence();
        if (GameManager.Instance.FirstOpening)
        {
            _camera.transform.DOMoveY(54, 0);
            seq.Append(_camera.transform.DOMoveY(12, 7)).OnComplete(()=>
            {
                GameManager.Instance.FirstOpening = false;
            });
        }
        else
        {
            _camera.transform.DOMoveY(12, 0);
        }
        seq.Append(_settings.transform.DOScale(0, 0.2f).From());
        seq.Append(_status.transform.DOScale(0, 0.2f).From());
        seq.Append(_upgrade.transform.DOScale(0, 0.2f).From());
        seq.Append(_playbutton.transform.DOScale(0, 0.2f).From());



    }
}
