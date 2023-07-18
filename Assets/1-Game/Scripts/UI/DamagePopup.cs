using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DamagePopup : MonoBehaviour
{
    private TextMeshPro damageText;
    [SerializeField]float lifeTime = 1f;
    private Transform cam;


    private void Awake()
    {
        cam = Camera.main.transform;
        damageText = GetComponent<TextMeshPro>();
    }

    void Start()
    {
        transform.LookAt(1 * transform.position - cam.transform.position);
        StartTween();
    }

    private void StartTween()
    {
        damageText.DOFade(0, lifeTime);
        transform.DOMoveY(transform.position.y + 5, lifeTime).OnComplete(() =>
        {
            Destroy(this.gameObject);
        });
    }

    public void SetDamageText(string damage)
    {
        //int convInt = Mathf.RoundToInt(damage);
        damageText.text = damage;
        //damageText.text = damage.ToString("0.#");
    }

}
