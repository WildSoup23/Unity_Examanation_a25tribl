using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public GameObject page1;
    public GameObject page2;
    public void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Page(int nr)
    {
        if (nr == 1)
        {
            page1.SetActive(true);
            page2.SetActive(false);
        }
        else if (nr == 2)
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
    }
}
