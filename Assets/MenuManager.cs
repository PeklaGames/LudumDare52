using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    private int _creditsHash = Animator.StringToHash("show_credits");

    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int sceneIndex)
    {

        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
        SceneManager.UnloadSceneAsync(0);
    }

    public void ShowCredits()
    {
        _animator.SetBool(_creditsHash, true);
    }

    public void HideCredits()
    {
        _animator.SetBool(_creditsHash, false);
    }
}
