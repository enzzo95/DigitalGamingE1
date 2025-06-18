using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject Button;

    public void StartButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadMenuMaps", 2f);
    }
    
    public void SettingsButton()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Open Settings");
    }

    public void WaterfallButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadMapWaterfall", 2f);
    }

    public void NightButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadMapNight", 2f);
    }

    public void BackButton()
    {
        GetComponent<AudioSource>().Play();
        Invoke("LoadMainMenu", 2f);
    }

    public void ExitButton()
    {
        GetComponent<AudioSource>().Play();
        Debug.Log("Exit Game");
        Invoke("Quit", 2f);
    }


    public void LoadMapWaterfall()
    {
        SceneManager.LoadScene("mapWaterfall");
    }

    public void LoadMapNight()
    {
        SceneManager.LoadScene("mapNight");
    }

    public void LoadMenuMaps()
    {
        SceneManager.LoadScene("MENU2");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MENU");
    }

    public void Quit()
    {
        Application.Quit();
    }

}

    
