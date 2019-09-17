using System.Collections.Generic;
using UnityEngine;

public class RandomGuard : MonoBehaviour
{

    public Transform[] path;
    public float speed = 0.08f;

    private int cur = 44;
    private float eps = 0.01f;
    private readonly float offset = 0.2f;

    private List<int> lastUsed = new List<int>{ 44 };

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

    //private float DistanceToRef(Vector3 refs, Transform x)
    //{
    //    return Mathf.Sqrt(Mathf.Pow(Mathf.Abs(refs.x - x.position.x), 2f) + Mathf.Pow(Mathf.Abs(refs.y - x.position.y), 2f));
    //}

    //private void SortPath(Vector3 refs)
    //{
    //    for (int i = 0; i < path.Length; i++)
    //    {
    //        for (int j = i + 1; j < path.Length; j++)
    //        {
    //            if (DistanceToRef(refs, path[j]) < DistanceToRef(refs, path[i]))
    //            {
    //                Transform tmp = path[i];
    //                path[i] = path[j];
    //                path[j] = tmp;
    //            }
    //        }
    //    }
    //}

    private int DecideNext(Vector3 position)
    {
        for (int i = 0; i < path.Length; i++)
        {
            if (lastUsed.Contains(i))
            {
                continue;
            }

            RaycastHit2D hit = Physics2D.Linecast((position + path[i].position) * 0.5f, path[i].position);
            if (hit.collider == null)
            {
                return i;
            }

            if (hit.collider?.name?.Contains("Chair") == true || hit.collider == GetComponent<BoxCollider2D>())
            {
                continue;
            } 

            return i;
        }

        return 0;
    }

    void FixedUpdate()
    {
        if (WithinMargin(transform.position.x, path[cur].position.x) && WithinMargin(transform.position.y, path[cur].position.y))
        {
            int next = DecideNext(path[cur].position);
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
            lastUsed.Add(cur);
            if (lastUsed.Count == 15)
            {
                lastUsed.RemoveAt(0);
            }

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
