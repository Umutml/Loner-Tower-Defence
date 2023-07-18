using System;
using DG.Tweening;
using UnityEngine;

public class CameraZoomOut : MonoBehaviour
{
    [SerializeField] private float rangeRadiusMultiplier;
    private Camera _camera;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Start()
    {
        ScaleUp(AbilityController.Instance.TotalAbilities.range);
    }

    private void OnEnable()
    {
        GameEventManager.OnRangeUpgradeEvent += ScaleUp;
    }

    private void OnDisable()
    {
        GameEventManager.OnRangeUpgradeEvent -= ScaleUp;
    }

    private void ScaleUp(float value)
    {
        //_camera.orthographicSize += (value * rangeRadiusMultiplier);
        _camera.DOOrthoSize(_camera.orthographicSize + (value * rangeRadiusMultiplier),0.6f); // Smooth transition camera orthosize
    }


}