using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    #region Singleton
    public static GUIManager instance;

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

    public Button startGameBTN;
    public Button exitGameBTN;
    public Button backToMMBTN;
    public Button resumeGameBTN;
    public Button exitYesBTN;
    public Button exitNoBTN;
    public Button restartBTN;
    public GameObject exitPNL;
    public GameObject introPNL;

	private void Start()
	{
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex == 1)
        {
            Time.timeScale = 1f;
        }
    }

	public void StartGame()
    {
        SceneManager.LoadScene(1);
        introPNL.SetActive(true);
        OpenIntroPanel();
    }

    public void OpenIntroPanel()
    {
        introPNL.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseIntroPanel()
    {
        introPNL.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitGame()
    {
        ExitPanel();
    }
    public void ExitPanel()
    {
        exitPNL.SetActive(true);
    }
    public void ExitYesBTN()
    {
        Application.Quit();
    }
    public void ExitNoBTN()
    {
        exitPNL.SetActive(false);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ResumeGame()
    {
        PauseMenu.instance.isPauseMenuActive = false;
        PauseMenu.instance.pauseMenuPNL.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartLevel()
    {
        PauseMenu.instance.isPauseMenuActive = false;
        PauseMenu.instance.pauseMenuPNL.SetActive(false);
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
