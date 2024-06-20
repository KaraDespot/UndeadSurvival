using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    #region Singleton
    public static PlayerState instance;

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

    public GameObject PlayerDeadPNL;
    public GameObject WonPNL;
    private PlayerVisual playerVisual;
    private bool isAlive;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDeadPNL.SetActive(false);
        WonPNL.SetActive(false);
        playerVisual = GetComponent<PlayerVisual>();
        //WonPNL = false;
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
