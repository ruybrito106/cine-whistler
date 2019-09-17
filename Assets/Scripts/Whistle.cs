using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistle : MonoBehaviour
{

    private Collider2D collider;

    void Start()
    {
        collider = GetComponent<Collider2D>();
    }

    // FixedUpdate is called once per frame
    void FixedUpdate()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            var wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            var touchPosition = new Vector2(wp.x, wp.y);

            if (collider == Physics2D.OverlapPoint(touchPosition))
            {
                Debug.Log("HIT!");
            }
            else
            {
                Debug.Log("MISS");
            }
        }
    }
}
