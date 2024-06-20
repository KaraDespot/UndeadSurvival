using UnityEngine;
using UnityEngine.UI;

public class EnemyDeathCounter : MonoBehaviour
{
    public Text deathCountText;
    public int currentDeathCount = 0; 
    private int overallDeathCount = 0; 

    public void IncreaseDeathCount()
    {
        currentDeathCount++;
        overallDeathCount++;
        UpdateDeathCountUI();
    }

    public void UpdateDeathCountUI()
    {
        deathCountText.text = overallDeathCount.ToString();
    }

    public int GetDeathCount()
    {
        return currentDeathCount;
    }
}

