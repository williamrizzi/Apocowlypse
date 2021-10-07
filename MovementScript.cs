using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovementScript : MonoBehaviour
{
    static MovementScript _instance;
    public static MovementScript Instance
    {
        get { return _instance; }
    }

    public float speedMod;
    private Rigidbody2D rgBody;
    public PlayerAnimation anim;
    private List<GameObject> cowList;

    public Joystick joy;

    public List<GameObject> CowList
    {
        get { return cowList; }
    }

    public bool blockMove;
    public Vector2 values;

    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        blockMove = true;
        anim = GetComponent<PlayerAnimation>();
        rgBody = GetComponent<Rigidbody2D>();
        cowList = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SceneManager.LoadScene("Build");
            Time.timeScale = 1;
        }
    }

    private void FixedUpdate()
    {
        if (blockMove == false)
        {
            joy.gameObject.SetActive(true);

            float x = joy.Horizontal;
            float y = joy.Vertical;

            if(joy.Horizontal >= .2f)
            {
                x += speedMod * Time.deltaTime;
            }else if(joy.Horizontal <= -.2f)
            {
                x -= speedMod * Time.deltaTime;
            }
            else
            {
                x = 0f;
            }

            values = new Vector2(x, y);
            values *= speedMod * Time.deltaTime;
            transform.Translate(values);

            // comparação de axis para iniciar walk e flip do sprite

            if (x != 0 || y != 0)
            {

                anim.status = PlayerAnimation.animationStatus.walk;
                if (x < 0)
                {
                    transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;
                }
                else if (x > 0)
                {
                    transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
                }

            }
            else
            {
                anim.status = PlayerAnimation.animationStatus.idle;
            }
        }
        else
        {
            anim.status = PlayerAnimation.animationStatus.idle;
            joy.gameObject.SetActive(false);
        }
    }

    public void Launch(Vector3 center, float force)
    {
        Vector3 dir = transform.position - center;
        rgBody.AddForce(dir * force, ForceMode2D.Impulse);
    }

    public void AddCow(GameObject cow)
    {
        cowList.Add(cow);
    }

    public void RemoveCow(GameObject cow)
    {
        cowList.Remove(cow);
    }

    public int GetDistance(GameObject cow)
    {
        int index = 0;
        foreach (GameObject g in cowList)
            if (g == cow)
                break;
            else
                index++;

        return Mathf.FloorToInt(index / 10.0f) + 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name == "End")
        {
            GameManagerScript.Instancie.painel.SetActive(true);
            GameManagerScript.Instancie.blockSpawns = true;
            blockMove = true;
            GameManagerScript.Instancie.DestroyAllEnemys();

            foreach (GameObject go in cowList)
            {
                go.GetComponent<FollowAI>().estado = FollowAI.moveState.idle;
                go.GetComponent<FollowAI>().anime.SetBool("Running", false);
                go.GetComponent<FollowAI>().anime.Play("Idle eating");
            }
        }
    }
}
