using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerolitoScript : MonoBehaviour
{
    public float pushStr;

    private enum Status
    {
        target, meteor, explosion
    }
    private Status state;

    private GameObject target;
    private GameObject meteor;
    private GameObject explosion;
    private CircleCollider2D circleCollider;
    private float counter;

    public GameObject crack;

    public GameObject vacaDead;

    private void Start()
    {
        state = Status.target;
        target = transform.GetChild(0).gameObject;
        target.SetActive(true);
        meteor = transform.GetChild(1).gameObject;
        meteor.SetActive(false);
        explosion = transform.GetChild(2).gameObject;
        explosion.SetActive(false);
        counter = Time.time + 3.0f;
        target.transform.localScale = new Vector3(0.01f, 0.01f, 1.0f);
        circleCollider = GetComponent<CircleCollider2D>();
    }


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Grass"))
        {
            Destroy(collision.gameObject);
        }
            
    }*/

    private void Update()
    {
        if (state == Status.target)
        {
            if (Time.time < counter)
            {
                Vector3 scale = new Vector3()
                {
                    x = 0.99f / 3.0f * Time.deltaTime,
                    y = 0.99f / 3.0f * Time.deltaTime,
                    z = 0.0f
                };
                target.transform.localScale += scale;
            }
            else
            {
                counter = Time.time + 0.25f;
                meteor.SetActive(true);
                meteor.transform.Translate(new Vector2(Random.Range(-3.0f, 3.0f), 10.0f));
                state = Status.meteor;
            }
        }
        else if (state == Status.meteor)
        {
            if (Time.time < counter)
            {
                meteor.transform.position = Vector2.MoveTowards(meteor.transform.position, transform.position, 41.76f * Time.deltaTime);
            }
            else
            {
                CameraShake.instance.ShakeCam(CameraShake.instance.shakeAmount);

                target.SetActive(false);
                meteor.SetActive(false);
                explosion.SetActive(true);
                Collider2D[] colliders = new Collider2D[100];
                ContactFilter2D filter = new ContactFilter2D();
                filter.NoFilter();
                circleCollider.OverlapCollider(filter, colliders);

                foreach (Collider2D c in colliders)
                    if (c != null)
                        if (c.tag == "Animal" && !c.isTrigger)
                        {
                            if (c.GetComponent<FollowAI>().estado == FollowAI.moveState.follow)
                            {
                                Instantiate(vacaDead, c.gameObject.transform.position, vacaDead.transform.rotation);

                                MovementScript.Instance.RemoveCow(c.gameObject);

                                Destroy(c.gameObject);
                             }
                        }
                        else if (c.tag == "Player")
                        {
                            c.GetComponent<MovementScript>().Launch(transform.position, pushStr);
                        }
                        else if (c.CompareTag("Grass"))
                        {
                            Destroy(c.gameObject);
                        }


                Instantiate(crack, transform.position, transform.rotation);

                state = Status.explosion;
            }
        }
    }
}
