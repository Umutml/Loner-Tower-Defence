using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject damagePopupPrefab;
    [SerializeField] private GameObject expPopupPrefab;
    
    
    private void OnEnable()
    {
        GameEventManager.OnDamageTakenEvent += SpawnPopup;
        GameEventManager.OnExpEarnedEvent += SpawnExpEarn;
    }

    private void OnDisable()
    {
        GameEventManager.OnDamageTakenEvent -= SpawnPopup;
        GameEventManager.OnExpEarnedEvent -= SpawnExpEarn;
    }

    public void SpawnPopup(GameObject go, float value,Color color)
    {
        damagePopupPrefab.GetComponent<TextMeshPro>().color = color;
        var clonePopup = Instantiate(damagePopupPrefab, go.transform.position + Vector3.up*10, Quaternion.identity,this.transform);
        clonePopup.GetComponent<DamagePopup>().SetDamageText(value.ToString("0.#"));

    }

    public void SpawnExpEarn(GameObject go, float value)
    {
        var clonePopup = Instantiate(expPopupPrefab, go.transform.position + Vector3.up*14, Quaternion.identity,this.transform);
        clonePopup.GetComponent<DamagePopup>().SetDamageText("<sprite=46>" + value.ToString("0.#"));
    }
}
