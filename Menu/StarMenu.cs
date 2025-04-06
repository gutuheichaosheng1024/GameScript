using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StarMenu : MonoBehaviour
{
    public float SwitchSpeed;
    private float Alpha;
    private GameObject Fade;
    private Image Image;

    private void Awake()
    {
        Fade = GameObject.Find("Fade");
        Image = Fade.GetComponent<Image>();
    }

    void Start()
    {
        StartCoroutine(FadeIn());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        StartCoroutine(FadeOut("Level_0"));
       
    }

    public void GameExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();

    }

    IEnumerator FadeIn()
    {
        Alpha = 1f;

        while (Alpha > 0f)
        {
            Alpha -= SwitchSpeed;
            Image.color = new Color(0,0,0,Alpha);
            yield return null;
        }
    }


    IEnumerator FadeOut(string Scene_Name)
    {
        Alpha = 0f;

        while (Alpha < 1f)
        {
            Alpha += SwitchSpeed;
            Image.color = new Color(0, 0, 0, Alpha);
            yield return null;
        }

        SceneManager.LoadScene(Scene_Name, LoadSceneMode.Single);
    }
}
