using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Audio;

public class LoadLVL : MonoBehaviour
{
    [SerializeField] RectTransform fader;
    [SerializeField] Image loadingCircle;

    [SerializeField] AudioSource MenuTheme;
    [SerializeField] AudioSource GameMusic;

    [SerializeField] AudioMixer mixer;


    void Start()
    {
        Time.timeScale = 1f;
        GameMusic.Stop();
        fader.gameObject.SetActive(true);

        LeanTween.scale(fader, new Vector3(1, 1, 1), 0);
        LeanTween.scale(fader, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() => {
            fader.gameObject.SetActive(false);
        });
    }

    public void Load(int index)
    {
        fader.gameObject.SetActive(true);
        LeanTween.scale(fader, Vector3.zero, 0);
        LeanTween.scale(fader, new Vector3(10, 10, 10), 0.5f).setEase(LeanTweenType.easeInOutExpo).setOnComplete(() => {
            StartCoroutine(AsyncLoad(index));
            //SceneManager.LoadScene(index);
        });
        
    }

    public void OnPause()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
    }

    public void Continue()
    {
        AudioListener.pause = false;
        Time.timeScale = 1f;
    }

    public void ToMainMenu()
    {
        AudioListener.pause = false;
        AsyncOperation operation = SceneManager.LoadSceneAsync(0);
    }

    IEnumerator AsyncLoad(int index)
    {
        MenuTheme.Stop();
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);
        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            loadingCircle.fillAmount = progress;
            yield return null;
        }
        GameMusic.Play();

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
