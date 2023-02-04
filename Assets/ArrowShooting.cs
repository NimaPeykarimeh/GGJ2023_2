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

    bool attackFlag = true;
    Vector2 startDragPos;
    Vector2 endDragPos;
    public Vector2 _velocity;

    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();

        power = transform.parent.gameObject.GetComponent<aimBow>().bowPower[transform.parent.gameObject.GetComponent<aimBow>().bowLevel - 1];
    }

    // Update is called once per frame
    void Update()
    {
        if (attackFlag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<LineRenderer>().enabled = true;

                startDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {

                endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;

                Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

                lr.positionCount = trajectory.Length;

                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < trajectory.Length; ++i)
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

                    transform.parent.GetComponent<aimBow>().AddNewArrow();
                    transform.parent = null;
                    Vector2 endDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.velocity = _velocity;
                    Destroy(gameObject, 3f);
                    //this.enabled = false;
                    attackFlag = false;
                }

                GetComponent<LineRenderer>().enabled = false;
                //GetComponent<ArrowShooting>().enabled = false;
            }
            
        }
        else
        {   if (rb.velocity.magnitude > Vector2.zero.magnitude)
            {
                float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
                rb.rotation = angle;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!attackFlag && !collision.gameObject.CompareTag("Arrow"))
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
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
