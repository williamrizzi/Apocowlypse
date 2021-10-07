using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI : MonoBehaviour
{
    public enum moveState
    {
        follow, idle
    }

    public moveState estado;

    public Animator anime;
    private GameObject target; // player
    private MovementScript playerScript;
    private CircleCollider2D circleCollider;
    public float moveSpeed; // velocidade
    private float _distance;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        playerScript = target.GetComponent<MovementScript>();
        anime = transform.GetChild(1).GetComponent<Animator>();
        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.enabled = false;
        estado = moveState.idle;
    }

    private void Update()
    {
        _distance = Vector2.Distance(transform.position, target.transform.position);
    }

    private void FixedUpdate()
    {
        //se seguindo
        if (estado == moveState.follow)
        {
            Follow();
        }
    }

    private void Follow()
    {
        // vetor de posição a seguir o jogador
        if (_distance > playerScript.GetDistance(gameObject))
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            anime.SetBool("Running", true);
        }
        else if (playerScript.anim.status == PlayerAnimation.animationStatus.idle)
        {
            anime.SetBool("Running", false);
        }

        //Setar flip sprite
        if (target.transform.position.x < transform.position.x)
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player" && estado == moveState.idle)
        {
            GameObject.Find("SoundPlayer").GetComponent<SoundPlayer>().PlayVaca();
            // setar como seguindo
            estado = moveState.follow;
            // desativa o trigger
            GetComponent<BoxCollider2D>().enabled = false;
            circleCollider.enabled = true;
            playerScript.AddCow(gameObject);
        }

        if (other.transform.name == "TriggerCow")
        {
            anime.SetTrigger("NewGame");
            //other.enabled = false;
        }
    }
}
