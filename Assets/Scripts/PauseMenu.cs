using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Singleton
    public static PauseMenu instance;

    private void Awake()
    {
        // If there is no instance of this manager, Destroy it just in case before creating a new instance
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    public GameObject pauseMenuPNL;
    public bool isPauseMenuActive;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuPNL.SetActive(false);
        isPauseMenuActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)|| Input.GetKeyDown(KeyCode.P))
        {
            if (!isPauseMenuActive)
            {
                isPauseMenuActive = true;
                PauseMenuOpen();
            }
            else if (isPauseMenuActive && gameObject.activeInHierarchy)
            {
                isPauseMenuActive = false;
                PauseMenuClose();
            }
        }

        void PauseMenuOpen()
        {
            pauseMenuPNL.SetActive(true);
            Time.timeScale = 0f;
        }

        void PauseMenuClose()
        {
            pauseMenuPNL.SetActive(false);
            Time.timeScale = 1f;
        }
    }
}