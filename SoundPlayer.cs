using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    private static SoundPlayer instance;
    public AudioSource botao;
    public AudioSource vaca;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayBotao()
    {
        botao.Play();
    }

    public void PlayVaca()
    {
        vaca.Play();
    }
}
