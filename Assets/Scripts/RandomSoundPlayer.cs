using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RandomSoundPlayer : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> _sounds;

    private AudioSource _source;

    public void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void PlayRandomSound()
    {
        _source.PlayOneShot(_sounds[Random.Range(0, _sounds.Count)]);
    }
}
