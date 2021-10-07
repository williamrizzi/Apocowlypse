using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AerolitoExplosionScript : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(transform.root.gameObject);
    }
}
