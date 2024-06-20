using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform healthBarInstance; 
    public Slider healthSlider;

    void Start()
    {
        healthSlider = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if (healthBarInstance != null)
        {
            Vector3 enemyPosition = healthBarInstance.position;

            Vector3 healthBarPosition = enemyPosition + new Vector3(0.2f, 0.7f, 0f);

            Vector3 screenPosition = Camera.main.WorldToScreenPoint(healthBarPosition);

            healthSlider.transform.position = screenPosition;
        }
    }
}
