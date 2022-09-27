using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transição : MonoBehaviour
{
    [SerializeField] bool textoInicial;
    [SerializeField] GameObject[] coisasDesaparecer;
    [SerializeField] GameObject[] coisasAparecer;
    Animator animator;

    [SerializeField] GameObject sitio;
    [SerializeField] MovementManager movManager;

    public Animator Animator { get => animator; set => animator = value; }
    public GameObject[] CoisasAparecer { get => coisasAparecer; set => coisasAparecer = value; }
    public GameObject[] CoisasDesaparecer { get => coisasDesaparecer; set => coisasDesaparecer = value; }

    void Start()
    {
        Animator = gameObject.GetComponent<Animator>();
        if(sitio != null)
            movManager = movManager.GetComponent<MovementManager>();
        if(textoInicial == false)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        Animator.SetTrigger("FadeOut");

        yield return new WaitForSeconds(2f);
        foreach (var item in CoisasDesaparecer)
            item.gameObject.SetActive(false);

        yield return new WaitForSeconds(2f);
        foreach (var item in CoisasAparecer)
            item.gameObject.SetActive(true);

        Animator.SetTrigger("FadeIn");

        yield return new WaitForSeconds(1f);

        if (sitio != null)
        {
            sitio.gameObject.SetActive(true);
            movManager.PosiçaoCamera = sitio.transform;
            movManager.CameraMover.transform.position = new Vector3(movManager.PosiçaoCamera.transform.position.x, movManager.PosiçaoCamera.transform.position.y, -10);
        }

        gameObject.SetActive(false);

        //if (nomeScene != "")
        //{
        //    yield return new WaitForSeconds(1.2f);
        //    SceneManager.LoadScene(nomeScene);
        //}
    }

}
