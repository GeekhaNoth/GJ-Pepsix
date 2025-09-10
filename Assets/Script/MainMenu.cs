using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _mainScreen;
    public GameObject _launcher;
    public GameObject _settings;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _launcher.SetActive(false);
        _settings.SetActive(false);
    }

    public void GoToLaunchMenu()
    {
        _launcher.SetActive(true);
        _mainScreen.SetActive(false);
        _settings.SetActive(false);
    }
    
    public void GoToSettings()
    {
        _launcher.SetActive(false);
        _mainScreen.SetActive(false);
        _settings.SetActive(true);
    }
    
    public void GoToMainScreen()
    {
        _launcher.SetActive(false);
        _mainScreen.SetActive(true);
        _settings.SetActive(false);
    }
}
