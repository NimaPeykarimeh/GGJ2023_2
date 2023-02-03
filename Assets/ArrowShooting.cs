using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooting : MonoBehaviour
{
    public float power;
    public int AimDistance;
    LineRenderer lr;
    Rigidbody2D rb;

    Vector2 startDragPos;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //startDragPos = transform.position;
            startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            
            Vector2 endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;

            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

            lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for(int i = 0; i < trajectory.Length; ++i)
            {
                positions[i] = trajectory[i];
            }

            lr.SetPositions(positions);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;

            rb.velocity = _velocity;
        }
    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps];

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;

        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < steps; ++i)
        {

                moveStep += gravityAccel;
                moveStep *= drag;

                pos += moveStep;
                results[i] = pos;
        }

        return results;
    }

    private void FixedUpdate()
    {
        
    }
}
