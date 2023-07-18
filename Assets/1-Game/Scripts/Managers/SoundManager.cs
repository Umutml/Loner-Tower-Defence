using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class SoundManager
{

    public enum Sound
    {
        PlayerAttack,
        Bow,
        SkeletonDeath,
        OrcDeath,
        GhostDeath,
        TakeDamageEnemy,
        Coin,
        Achievement1,
        Achievement2,
        Achievement3,
        NextLevelPanel,
        ButtonPositive,
        ButtonNegative,
        Upgrade,
        HPPotion,
        ManaPotion,
        GainPotion,
        CheckpointPanel,
        CastleDestroy,
        PlayerTakeDamage,
        CastleRevive,
        SecondChancePanel,
        RevivePanel,
        Fire1,
        Fire2,
        Fire3,
        ChestOpen,
        ChestClose,
        GameStart,
        BossIncoming,
        ScorePanel,
        Complete,
        TowerTakeHit,
        UpgradePanel
        
    }
    private static Dictionary<Sound, float> soundTimerDictionary;
    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public static bool musicOn = true, soundOn = true;
    public static AudioMixer mixer;

    
    public static void PlaySoundOneShot(Sound sound, float volume = 0.5f)
    {
        if (soundOn)
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.volume = volume;
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void PlaySoundOneShot(Sound sound, Vector2 pitch, float volume = 0.5f)
    {
        if (soundOn)
        {
            if (oneShotGameObject == null)
            {
                oneShotGameObject = new GameObject("One Shot Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.volume = volume;
            oneShotAudioSource.pitch = Random.Range(pitch.x, pitch.y);
            oneShotAudioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    public static void PlaySound(Sound sound, float volume = 0.5f)
    {
        if (CanPlaySound(sound) && soundOn)
        {
            GameObject soundGameObject = new GameObject("Sound_" + sound.ToString());
            AudioSource source = soundGameObject.AddComponent<AudioSource>();
            source.clip = GetAudioClip(sound);
            source.volume = volume;
            source.Play();
            Object.Destroy(soundGameObject, source.clip.length);
        }
    }

    public static void PlaySound(Sound sound, Vector2 pitch, float volume = 0.5f)
    {
        if (CanPlaySound(sound) && soundOn)
        {
            GameObject soundGameObject = new GameObject("Sound_" + sound.ToString());
            AudioSource source = soundGameObject.AddComponent<AudioSource>();
            source.clip = GetAudioClip(sound);
            source.pitch = Random.Range(pitch.x, pitch.y);
            source.volume = volume;
            source.Play();
            Object.Destroy(soundGameObject, source.clip.length);
        }
    }


    public static void Initialize()
    {
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerAttack] = 0;
        mixer = GameManager.Instance._audioMixer;
        UpdateSettings();
        //Taptic.tapticOn = ES3.Load<bool>("tapticOn", true);


    }

 
    public static void UpdateSettings()
    {
        musicOn = GameManager.Instance.Music;
        soundOn = GameManager.Instance.SoundFX;
    }

    private static bool CanPlaySound(Sound sound)
    {
        switch (sound)
        {
            default:
                return true;
            case Sound.PlayerAttack:
                if (soundTimerDictionary.ContainsKey(sound))
                {
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = .1f;
                    if (lastTimePlayed + playerMoveTimerMax < Time.time)
                    {
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
                break;
        }
    }


    private static AudioClip GetAudioClip(Sound sound)
    {
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.i.soundAudioClipArray)
        {

            if (soundAudioClip.sound == sound)
            {
                return soundAudioClip.audioClip[Random.Range(0, soundAudioClip.audioClip.Length)];
            }
        }
        Debug.LogError("Sound " + sound + " not found!");
        return null;
    }

}
