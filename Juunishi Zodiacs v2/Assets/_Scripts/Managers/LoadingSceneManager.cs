using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor.SearchService;

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

    public void LoadScene(string sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public void LoadScene(string sceneId, GameObject gm)
    {
        if(sceneId == "CombatScene")
        {
            LoadCombatScene(gm);
        }
        else
        {
            LoadScene(sceneId);
        }
    }

    public void LoadCombatScene(GameObject enemyPrefab)
    {
        StartCoroutine(LoadCombatSceneAsync(enemyPrefab));
    }

    IEnumerator LoadSceneAsync(string sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId, LoadSceneMode.Single);
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
        _loadingScreen.SetActive(false);
        operation.allowSceneActivation = true;
        
        
    }

    IEnumerator LoadCombatSceneAsync(GameObject enemyPref)
    {
        UnityEngine.SceneManagement.Scene sceneID = SceneManager.GetActiveScene();
        AsyncOperation operation = SceneManager.LoadSceneAsync("CombatScene", LoadSceneMode.Additive);
        _loadingScreen.SetActive(true);
        
        //operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            _loadingBarFill.fillAmount = progressValue;

            yield return null;
        }

        SceneManager.UnloadSceneAsync(sceneID);

        GameObject cM = GameObject.Find("CombatManager");
        Instantiate(enemyPref);
        cM.GetComponent<CombatManager>().LocateEnemies();
        canTimer = true;
        Debug.Log("123plaese");
        
        while (timer < 2)
        {
            yield return null;
        }

        canTimer = false;
        timer = 0;
        _loadingScreen.SetActive(false);
        operation.allowSceneActivation = true;

    }
}
