using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public float speed = 0.08f;
    public int id;

    private Transform[] path;
    private int cur = 0;
    private float eps = 0.01f;
    private float offset = 0.2f;
    private List<List<int>> pathIds = new List<List<int>> { 
        new List<int> { 45, 30, 32, 58, 61, 71, 70, 80, 79, 86, 85, 77, 78, 68, 67, 57, 53, 42, 41, 25, 26, 16, 17, 2, 3, 10, 9, 18, 19, 29, 30 },
        new List<int> { 45, 30, 33, 5, 6, 24, 23, 34, 35, 49, 48, 60, 55, 28, 27, 83, 82, 72, 74, 27, 30 },
        new List<int> { 45, 30, 32, 58, 59, 5, 4, 11, 14, 24, 22, 69, 68, 78, 77, 85, 82, 72, 74, 2, 1, 15, 17, 27, 30 },
        new List<int> { 45, 30, 31, 20, 21, 12, 11, 4, 6, 14, 7, 15, 17, 54, 55, 50, 51, 50, 28, 32, 78, 77, 85, 82, 72, 74, 27, 30 }   
    };

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

    void Start()
    {
        GameObject[] gos = GameObject.FindObjectsOfType<GameObject>();
        Array.Resize(ref path, pathIds[id].Count);
        for(int i = 0; i < pathIds[id].Count; i++)
        {
            foreach (var go in gos)
            {
                if (go.name == $"GameObject ({pathIds[id][i]})")
                {
                    path[i] = go.transform;
                }
            }
        }
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
            Modal.MessageBox("Oh no!", "You have been catch!", () => {
                Application.LoadLevel(0);
            });
        }
    }
}
