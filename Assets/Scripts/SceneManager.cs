using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManager : MonoBehaviour
{


    public Animator transition;
    public Animator circleWipe;



    public float transitionTime = 1f;


    private void Start()
    {

    }
    private void Update()
    {

    }
    public void LoadMainScene()
    {
        StartCoroutine(this.LoadScene("SampleScene"));
    }

    public void LoadMainMenu()
    {
        StartCoroutine(this.LoadScene("MainMenu"));
    }

    public void LoadOptions()
    {
        StartCoroutine(this.LoadScene("Options"));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public IEnumerator LoadScene(string SceneName)
    {
        // Play Animation
        circleWipe.SetTrigger("StartCircle");

        // Wait
        yield return new WaitForSeconds(transitionTime);

        // Load Scene
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }



}