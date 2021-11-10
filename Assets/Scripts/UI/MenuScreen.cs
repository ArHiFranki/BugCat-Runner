using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScreen : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _backButton;
    [SerializeField] private GameObject _mainMenuPanel;
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _effectsVolumeSlider;
    [SerializeField] private Toggle _fullScreenToggle;
    [SerializeField] private TMP_Dropdown _resolutionDropdown;
    [SerializeField] private SettingsController _settingsController;
    [SerializeField] private SoundController _soundController;

    private const string _settingsControllerName = "SettingsController";
    private Resolution[] _resolutions;

    private void OnEnable()
    {
        _playButton.onClick.AddListener(OnPlayButtonClick);
        _settingsButton.onClick.AddListener(OnSettingsButtonClick);
        _exitButton.onClick.AddListener(OnExitButtonClick);
        _backButton.onClick.AddListener(OnBackButtonClick);
    }

    private void OnDisable()
    {
        _playButton.onClick.RemoveListener(OnPlayButtonClick);
        _settingsButton.onClick.RemoveListener(OnSettingsButtonClick);
        _exitButton.onClick.RemoveListener(OnExitButtonClick);
        _backButton.onClick.RemoveListener(OnBackButtonClick);
    }

    private void Start()
    {
        _settingsController = GameObject.Find(_settingsControllerName).GetComponent<SettingsController>();

        Screen.SetResolution(_settingsController.ResolutionWidth, 
                             _settingsController.ResolutionHeight, 
                             _settingsController.IsFullscreen);

        GetAvailableResolutions();
        _fullScreenToggle.isOn = Screen.fullScreen;
        _mainMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
        _musicVolumeSlider.value = _settingsController.MusicVolume;
        _effectsVolumeSlider.value = _settingsController.EffectsVolume;
    }

    private void GetAvailableResolutions()
    {
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        _resolutions = Screen.resolutions;
        _resolutionDropdown.ClearOptions();

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + " x " + _resolutions[i].height;
            options.Add(option);

            if (_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionDropdown.AddOptions(options);
        _resolutionDropdown.value = currentResolutionIndex;
        _resolutionDropdown.RefreshShownValue();
    }

    private void OnPlayButtonClick()
    {
        SceneManager.LoadScene(1);
    }

    private void OnSettingsButtonClick()
    {
        _mainMenuPanel.SetActive(false);
        _settingsPanel.SetActive(true);
    }

    private void OnExitButtonClick()
    {
        Debug.Log("Application Quit");
        Application.Quit();
    }

    private void OnBackButtonClick()
    {
        _mainMenuPanel.SetActive(true);
        _settingsPanel.SetActive(false);
        OnMusicVolumeChange();
        OnEffectsVolumeChange();
        ES3.Save("musicVolume", _musicVolumeSlider.value);
        ES3.Save("effectsVolume", _effectsVolumeSlider.value);
        ES3.Save("resolutionWidth", Screen.width);
        ES3.Save("resolutionHeight", Screen.height);
        ES3.Save("isFullscreen", Screen.fullScreen);
    }

    public void OnMusicVolumeChange()
    {
        _settingsController.SetMusicVolume(_musicVolumeSlider.value);
        _soundController.ChangeBackgroundMusicVolumeTo(_musicVolumeSlider.value);
    }

    public void OnEffectsVolumeChange()
    {
        _settingsController.SetEffectsVolume(_effectsVolumeSlider.value);
        _soundController.EffectsVolumeCalculator();
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
