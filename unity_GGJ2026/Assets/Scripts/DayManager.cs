using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{
    public float MaxTime=720f;

    private void Update()
    {
        MaxTime-= Time.deltaTime;
        if (MaxTime <= 0f)
        {
            UIManager.instance.OpenEndGameUI();
           //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
