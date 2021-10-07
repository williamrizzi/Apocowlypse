using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public float amplitude;
    public float speedX;
    public float speedY;
    public float pushStr;
    private Vector2 startPos;
    private float offset;
    public GameObject sprite;

    private void Start()
    {
        startPos = transform.position;
        startPos.y -= 1.3f;
        offset = Random.Range(0.0f, 7.0f);
        Destroy(gameObject, 10.0f);
    }

    private void Update()
    {
        startPos.x += speedX * Time.deltaTime;
        Vector2 nextPos = startPos;
        nextPos.y += amplitude * Mathf.Sin((Time.time + offset) * speedY);
        transform.position = nextPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Animal")
        {
            if (collision.transform.GetComponent<FollowAI>().estado == FollowAI.moveState.follow)
            {
                sprite.GetComponent<Animator>().Play("TornadoAnimationCow");

                MovementScript.Instance.RemoveCow(collision.gameObject);

                Destroy(collision.gameObject);
            }
        }
        else if (collision.gameObject.tag == "Player")
            collision.transform.GetComponent<MovementScript>().Launch(transform.position, pushStr);
    }
}
