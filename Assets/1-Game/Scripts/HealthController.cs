using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    Camera _cam;
    [ReadOnly] [SerializeField]Transform _targetTransform;
    [SerializeField] private Image healthSlider,effectSlider;
    [SerializeField] private float maxHealth;
    private Tween hpTween;
    public Transform TargetTransform { get { return _targetTransform; } set { _targetTransform = value; } }
    public float MaxHealth { get { return maxHealth; } set { maxHealth = value; } }

    private void Awake()
    {
        
        _cam = Camera.main;
    }

    private void Update()
    {
        if (_targetTransform == null)
        {
            Destroy(this.gameObject);
            return;
        }
        Vector3 pos = _targetTransform.position;
        pos.y += 10;
        transform.position = pos;
        transform.LookAt(1 * transform.position - _cam.transform.position);

    }

    public void SetSliderValue(float value)
    {
        
        healthSlider.fillAmount = value/maxHealth;
        hpTween = effectSlider.DOFillAmount(healthSlider.fillAmount, 0.5f).OnComplete(() =>
        {
            if (effectSlider.fillAmount == 0)
            {
                Destroy(this.gameObject);
            }
        });

    }

    public void RemoveSliderValue(float value)
    {
        healthSlider.fillAmount -= value/maxHealth;
        hpTween = effectSlider.DOFillAmount( healthSlider.fillAmount,0.5f);
    }

    public void AddSliderValue(float value)
    {
        healthSlider.fillAmount += value/maxHealth;
    }
    
    

}
