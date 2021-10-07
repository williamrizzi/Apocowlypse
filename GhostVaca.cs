using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostVaca : MonoBehaviour
{
    SpriteRenderer sprite;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update ()
    {
        transform.Translate(Vector3.up * 2 * Time.deltaTime);

        sprite.color = new Color(1, 1, 1, sprite.color.a - 0.5f * Time.deltaTime);

        if(sprite.color.a < 0.05f)
        {
            Destroy(gameObject);
        }
	}
}
