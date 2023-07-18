using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController : MonoBehaviour
{
    private void OnEnable()
    {
        GameEventManager.OnDeathEnemyEvent += OutlineOff;
        GameEventManager.OnChangedTargetEvent += OutlineOn;
    }

    private void OnDisable()
    {
        GameEventManager.OnDeathEnemyEvent -= OutlineOff;
        GameEventManager.OnChangedTargetEvent -= OutlineOn;

    }

    void OutlineOn(Enemy enemy)
    {
        enemy.GetComponent<Outline>().enabled=true;
    }

    void OutlineOff(Enemy enemy)
    {
        enemy.GetComponent<Outline>().enabled = false;
    }
}
