using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilityController : MonoBehaviour
{
    public static AbilityController Instance { get; private set; }
    public Abilities TotalAbilities;
    public AbilityLevels gameAbilitiesLevel;
    public float firstPrice = 10f;
    [SerializeField] AbilitySettings abilitySettings;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            SceneManager.sceneLoaded += OnSceneLoaded;

        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ResetGameAbility();
        StartScene();
    }

    
    private void StartScene()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            UpdateAbilities(Enum_Abilities.Damage);
            UpdateAbilities(Enum_Abilities.AttackSpeed);
            UpdateAbilities(Enum_Abilities.Health);
            UpdateAbilities(Enum_Abilities.HealthRegeneration);
            UpdateAbilities(Enum_Abilities.Range);
            UpdateAbilities(Enum_Abilities.GoldPerEnemy);
            GameManager.Instance.GameGold = 0;
            GameManager.Instance.GameEXP = 0;
        }
        
    }

    void ResetGameAbility()
    {
        gameAbilitiesLevel.damage = 0;
        gameAbilitiesLevel.attackSpeed = 0;
        gameAbilitiesLevel.health = 0;
        gameAbilitiesLevel.healthRegeneration = 0;
        gameAbilitiesLevel.range = 0;
        gameAbilitiesLevel.goldPerEnemy = 0;
    }

    public void UpgradeBaseAbility(Enum_Abilities ability)
    {
        int lvl = GetBaseAbilityLevel(ability) + 1;
        ES3.Save<int>("base_" + ability.ToString(), lvl);
        UpdateAbilities(ability);
    }

    public void UpgradeGameAbility(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                gameAbilitiesLevel.damage++;
                break;
            case Enum_Abilities.AttackSpeed:
                gameAbilitiesLevel.attackSpeed++;
                break;
            case Enum_Abilities.Range:
                gameAbilitiesLevel.range++;
                break;
            case Enum_Abilities.Health:
                gameAbilitiesLevel.health++;
                break;
            case Enum_Abilities.HealthRegeneration:
                gameAbilitiesLevel.healthRegeneration++;
                break;
            case Enum_Abilities.GoldPerEnemy:
                gameAbilitiesLevel.goldPerEnemy++;
                break;
        }
        UpdateAbilities(ability);
    }


    void UpdateAbilities(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                TotalAbilities.damage = GetTotalValue(Enum_Abilities.Damage);
                GameEventManager.OnDamageUpgrade(TotalAbilities.damage);
                break;
            case Enum_Abilities.AttackSpeed:
                TotalAbilities.attackSpeed = GetTotalValue(Enum_Abilities.AttackSpeed);
                if (SceneManager.GetActiveScene().name != "Menu")
                {
                    Player.Instance.AttackSpeed = TotalAbilities.attackSpeed; 
                }
                GameEventManager.OnAttackSpeedUpgrade(TotalAbilities.attackSpeed);
                break;
            case Enum_Abilities.Range:
                TotalAbilities.range = GetTotalValue(Enum_Abilities.Range);
                if (SceneManager.GetActiveScene().name != "Menu")
                {
                    RangeController.Instance.RadiusRange = TotalAbilities.range;
                }
                GameEventManager.OnRangeUpgrade(TotalAbilities.range);
                break;
            case Enum_Abilities.Health:
                TotalAbilities.health = GetTotalValue(Enum_Abilities.Health);
                GameEventManager.OnHealthUpgrade(TotalAbilities.health);
                break;
            case Enum_Abilities.HealthRegeneration:
                TotalAbilities.healthRegeneration = GetTotalValue(Enum_Abilities.HealthRegeneration);
                //GameEventManager.OnHealthRegenerationUpgrade(TotalAbilities.healthRegeneration);
                break;
            case Enum_Abilities.GoldPerEnemy:
                TotalAbilities.goldPerEnemy = GetTotalValue(Enum_Abilities.GoldPerEnemy);
                //GameEventManager.OnGoldPerEnemyUpgrade(TotalAbilities.goldPerEnemy);
                break;
        }
    }

    public float GetPrice(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                return (gameAbilitiesLevel.damage+1)* firstPrice;
            case Enum_Abilities.AttackSpeed:
                return (gameAbilitiesLevel.attackSpeed + 1) * firstPrice;
            case Enum_Abilities.Range:
                return (gameAbilitiesLevel.range + 1) * firstPrice;
            case Enum_Abilities.Health:
                return (gameAbilitiesLevel.health + 1) * firstPrice;
            case Enum_Abilities.HealthRegeneration:
                return (gameAbilitiesLevel.healthRegeneration + 1) * firstPrice;
            case Enum_Abilities.GoldPerEnemy:
                return (gameAbilitiesLevel.goldPerEnemy + 1 )* firstPrice;
        }
        return float.MaxValue;
    }

    public float GetPriceMenu(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                return (GetBaseAbilityLevel(Enum_Abilities.Damage) + 1) * firstPrice;
            case Enum_Abilities.AttackSpeed:
                return (GetBaseAbilityLevel(Enum_Abilities.AttackSpeed) + 1) * firstPrice;
            case Enum_Abilities.Range:
                return (GetBaseAbilityLevel(Enum_Abilities.Range) + 1) * firstPrice;
            case Enum_Abilities.Health:
                return (GetBaseAbilityLevel(Enum_Abilities.Health) + 1) * firstPrice;
            case Enum_Abilities.HealthRegeneration:
                return (GetBaseAbilityLevel(Enum_Abilities.HealthRegeneration) + 1) * firstPrice;
            case Enum_Abilities.GoldPerEnemy:
                return (GetBaseAbilityLevel(Enum_Abilities.GoldPerEnemy) + 1) * firstPrice;
        }
        return float.MaxValue;
    }

    public string GetStats(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                return $"{GetTotalValue(Enum_Abilities.Damage)} > {GetNextLevelTotalValue(Enum_Abilities.Damage)}";
            case Enum_Abilities.AttackSpeed:
                return $"{GetTotalValue(Enum_Abilities.AttackSpeed)} > {GetNextLevelTotalValue(Enum_Abilities.AttackSpeed)}";
            case Enum_Abilities.Range:
                return $"{GetTotalValue(Enum_Abilities.Range)} > {GetNextLevelTotalValue(Enum_Abilities.Range)}";
            case Enum_Abilities.Health:
                return $"{GetTotalValue(Enum_Abilities.Health)} > {GetNextLevelTotalValue(Enum_Abilities.Health)}";
            case Enum_Abilities.HealthRegeneration:
                return $"{GetTotalValue(Enum_Abilities.HealthRegeneration)} > {GetNextLevelTotalValue(Enum_Abilities.HealthRegeneration)}";
            case Enum_Abilities.GoldPerEnemy:
                return $"{GetTotalValue(Enum_Abilities.GoldPerEnemy)} > {GetNextLevelTotalValue(Enum_Abilities.GoldPerEnemy)}";
        }
        return "Null";
    }

    public float GetTotalValue(Enum_Abilities ability)
    {
        AbilityValues abilityValues = GetAbilitySettingsValue(ability);
        float value = 0;
        value += abilityValues.firstValue;
        value += abilityValues.multiplier * GetBaseAbilityLevel(ability);
        value += abilityValues.multiplier * GetGameAbilityLevel(ability);
        return value;
    }

    public float GetNextLevelTotalValue(Enum_Abilities ability)
    {
        AbilityValues abilityValues = GetAbilitySettingsValue(ability);
        float value = 0;
        value += abilityValues.firstValue;
        value += abilityValues.multiplier * GetBaseAbilityLevel(ability);
        value += abilityValues.multiplier * (GetGameAbilityLevel(ability)+1);
        return value;
    }

    AbilityValues GetAbilitySettingsValue(Enum_Abilities ability)
    {
        foreach(var v in abilitySettings.abilityValues)
        {
            if(v.ability == ability)
            {
                return v;
            }
        }
        return null;
    }

    int GetBaseAbilityLevel(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                return ES3.Load<int>("base_"+ability.ToString(), 0);
            case Enum_Abilities.AttackSpeed:
                return ES3.Load<int>("base_" + ability.ToString(), 0);
            case Enum_Abilities.Range:
                return ES3.Load<int>("base_" + ability.ToString(), 0);
            case Enum_Abilities.Health:
                return ES3.Load<int>("base_" + ability.ToString(), 0);
            case Enum_Abilities.HealthRegeneration:
                return ES3.Load<int>("base_" + ability.ToString(), 0);
            case Enum_Abilities.GoldPerEnemy:
                return ES3.Load<int>("base_" + ability.ToString(), 0);
        }
        return 0;
    }

    int GetGameAbilityLevel(Enum_Abilities ability)
    {
        switch (ability)
        {
            case Enum_Abilities.Damage:
                return gameAbilitiesLevel.damage;
            case Enum_Abilities.AttackSpeed:
                return gameAbilitiesLevel.attackSpeed;
            case Enum_Abilities.Range:
                return gameAbilitiesLevel.range;
            case Enum_Abilities.Health:
                return gameAbilitiesLevel.health;
            case Enum_Abilities.HealthRegeneration:
                return gameAbilitiesLevel.healthRegeneration;
            case Enum_Abilities.GoldPerEnemy:
                return gameAbilitiesLevel.goldPerEnemy;
        }
        return 0;
    }


    




}
