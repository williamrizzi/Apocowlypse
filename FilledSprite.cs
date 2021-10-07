using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FilledSprite : MonoBehaviour {
    public SpriteMask mask;

    private void Update()
    {
        Apply();
    }
    public void Apply()
    {
       mask.transform.localScale = new Vector2( mask.transform.localScale.x, Mathf.MoveTowards(mask.transform.localScale.y, 3, 1f * Time.deltaTime));
    }
}
