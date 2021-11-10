using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsController : MonoBehaviour
{
    [SerializeField] private float _musicVolume;
    [SerializeField] private float _effectsVolume;
    [SerializeField] private float _defaultMusicVolume;
    [SerializeField] private float _defaultEffectsVolume;
    [SerializeField] private int _resolutionWidth;
    [SerializeField] private int _resolutionHeight;
    [SerializeField] private int _defaultResolutionWidth;
    [SerializeField] private int _defaultResolutionHeight;
    [SerializeField] private bool _isFullscreen;
    [SerializeField] private bool _defaultIsFullScreen;

    public float MusicVolume => _musicVolume;
    public float EffectsVolume => _effectsVolume;
    public int ResolutionWidth => _resolutionWidth;
    public int ResolutionHeight => _resolutionHeight;
    public bool IsFullscreen => _isFullscreen;

    private void Awake()
    {
        int objectsCount = FindObjectsOfType<SettingsController>().Length;

        if (objectsCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }

        _resolutionWidth = ES3.Load("resolutionWidth", _defaultResolutionWidth);
        _resolutionHeight = ES3.Load("resolutionHeight", _defaultResolutionHeight);
        _isFullscreen = ES3.Load("isFullscreen", _defaultIsFullScreen);
        _musicVolume = ES3.Load("musicVolume", _defaultMusicVolume);
        _effectsVolume = ES3.Load("effectsVolume", _defaultEffectsVolume);
    }

    public void SetMusicVolume(float value)
    {
        _musicVolume = value;
    }

    public void SetEffectsVolume(float value)
    {
        _effectsVolume = value;
    }
}
