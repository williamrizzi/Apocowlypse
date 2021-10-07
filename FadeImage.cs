using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeImage : MonoBehaviour
{
    static FadeImage _instance;
    public static FadeImage Instance
    {
        get { return _instance; }
    }

    Animator anim;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();
    }

    void Update()
    {
        transform.position = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.y);
    }

    public void FadeIn()
    {
        anim.Play("FadeIn");
    }

    public void FadeOut()
    {
        anim.Play("FadeOut");
    }
}
