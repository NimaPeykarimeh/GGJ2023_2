using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowShooting : MonoBehaviour
{
    public float power;
    public int AimDistance;
    public int cancelOffset;

    LineRenderer lr;
    Rigidbody2D rb;

    Vector2 startDragPos;
    Vector2 endDragPos;

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
            GetComponent<LineRenderer>().enabled = true;
            
            startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            
            endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;

            Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

            lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for(int i = 0; i < trajectory.Length; ++i)
            {
                positions[i] = trajectory[i];
            }

            lr.SetPositions(positions);

            if (Vector2.Distance(endDragPos, startDragPos) < cancelOffset)
                GetComponent<LineRenderer>().SetColors(Color.grey, Color.grey);
            else
                GetComponent<LineRenderer>().SetColors(Color.white, Color.white);
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (Vector2.Distance(endDragPos, startDragPos) > cancelOffset)
            {

                Vector2 endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;

                rb.velocity = _velocity;
                
            }

            GetComponent<LineRenderer>().enabled = false;
            //GetComponent<ArrowShooting>().enabled = false;
            Destroy(gameObject, 5f);
            this.enabled = false;
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
