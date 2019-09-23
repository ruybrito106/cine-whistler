using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistler : MonoBehaviour
{

    public float speed = 0.4f;
    public float step = 0.2f;
    public float swipe_sensibility = 20f;

    private Vector2 dest = Vector2.zero;
    private ParticleSystem particle;

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    void Start()
    {
        dest = transform.position;
        particle = GetComponentInChildren<ParticleSystem>();
        if (particle.isPlaying)
        {
            particle.Stop();
        }
    }

    void FixedUpdate()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            OnMoveUp();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            OnMoveRight();
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            OnMoveDown();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            OnMoveLeft();
        }
        else if (Input.GetKey(KeyCode.Space))
        {
            if (particle.isPlaying)
            {
                particle.Stop();
            }
            else
            {
                particle.Play();
            }
        }

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }
            if (touch.phase == TouchPhase.Moved)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
    }

    void CheckSwipe()
    {
        if (VerticalMove() > swipe_sensibility && VerticalMove() > HorizontalValMove())
        {
            if (fingerDown.y - fingerUp.y > 0)
            {
                OnMoveUp();
            }
            else if (fingerDown.y - fingerUp.y < 0)
            {
                OnMoveDown();
            }
            fingerUp = fingerDown;
        }

        else if (HorizontalValMove() > swipe_sensibility && HorizontalValMove() > VerticalMove())
        {
            if (fingerDown.x - fingerUp.x > 0)
            {
                OnMoveRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)
            {
                OnMoveLeft();
            }
            fingerUp = fingerDown;
        }
    }

    float VerticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float HorizontalValMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    void OnMoveUp()
    {
        dest = (Vector2)transform.position + step * Vector2.up;
        transform.eulerAngles = new Vector3(0, 0, 0);
        particle.Stop();
    }

    void OnMoveDown()
    {
        dest = (Vector2)transform.position - step * Vector2.up;
        transform.eulerAngles = new Vector3(0, 0, 180);
        particle.Stop();
    }

    void OnMoveLeft()
    {
        dest = (Vector2)transform.position + step * Vector2.left;
        transform.eulerAngles = new Vector3(0, 0, 90);
        particle.Stop();
    }

    void OnMoveRight()
    {
        dest = (Vector2)transform.position + step * Vector2.right;
        transform.eulerAngles = new Vector3(0, 0, 270);
        particle.Stop();
    }
}
