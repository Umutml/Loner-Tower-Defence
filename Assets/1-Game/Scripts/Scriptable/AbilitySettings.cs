using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AbilitySettings", menuName = "Scriptable/AbilitySettings")]
public class AbilitySettings : ScriptableObject
{
    public List<AbilityValues> abilityValues = new List<AbilityValues>();
}

[System.Serializable]
public class AbilityValues
{
    public Enum_Abilities ability;
    public float firstValue;
    public float multiplier;
}