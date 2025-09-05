using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    private bool _isMenuOpen = false;
    
    private bool _isNPMactive  = false;
    private GameObject[] checkpoints;
    public Vector3 _lastCheckpoint;
    
    public Vector3 _firstCheckpoint;
    
    [SerializeField] private Canvas _UICanvas;
    public GameObject MainMenu;
    public GameObject FinishMenu;
    public GameObject Currentlevel;
    public int _nmbrOfPespi;
    public int _nmbrPepsiUnlock = 0;
    
    private bool _isLevel1Finished =  false;
    private bool _isLevel2Finished =  false;
    
    public TextMeshProUGUI _textPepsi;
    public TextMeshProUGUI _textDeath;
    public int _nmbrOfDeath = 0;
    
    [SerializeField] private Image _level2Image;
    [SerializeField] private Image _level3Image;
    [SerializeField] private Sprite _level2Sprite;
    [SerializeField] private Sprite _level3Sprite;
    
    [SerializeField] private Button _level2Button;
    [SerializeField] private Button _level3Button;
    [SerializeField] private Sprite _levelUnlockSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //_UICanvas.gameObject.SetActive(false); //Desactive les options par précaution
        //FinishMenu.SetActive(false);
        _level2Button.GetComponent<Button>().interactable = false;
        _level3Button.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !FinishMenu.activeSelf && !MainMenu.activeSelf) //Active ou desactive les options en fonction de ssi le joueur est en jeu ou dans les options
        {
            if (!_isMenuOpen)
            {
                MenuActivation();
            }
            else if (_isMenuOpen)
            {
                MenuExit();
            }
        }

        if (_isLevel1Finished)
        {
            _level2Image.sprite = _level2Sprite;
            _isLevel1Finished = false;
            _level2Button.GetComponent<Image>().sprite = _levelUnlockSprite;
            _level2Button.GetComponent<Button>().interactable = true;
        }

        if (_isLevel2Finished)
        {
            _level3Image.sprite = _level3Sprite;
            _isLevel2Finished = false;
            _level3Button.GetComponent<Image>().sprite = _levelUnlockSprite;
            _level3Button.GetComponent<Button>().interactable = true;
        }
        
    }

    private void MenuActivation() //Active les options et stop le jeu en arrière-plan
    {
        _isMenuOpen = true;
        Time.timeScale = 0;
        _UICanvas.gameObject.SetActive(true);
    }

    private void MenuExit() //Desactive les options et relance le jeu
    {
        _isMenuOpen = false;
        Time.timeScale = 1;
        _UICanvas.gameObject.SetActive(false);
    }

    public void NoRespawnMode() //Active on non le mode no death allowed
    {
        if (!_isNPMactive)
        {
            _lastCheckpoint = _firstCheckpoint; //Si activé respawn le joueur aux coordonnées du spawn au lancement de la scène
            ChangeTriggerCheckPoint();
            _isNPMactive = true;
            
        }
        else if (_isNPMactive)
        {
            ChangeTriggerCheckPoint();
            _isNPMactive = false;
        }
    }

    private void ChangeTriggerCheckPoint() //Active ou desactive les checkpoints en fonction de l'etat de la bool no death allowed
    {
        checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        foreach (GameObject cp in checkpoints)
        {
            cp.GetComponent<BoxCollider2D>().enabled = _isNPMactive;
        }
    }

    public void LaunchALevel(GameObject level) //Permet d'active la prefab du level à lancer
    {
        MainMenu.gameObject.SetActive(false);
        _nmbrPepsiUnlock = 0;
        _nmbrOfPespi = 0;
        _nmbrOfDeath = 0;
        level.SetActive(true);
        GameObject[] total = GameObject.FindGameObjectsWithTag("Pepsi"); //Permet d'avoir le nombre de pepsi dans le niveau uniquement sans compter ceux des autres niveaux
        foreach (GameObject pp in total)
        {
            if (pp.activeSelf) _nmbrOfPespi++;
        }
        Currentlevel = level;
        NoRespawnMode();
    }

    public void BackToMainMenu() //Permet de retourner au menu principale, à la fin d'un niveau ou en cours de jeu
    {
        Currentlevel.SetActive(false);
        MainMenu.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void FinishLevel()
    {
        if (!_isLevel1Finished) _isLevel1Finished = true;
        else if (!_isLevel2Finished && _level2Button.GetComponent<Button>().interactable) _isLevel2Finished = true;
        //else if (_isLevel1Finished && _isLevel2Finished) ; //Si on met un truc quand le joueur fini le niveau 3
    }
}
