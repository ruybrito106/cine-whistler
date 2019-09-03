using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{

    public Transform[] path;
    public float speed = 0.08f;

    private int cur = 0;
    private float eps = 0.01f;
    private float offset = 0.2f;

    private bool WithinMargin(float a, float b)
    {
        return Mathf.Abs(a - b) <= eps;
    }

    private int RotationType(float x0, float y0, float x1, float y1)
    {
        if (x1 - x0 >= offset)
        {
            return 0;
        }
        else if (x0 - x1 >= offset)
        {
            return 1;
        }
        else if (y1 - y0 >= offset)
        {
            return 2;
        }
        else if (y0 - y1 >= offset)
        {
            return 3;
        }

        return 4;
    }

    void FixedUpdate()
    {
        if (WithinMargin(transform.position.x, path[cur].position.x) && WithinMargin(transform.position.y, path[cur].position.y))
        {
            int next = (cur + 1) % path.Length;
            float type = RotationType(path[cur].position.x, path[cur].position.y, path[next].position.x, path[next].position.y);

            switch (type)
            {
                case 0:
                    transform.eulerAngles = new Vector3(0, 0, 270);
                    break;
                case 1:
                    transform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 2:
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 3:
                    transform.eulerAngles = new Vector3(0, 0, 180);
                    break;
            }

            cur = next;

        }
        else
        {
            Vector2 p = Vector2.MoveTowards(transform.position, path[cur].position, speed);
            GetComponent<Rigidbody2D>().MovePosition(p);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Whistler")
        {
            Destroy(collision.gameObject);
        }
    }
}
