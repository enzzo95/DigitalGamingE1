using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject Button;

    public void StartGame()
    {
        Debug.Log("Start Game");
        GetComponent<AudioSource>().Play();
        Invoke("LoadScene1", 3f);

    }
    
    public void OpenSettings()
    {
        Debug.Log("Open Settings");
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit(); 
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("Map1");
    }
}

    
