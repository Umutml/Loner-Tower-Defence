using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using UnityEngine.SceneManagement;

public class Popup_Score : MonoBehaviour
{
    [SerializeField] Image _dimed;
    [SerializeField] Transform _popupTransform;
    [SerializeField] Button _button, _rewardedButton;
    [SerializeField] TextMeshProUGUI _goldCountText;
    AsyncOperation asyncLoad = null;
    private void OnEnable()
    {
        Open();
        _goldCountText.text = GameManager.Instance.GameGold.ToString("0");
        GameManager.Instance.BaseGold += GameManager.Instance.GameGold;
        StartCoroutine(LoadYourAsyncScene());

    }

 

    public void Open()
    {
        _dimed.DOFade(0, 0.1f).From().SetUpdate(true).SetDelay(1);
        _popupTransform.DOScale(0, 0.35f).From().SetUpdate(true).SetDelay(1);

    }

    public void WatchRewarded()
    {
        Advertisements.Instance.ShowRewardedVideo(RewardedComplete);
    }

    public void RewardedComplete(bool complete)
    {
        if (complete)
        {
            GameManager.Instance.BaseGold += GameManager.Instance.GameGold;
            asyncLoad.allowSceneActivation = true;
            Destroy(this.gameObject);
        }
        else
        {
            asyncLoad.allowSceneActivation = true;
            Destroy(this.gameObject);
        }
        
    }

    IEnumerator LoadYourAsyncScene()
    {
        asyncLoad = SceneManager.LoadSceneAsync("Menu");
        asyncLoad.allowSceneActivation = false;
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
