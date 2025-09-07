using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    public void LoadLevel(int index)
    {
        if (index == 0)
        {
            SceneManager.LoadScene("Level0");
        }
        if (index == 1)
        {
            SceneManager.LoadScene("Level1");
        }
        if (index == 2)
        {
            SceneManager.LoadScene("Level2");
        }
        if (index == 3)
        {
            SceneManager.LoadScene("Level3");
        }
        if (index == 4)
        {
            SceneManager.LoadScene("Level4");
        }
        if (index == 5)
        {
            SceneManager.LoadScene("Level5");
        }
    }
}
