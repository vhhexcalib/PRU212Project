using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScene : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] public AudioSource musicSource;
    [SerializeField] public AudioSource SFXSource;

    [Header("Audio Clip")]
    public AudioClip background;
    public AudioClip shooting;
    public AudioClip archershooting;
    public AudioClip enemydie;
    public AudioClip failscene;
    public AudioClip passscene;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }
}
