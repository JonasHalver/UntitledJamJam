using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    float t = 0;
    public Text title;
    public Image logo;

    public GameObject b1, b2;

    bool allowLoad;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        title.color = new Color(title.color.r, title.color.g, title.color.b, t);
        logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, t);
    }

    public void LogoClicked()
    {
        StartCoroutine(FadeOut());
    }

    public void PlayClicked()
    {
        allowLoad = true;
    }
    
    public void ExitClicked()
    {
        Application.Quit();
    }

    IEnumerator FadeIn()
    {
        t = 0;
        for (int i = 0; i < 100; i++)
        {
            t += 0.01f;
            yield return new WaitForSecondsRealtime(1 / 100);
        }
    }
    IEnumerator FadeOut()
    {
        StartCoroutine(LoadScene());
        t = 1;
        for (int i = 0; i < 100; i++)
        {
            t -= 0.01f;
            yield return new WaitForSecondsRealtime(1 / 100);
        }
        b1.SetActive(true);
        b2.SetActive(true);        
    }

    IEnumerator LoadScene()
    {
        yield return null;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        
        while (!asyncOperation.isDone)
        {
            if (asyncOperation.progress >= 0.9f)
            {
                if (allowLoad)
                    asyncOperation.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
