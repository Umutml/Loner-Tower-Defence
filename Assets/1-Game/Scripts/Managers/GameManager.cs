using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioMixer _audioMixer;
    private float _gameGold = 0;
    [SerializeField] [ReadOnly] private float _gameEXP = 0;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            FirstOpening = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
        SoundManager.Initialize();
        Advertisements.Instance.Initialize();
        SoundFX = true;
        Music = true;


    }
    public float GameEXP { get { return _gameEXP; } set { _gameEXP = value; GameEventManager.OnChangedGameEXP(_gameEXP); } }
    public float GameGold { get { return _gameGold; } set { _gameGold = value; GameEventManager.OnChangedGold(_gameGold); } }
    public bool SoundFX { 
        get 
        {
            return ES3.Load<bool>("settings_soundfx", true);
        } 
        set 
        {
            if (value)
            {
                _audioMixer.SetFloat("SFXVolume", 0);
            }
            else
            {
                _audioMixer.SetFloat("SFXVolume", -80);
            }
            ES3.Save<bool>("settings_soundfx", value);
        }
    }

    public float BaseGold
    {
        get
        {
            return ES3.Load<float>("game_basegold", 0);
        }
        set
        {
            ES3.Save<float>("game_basegold", value);
            GameEventManager.OnChangedGold(BaseGold);
        }
    }



    public float HighScore
    {
        get
        {
            return ES3.Load<float>("game_highscore", 0);
        }
        set
        {
            ES3.Save<float>("game_highscore", value);
            //GameEventManager.OnChangedGold(BaseGold); SCORE DE���T� EVENTI GELEB�L�R
        }
    }

    public bool FirstOpening
    {
        get
        {
            return ES3.Load<bool>("game_firstopening", true);
        }
        set
        {
            ES3.Save<bool>("game_firstopening", value);
        }
    }

    public bool Music
    {
        get
        {
            return ES3.Load<bool>("settings_music", true);
        }
        set
        {
            if (value)
            {
                _audioMixer.SetFloat("MusicVolume", 0);
            }
            else
            {
                _audioMixer.SetFloat("MusicVolume", -80);
            }
            ES3.Save<bool>("settings_music", value);
        }
    }


    void OnEnable() => GameEventManager.OnDeathEnemyEvent += AddEXP;
    void OnDisable() => GameEventManager.OnDeathEnemyEvent -= AddEXP;
    

    void AddEXP(Enemy e)
    {
        GameEXP = _gameEXP +  AbilityController.Instance.GetTotalValue(Enum_Abilities.GoldPerEnemy);
    }



}
