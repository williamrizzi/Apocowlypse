using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    public bool isStatic;
    public SpriteRenderer sprite;

    public float point;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(new Vector2(transform.position.x, transform.position.y - point), 0.05f);
    }

    private void Start()
    {
        if (isStatic)
            Sort();
    }

    private void Update()
    {
        if (!isStatic)
            Sort();
    }

    private void Sort()
    {
        sprite.sortingOrder = Mathf.FloorToInt((transform.position.y - point) / 0.1f) * -1 + 100;
    }
}
