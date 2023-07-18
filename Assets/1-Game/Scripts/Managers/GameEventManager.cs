using System;
using UnityEngine;

public class GameEventManager : MonoBehaviour
{
    // Player Events
    public static event Action OnDeathPlayerEvent;

    // Enemy Events
    public static event Action<Enemy> OnDeathEnemyEvent;
    public static event Action<Enemy> OnSpawnEnemyEvent;
    public static event Action<Enemy> OnChangedTargetEvent;
    public static event Action<float> OnChangedHPEvent;
    public static event Action<float> OnEnemyAttackedEvent;
    public static event Action<int> OnChangedLevelEvent;
    public static event Action<float> OnChangedGoldEvent;
    public static event Action<float> OnChangedGameEXPEvent;
    public static event Action<GameObject,float,Color> OnDamageTakenEvent;
    public static event Action<Popup> ShowPanelEvent;
    public static event Action<float> OnDamageUpgradeEvent;
    public static event Action<float> OnAttackSpeedUpgradeEvent;
    public static event Action<float> OnRangeUpgradeEvent;
    public static event Action<float> OnHealthUpgradeEvent;
    public static event Action<float> OnHealthRegenerationUpgradeEvent;
    public static event Action<float> OnGoldPerEnemyUpgradeEvent;
    public static event Action<GameObject, float> OnExpEarnedEvent;
    public static event Action OnPotionUsedEvent;
    public static event Action<bool> OnGamePauseEvent;
    public static event Action<bool> OnChangedSFXEvent;
    public static event Action<bool> OnChangedMusicEvent;

    public static void OnChangedMusic(bool value)
    {
        OnChangedMusicEvent?.Invoke(value);
    }
    public static void OnChangedSFX(bool value)
    {
        OnChangedSFXEvent?.Invoke(value);
    }

    public static void OnGamePause(bool value)
    {
        OnGamePauseEvent?.Invoke(value);
    }
    public static void OnChangedHP(float value)
    {
        OnChangedHPEvent?.Invoke(value);
    }
    public static void OnExpEarned(GameObject go, float value)
    {
        OnExpEarnedEvent?.Invoke(go,value);
    }
    public static void OnDamageUpgrade(float value)
    {
        OnDamageUpgradeEvent?.Invoke(value);
    }

    public static void OnAttackSpeedUpgrade(float value)
    {
        OnAttackSpeedUpgradeEvent?.Invoke(value);
    }

    public static void OnRangeUpgrade(float value)
    {
        OnRangeUpgradeEvent?.Invoke(value);
    }

    public static void OnHealthUpgrade(float value)
    {
        OnHealthUpgradeEvent?.Invoke(value);
    }

    public static void OnHealthRegenerationUpgrade(float value)
    {
        OnHealthRegenerationUpgradeEvent?.Invoke(value);
    }

    public static void OnGoldPerEnemyUpgrade(float value)
    {
        OnGoldPerEnemyUpgradeEvent?.Invoke(value);
    }

    public static void ShowPanel(Popup popup)
    {
        ShowPanelEvent?.Invoke(popup);
    }

    public static void OnChangedGameEXP(float value)
    {
        OnChangedGameEXPEvent?.Invoke(value);
    }

    public static void OnChangedGold(float value)
    {
        OnChangedGoldEvent?.Invoke(value);
    }

    public static void OnDeathPlayer()
    {
        OnDeathPlayerEvent?.Invoke();
    }

    public static void OnDeathEnemy(Enemy enemy)
    {
        OnDeathEnemyEvent?.Invoke(enemy);
    }

    public static void OnSpawnEnemy(Enemy enemy)
    {
        OnSpawnEnemyEvent?.Invoke(enemy);
    }

    public static void OnChangedTarget(Enemy enemy)
    {
        OnChangedTargetEvent?.Invoke(enemy);
    }

    public static void OnEnemyAttacked(float value)
    {
        OnEnemyAttackedEvent?.Invoke(value);
    }

    public static void OnChangedLevel(int value)
    {
        OnChangedLevelEvent?.Invoke(value);
    }

    public static void OnDamageTaken(GameObject go,float value,Color color)
    {
        OnDamageTakenEvent?.Invoke(go,value,color);
    }

    public static void OnPotionUsed()
    {
        OnPotionUsedEvent?.Invoke();
    }
}