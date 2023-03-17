using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManger : MonoBehaviour
{
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
    public Text volSliderText;
    public Text livesText;

    [Header("Slider")]
    public Slider volSlider;

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

        if (volSlider)
            volSlider.onValueChanged.AddListener(OnSliderValueChanged);

        if (livesText)
            GameManager.instance.onLifeValueChanged.AddListener(UpdateLifeText);
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        if (volSlider && volSliderText)
            volSliderText.text = volSlider.value.ToString();
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
        if (volSliderText)
            volSliderText.text = value.ToString();
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
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}
