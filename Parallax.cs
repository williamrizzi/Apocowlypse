using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private Renderer back;

    [SerializeField]
    private float vel;

    [SerializeField]
    Vector2 offset;

    void Start()
    {
        back = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (MovementScript.Instance.gameObject.transform.position.x > CameraSmooth.instance.transform.position.x)
        {
            offset = new Vector2((MovementScript.Instance.gameObject.transform.position.x - CameraSmooth.instance.transform.position.x) * (vel) * Time.deltaTime, 0);
            back.material.mainTextureOffset += offset;
        }
    }
}
