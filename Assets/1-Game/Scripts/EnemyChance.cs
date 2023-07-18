using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EnemyChance
{
    public Enemy enemy;
    [Range(0,100)] public int chance;
    public int minLevel = 0;
}
