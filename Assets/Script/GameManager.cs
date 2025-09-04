using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private bool _isMenuOpen = false;
    
    private bool _isNPMactive  = false;
    private GameObject[] checkpoints;
    public Vector3 _lastCheckpoint;
    
    [SerializeField] private Canvas _UICanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _UICanvas.gameObject.SetActive(false); //Desactive les options par précaution
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Active ou desactive les options en fonction de ssi le joueur est en jeu ou dans les options
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
            _lastCheckpoint = new Vector3(-5,-2,0); //Si activé respawn le joueur à ces coordonnées, à modifier en fonction des niveaux
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
}
