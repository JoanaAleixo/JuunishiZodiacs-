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
    [SerializeField] string sceneAfterCombat;
    [SerializeField] GameObject enemiesPrefabForCombat;
    [SerializeField] Sprite lastBackgound;
    [SerializeField] bool oneCaracterOnCombat;

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

    public void LoadScene(string sceneId, GameObject gm, bool oneCarac, Sprite background)
    {
        sceneAfterCombat = sceneId;
        LoadCombatScene(gm,oneCarac, background);
    }

    public void LoadCombatScene(GameObject enemyPrefab, bool oneCar, Sprite background)
    {
        enemiesPrefabForCombat = enemyPrefab;
        lastBackgound = background;
        StartCoroutine(LoadCombatSceneAsync(enemyPrefab, oneCar, background));
        oneCaracterOnCombat = oneCar;
    }

    public void LoadSceneAfterCombat()
    {
        StartCoroutine(LoadSceneAsync(sceneAfterCombat));
    }

    public void ReloadCombatScene()
    {
        Destroy(CombatManager.combatInstance.gameObject);
        Destroy(CombatUiManager.uiInstance.gameObject);
        StartCoroutine(LoadCombatSceneAsync(enemiesPrefabForCombat, oneCaracterOnCombat, lastBackgound));
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

    IEnumerator LoadCombatSceneAsync(GameObject enemyPref, bool onCaracter, Sprite background)
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

        AsyncOperation operation2 = SceneManager.UnloadSceneAsync(sceneID);

        while (!operation2.isDone)
        {
            yield return null;
        }

        GameObject cM = GameObject.Find("CombatManager");
        Instantiate(enemyPref);
        cM.GetComponent<CombatManager>().LocateEnemies();
        if(background != null)
        {
            cM.GetComponent<CombatManager>().ChangeBackground(background);
        }
        if (onCaracter)
        {
            StartCoroutine(cM.GetComponent<CombatManager>().DiePls());
        }
        canTimer = true;
        
        while (timer < 3)
        {
            yield return null;
        }

        canTimer = false;
        timer = 0;
        _loadingScreen.SetActive(false);
        operation.allowSceneActivation = true;

    }

}
