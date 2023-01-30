using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingSceneManager : MonoBehaviour
{
    public GameObject _loadingScreen;
    public Image _loadingBarFill;
    public static LoadingSceneManager sceneInstance;
    [SerializeField] private float timer;
    private bool canTimer = false;

    private void Awake()
    {
        if (sceneInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            sceneInstance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        if(canTimer)
            timer += Time.deltaTime;
    }

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        _loadingScreen.SetActive(true);
        canTimer = true;
        operation.allowSceneActivation = false;

        while (!operation.isDone && timer < 2)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            _loadingBarFill.fillAmount = progressValue;

            yield return null;
        }

        
        canTimer = false;
        timer = 0;
        Debug.Log("please 1");
        _loadingScreen.SetActive(false);
        operation.allowSceneActivation = true;
        
        
    }
}
