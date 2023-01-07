using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChild : MonoBehaviour
{
    [SerializeField]
    private ChildFrequency[] children;

    void Awake()
    {
        float random = Random.Range(0f, 1f);
        var iterator = 0f;
        foreach (ChildFrequency child in children)
        {
            child.child.SetActive(false);
        }
        foreach (ChildFrequency child in children)
        {
            iterator += child.frequency;
            child.child.SetActive(false);
            if (random < iterator)
            {
                child.child.SetActive(true);
                break;
            }
        }
    }

    [System.Serializable]
    public struct ChildFrequency { public float frequency; public GameObject child; }
}
