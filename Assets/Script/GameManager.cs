using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    private bool _isMenuOpen = false;
    
    private bool _isNPMactive  = false;
    private GameObject[] checkpoints;
    
    [SerializeField] private Canvas _UICanvas;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _UICanvas.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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

    private void MenuActivation()
    {
        _isMenuOpen = true;
        Time.timeScale = 0;
        _UICanvas.gameObject.SetActive(true);
    }

    private void MenuExit()
    {
        _isMenuOpen = false;
        Time.timeScale = 1;
        _UICanvas.gameObject.SetActive(false);
    }

    public void NoRespawnMode()
    {
        if (!_isNPMactive)
        {
            ChangeTriggerCheckPoint();
            _isNPMactive = true;
            
        }
        else if (_isNPMactive)
        {
            ChangeTriggerCheckPoint();
            _isNPMactive = false;
        }
    }

    private void ChangeTriggerCheckPoint()
    {
        checkpoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        foreach (GameObject cp in checkpoints)
        {
            cp.GetComponent<BoxCollider2D>().enabled = _isNPMactive;
        }
    }
}
