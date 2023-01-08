using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadTrigger : MonoBehaviour
{
    [SerializeField]
    private string _nextSceneName;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == 6)
        {
            SceneManager.LoadScene(_nextSceneName, LoadSceneMode.Single);
        }
    }
}
