using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abduction : MonoBehaviour
{
    bool wasAbducted = false;

    [SerializeField]
    float _speedAbduction;

    GameObject _ufo;
    public GameObject UFO
    {
        get { return _ufo; }
        set
        {
            _ufo = value;

            wasAbducted = true;

            GetComponent<FollowAI>().estado = FollowAI.moveState.idle;

            Destroy(transform.GetChild(0).gameObject);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
		if(wasAbducted)
        {
            if (UFO == null)
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = new Vector2(UFO.transform.position.x, Mathf.MoveTowards(transform.position.y, UFO.transform.position.y, _speedAbduction * Time.deltaTime));
                transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(0.05f, 0.05f), (_speedAbduction / 3) * Time.deltaTime);
            }
        }
	}
}
