using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimation : MonoBehaviour
{

    public enum animationStatus
    {
        idle, walk
    }

    public animationStatus status;

    public GameObject canvas;
    public float crono;

    public Animator anime;

    // Use this for initialization
    void Start()
    {


        //receber animator
        anime = transform.GetChild(1).GetComponent<Animator>();
        status = animationStatus.idle;
    }

    // Update is called once per frame
    void Update()
    {
        // Contador de start game
        if (crono > -1)
        {
            crono -= Time.deltaTime;
        }
        if (crono < 3)
        {
            canvas.transform.GetChild(0).GetComponent<Text>().text = "3";
        }
        if (crono < 2)
        {
            canvas.transform.GetChild(0).GetComponent<Text>().text = "2";
        }
        if (crono < 1)
        {
            canvas.transform.GetChild(0).GetComponent<Text>().text = "1";
        }
        if (crono < 0 && crono > -0.5f)
        {
            canvas.transform.GetChild(0).GetComponent<Text>().text = "GO!";
        }
        if (crono < -0.5f)
        {
            canvas.transform.GetChild(0).GetComponent<Text>().text = "";
        }


        //setar idle e walk
        if (status == animationStatus.idle)
        {
            anime.SetBool("Running", false);
        }
        if (status == animationStatus.walk)
        {
            anime.SetBool("Running", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.transform.name == "TriggerPlayer")
        {
            anime.SetTrigger("StartGame");
            GameObject.Find("Celeiro").transform.GetChild(0).GetComponent<PolygonCollider2D>().enabled = true;
            GameObject.Find("Main Camera").transform.GetChild(2).GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<MovementScript>().blockMove = false;
            GameObject.Find("GameManager").GetComponent<GameManagerScript>().StartSpawning();
            other.enabled = false;


        }
    }
}
