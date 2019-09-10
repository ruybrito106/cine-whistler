using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGuard : MonoBehaviour
{

    public float speed = 0.08f;

    private float latestDirectionChangeTime;
    private readonly float directionChangeTime = 1f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    static private List<Vector2> directions = new List<Vector2> { new Vector2(0.2f, 0), new Vector2(0, 0.2f), new Vector2(-0.2f, 0), new Vector2(0, -0.2f) };

    void NewMovement()
    {
        movementDirection = directions[Random.Range(0, directions.Count)];
    }

    void Start()
    {
        latestDirectionChangeTime = 0f;
        NewMovement();
    }

    void FixedUpdate()
    {
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            NewMovement();
        }

        Vector2 newPosition = new Vector2(
            transform.position.x + (movementDirection.x * Time.deltaTime),
            transform.position.y + (movementDirection.y * Time.deltaTime));

        Vector2 p = Vector2.MoveTowards(transform.position, newPosition, speed);
        GetComponent<Rigidbody2D>().MovePosition(p);
    }

    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.name == "Whistler")
        {
            Destroy(collision.gameObject);
        }
        else if (collision.name == "Guard_0")
        {
            return;
        }
        else
        {
            NewMovement();
        }
    }
}
