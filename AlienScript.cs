using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienScript : MonoBehaviour
{
    public float speed;
    private GameObject spawn;
    private GameObject ship;
    private GameObject beam;
    private enum State { landing, beam }
    private State state;
    private float counter;

    private void Start()
    {
        spawn = GameObject.Find("AlienSpawn");
        ship = transform.GetChild(0).gameObject;
        beam = transform.GetChild(1).gameObject;
        counter = Time.time + 2.0f;
        Destroy(gameObject, 15.0f);
    }

    private void Update()
    {
        if (state == State.landing)
        {
            if (Time.time < counter)
            {
                Vector2 pos = transform.position;
                pos.x = spawn.transform.position.x;
                transform.position = pos;
            }
            else
            {
                state = State.beam;
            }
        }
        else if (state == State.beam)
        {
            transform.Translate(transform.right * -1 * speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (state == State.beam)
            if (collision.gameObject.tag == "Animal")
                if (collision.GetComponent<FollowAI>().estado == FollowAI.moveState.follow)
                {
                    MovementScript.Instance.RemoveCow(collision.gameObject);
                    collision.GetComponent<Abduction>().UFO = this.gameObject;
                }
    }
}
