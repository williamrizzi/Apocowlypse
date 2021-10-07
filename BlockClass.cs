using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockClass : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _ground;

    public SpriteRenderer Ground
    {
        get { return _ground; }
    }

    [SerializeField]
    GameObject[] _spawsVacas;

    public GameObject[] SpawsVacas
    {
        get { return _spawsVacas; }
    }
}
