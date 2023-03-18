using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class UIManger : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("Button")]
    public Button startButton;
    public Button settingButton;
    public Button quitButton;
    public Button titleButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;


    [Header("Menu")]
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject pauseMenu;

    [Header("Text")]
    public Text MusicVolSliderText;
    public Text SFXVolSliderText;
    public Text livesText;

    [Header("Slider")]
    public Slider MusicVolSlider;
    public Slider SFXVolSlider;

    public AudioClip pauseSound;
    public AudioClip enemyHurtSound;

    // Start is called before the first frame update
    void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    void ResumeGame()
    {
        pauseMenu.SetActive(false);
    }

    void UpdateLifeText(int value)
    {
        livesText.text = value.ToString();
    }

    void Start()
    {
        if (startButton)
            startButton.onClick.AddListener(StartGame);
        if (settingButton)
            settingButton.onClick.AddListener(ShowSettingsMenu);
        if (quitButton)
            quitButton.onClick.AddListener(QuitGame);
        if (backButton)
            backButton.onClick.AddListener(ShowMainMenu);
        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(ShowMainMenu);
        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(ResumeGame);

        if (MusicVolSlider)
            MusicVolSlider.onValueChanged.AddListener(OnSliderValueChanged);
        if (SFXVolSlider)
            SFXVolSlider.onValueChanged.AddListener(OnSliderValueChanged);

        if (livesText)
            GameManager.instance.onLifeValueChanged.AddListener(UpdateLifeText);
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        if (MusicVolSlider && MusicVolSliderText)
        {
            float value;
            audioMixer.GetFloat("MusicVol", out value);
            MusicVolSlider.value = value + 80;
            MusicVolSliderText.text = (value + 80).ToString();
        }
        if (SFXVolSlider && SFXVolSliderText)
        {
            float value;
            audioMixer.GetFloat("SFXVol", out value);
            SFXVolSlider.value = value + 80;
            SFXVolSliderText.text = (value + 80).ToString();
        }
    }

    void ShowMainMenu()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1|| SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
        }
    }

    void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    void OnSliderValueChanged(float value)
    {
        if (MusicVolSliderText)
        {
            MusicVolSliderText.text = value.ToString();
            audioMixer.SetFloat("MusicVol", value - 80);
        }
        if (SFXVolSliderText)
        {
            SFXVolSliderText.text = value.ToString();
            audioMixer.SetFloat("SFXVol", value - 80);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if(Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            if(pauseMenu.activeSelf)
            {
                GameManager.instance.playerInstance.GetComponent<AudioSourceManager>().PlayOneShot(pauseSound, false);
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
