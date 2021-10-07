using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSmooth : MonoBehaviour
{
    public static CameraSmooth instance;

    public float speed;
    private GameObject player;
    public Vector3 pos;

    void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        player = MovementScript.Instance.gameObject;
    }

    private void FixedUpdate()
    {
        if (player.transform.position.x > transform.position.x)
        {
            pos = new Vector3(player.transform.position.x, 0, -10);
            transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
        }
    }
}
