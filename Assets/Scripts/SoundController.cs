using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioClip _coinUpSound;
    [SerializeField] private AudioClip _powerUpSound;
    [SerializeField] private AudioClip _healthUpSound;
    [SerializeField] private AudioClip _takeDamegeSound;
    [SerializeField] private AudioClip _destroyEmenySound;
    [SerializeField] private AudioClip _movementSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _gameThemeSound;
    [SerializeField] private AudioClip _menuThemeSound;
    [SerializeField] private AudioClip _onMouseClickUISound;
    [SerializeField] private AudioClip _onMouseOverUISound;
    [SerializeField] private SpeedManager _speedManager;
    [SerializeField] private SettingsController _settingsController;
    [SerializeField] private float _pitchMin;
    [SerializeField] private float _pitchMax;
    [SerializeField] private float _gameOverSoundPitch;
    [SerializeField] private float _coinUpVolume;
    [SerializeField] private float _powerUpVolume;
    [SerializeField] private float _healthUpVolume;
    [SerializeField] private float _takeDamageVolume;
    [SerializeField] private float _destroyEnemyVolume;
    [SerializeField] private float _movementVolume;
    [SerializeField] private float _gameOverVolume;
    [SerializeField] private float _onMouseClickUIVolume;
    [SerializeField] private float _onMouseOverUIVolume;

    private AudioSource _gameSounds;
    private const string _menuScene = "MenuScene";
    private const string _gameScene = "GameScene";
    private const string _settingsControllerName = "SettingsController";
    private float _coinUpVolumeValue;
    private float _powerUpVolumeValue;
    private float _healthUpVolumeValue;
    private float _takeDamageVolumeValue;
    private float _destroyEnemyVolumeValue;
    private float _movementVolumeValue;
    private float _gameOverVolumeValue;
    private float _onMouseClickUIVolumeValue;
    private float _onMouseOverUIVolumeValue;

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == _gameScene)
        {
            _speedManager.SpeedChange += ChangeBackgroundMusicPitch;
        }
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == _gameScene)
        {
            _speedManager.SpeedChange -= ChangeBackgroundMusicPitch;
        }
    }

    private void Awake()
    {
        _gameSounds = GetComponent<AudioSource>();
        _settingsController = GameObject.Find(_settingsControllerName).GetComponent<SettingsController>();
        _gameSounds.Stop();

        if (SceneManager.GetActiveScene().name == _menuScene)
        {
            PlayBackgroundMusic(_menuThemeSound, _settingsController.MusicVolume);
        }
        else if (SceneManager.GetActiveScene().name == _gameScene)
        {
            PlayBackgroundMusic(_gameThemeSound, _settingsController.MusicVolume);
        }
    }

    private void Start()
    {
        EffectsVolumeCalculator();
    }

    private void PlayBackgroundMusic(AudioClip musicTheme, float volume = 0.5f)
    {
        _gameSounds.loop = true;
        _gameSounds.clip = musicTheme;
        _gameSounds.volume = volume;
        _gameSounds.Play();
    }

    private void ChangeBackgroundMusicPitch()
    {
        float pitch;
        float tempSpeed = _speedManager.CurrentSpeed - _speedManager.StartSpeed - _speedManager.SpeedIncrement;
        float interpolationFactor = tempSpeed / _speedManager.SpeedLimit;
        pitch = Mathf.Lerp(_pitchMin, _pitchMax, interpolationFactor);
        _gameSounds.pitch = pitch;
        Debug.Log("Current Pitch: " + pitch);
    }

    public void EffectsVolumeCalculator()
    {
        if (_settingsController.MusicVolume <= 0)
        {
            _gameSounds.volume = 0.0001f;
            _settingsController.SetMusicVolume(0.0001f);
        }
        _coinUpVolumeValue = (_coinUpVolume / _settingsController.MusicVolume) * _settingsController.EffectsVolume;
        _powerUpVolumeValue = (_powerUpVolume / _settingsController.MusicVolume) * _settingsController.EffectsVolume;
        _healthUpVolumeValue = (_healthUpVolume / _settingsController.MusicVolume) * _settingsController.EffectsVolume;
        _takeDamageVolumeValue = (_takeDamageVolume / _settingsController.MusicVolume) * _settingsController.EffectsVolume;
        _destroyEnemyVolumeValue = (_destroyEnemyVolume / _settingsController.MusicVolume) *_settingsController.EffectsVolume;
        _movementVolumeValue = (_movementVolume / _settingsController.MusicVolume) *_settingsController.EffectsVolume;
        _gameOverVolumeValue = (_gameOverVolume / _settingsController.MusicVolume) *_settingsController.EffectsVolume;
        _onMouseClickUIVolumeValue = (_onMouseClickUIVolume / _settingsController.MusicVolume) *_settingsController.EffectsVolume;
        _onMouseOverUIVolumeValue = (_onMouseOverUIVolume / _settingsController.MusicVolume) *_settingsController.EffectsVolume;
    }

    public void PlayCoinUpSound()
    {
        _gameSounds.PlayOneShot(_coinUpSound, _coinUpVolumeValue);
    }

    public void PlayPowerUpSound()
    {
        _gameSounds.PlayOneShot(_powerUpSound, _powerUpVolumeValue);
    }

    public void PlayHealthUpSound()
    {
        _gameSounds.PlayOneShot(_healthUpSound, _healthUpVolumeValue);
    }

    public void PlayTakeDamageSound()
    {
        _gameSounds.PlayOneShot(_takeDamegeSound, _takeDamageVolumeValue);
    }

    public void PlayDestroyEnemySound()
    {
        _gameSounds.PlayOneShot(_destroyEmenySound, _destroyEnemyVolumeValue);
    }

    public void PlayMovementSound()
    {
        _gameSounds.PlayOneShot(_movementSound, _movementVolumeValue);
    }

    public void PlayGameOverSound()
    {
        _gameSounds.pitch = _gameOverSoundPitch;
        _gameSounds.PlayOneShot(_gameOverSound, _gameOverVolumeValue);
    }

    public void PlayOnMouseClickUISound()
    {
        _gameSounds.PlayOneShot(_onMouseClickUISound, _onMouseClickUIVolumeValue);
    }

    public void PlayOnMouseOverUISound()
    {
        _gameSounds.PlayOneShot(_onMouseOverUISound, _onMouseOverUIVolumeValue);
    }

    public void StopBackgroundMusic()
    {
        _gameSounds.Stop();
    }

    public void PlayBackgroundMusic()
    {
        _gameSounds.Play();
    }

    public void ChangeBackgroundMusicPitchTo(float pitchValue)
    {
        _gameSounds.pitch = pitchValue;
    }

    public void ChangeBackgroundMusicVolumeTo(float musicVolumeValue)
    {
        _gameSounds.volume = musicVolumeValue;
    }
}
