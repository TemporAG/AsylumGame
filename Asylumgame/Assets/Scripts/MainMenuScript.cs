using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject Play;
    public GameObject Options;
    public GameObject Quit;
    public GameObject MiniTutorial;
    public GameObject Back;
    public GameObject Slider;
    public void PlayGame()
    {
        Play.SetActive(false);
        Options.SetActive(false);
        Quit.SetActive(false);
        MiniTutorial.SetActive(true);
    }

    public void OptionsB()
    {
        Back.SetActive(true);
        Slider.SetActive(true);
        Play.SetActive(false);
        Options.SetActive(false);
        Quit.SetActive(false);
    }

    public void QuitB()
    {
        Application.Quit();
        Debug.Log("quit");
    }

    public void BBack()
    {
        Play.SetActive(true);
        Options.SetActive(true);
        Quit.SetActive(true);
        Back.SetActive(false);
        Slider.SetActive(false);
    }

    public void PPlay()
    {
        SceneManager.LoadSceneAsync(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
