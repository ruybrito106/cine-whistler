using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whistler : MonoBehaviour
{

    public float speed = 0.4f;
    public float step = 0.2f;
    private Vector2 dest = Vector2.zero;
    private ParticleSystem particle;
    private bool touchStart = false;
    private Vector2 point;
    public Transform touch;
    public Transform circle;

    public Transform whistle;

    void Start()
    {
        dest = transform.position;
        particle = GetComponentInChildren<ParticleSystem>();
        if (particle.isPlaying)
        {
            particle.Stop();
        }
    }

    void Update () {
        if (Input.GetMouseButton(0)) {
            touchStart = true;
            point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        } else {
            touchStart = false;
        }
	}

    void FixedUpdate()
    {
        Vector2 p = Vector2.MoveTowards(transform.position, dest, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);

        if (touchStart) {
            Vector2 offsetToWhistle = point - ((Vector2) whistle.position);
            if (offsetToWhistle.magnitude <= 1.0f) {
                if (!particle.isPlaying) particle.Play();
                return;
            }
            Vector2 offsetToJoystick = point - ((Vector2) circle.position);
            if (offsetToJoystick.magnitude <= 3.0f) {
                Vector2 direction = Vector2.ClampMagnitude(offsetToJoystick, 1.0f);
                touch.GetComponent<SpriteRenderer>().enabled = true;
                touch.transform.position = new Vector2(circle.position.x + direction.x, circle.position.y + direction.y);
                if (offsetToJoystick.x <= -0.5) {
                    OnMoveLeft();
                } else if (offsetToJoystick.x >= 0.5) {
                    OnMoveRight();
                } else if (offsetToJoystick.y <= -0.5) {
                    OnMoveDown();
                } else if (offsetToJoystick.y >= 0.5) {
                    OnMoveUp();
                }
            }
        } else {
            touch.GetComponent<SpriteRenderer>().enabled = false;
            particle.Stop();
        }
    }

    bool valid(Vector2 dir)
    {
        Vector2 pos = transform.position;
        RaycastHit2D hit = Physics2D.Linecast(pos + dir, pos);
        return (hit.collider == GetComponent<Collider2D>());
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