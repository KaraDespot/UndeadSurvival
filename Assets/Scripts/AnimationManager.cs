using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    #region Singleton
    public static AnimationManager instance;

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

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayEnemy0Hit()
    {
        anim.SetTrigger("Hit");
    }

    public void PlayDeadEnemy()
    {
        anim.Play("DeadEnemy 0");
    }

    public void PlayPlayerDeath()
    {
        anim.Play("Player_Death");
    }
}
