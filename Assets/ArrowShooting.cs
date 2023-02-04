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
    [SerializeField] GameObject _camera;
    [SerializeField] float multi;
    [SerializeField] float _multi;
    [SerializeField] Vector2 _cameraXoffsetRange;
    [SerializeField] Vector2 _cameraYoffsetRange;
    [SerializeField] AudioClip shootingSound;
    [SerializeField] AudioClip arrowHit;
    AudioSource _audioSource;
    // Start is called before the first frame update
    void Start()
    {
        lr = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody2D>();
        _camera = GameObject.FindGameObjectWithTag("MainCamera");
        power = transform.parent.gameObject.GetComponent<aimBow>().bowPower[transform.parent.gameObject.GetComponent<aimBow>().bowLevel - 1];
        _audioSource= GetComponent<AudioSource>();
        _audioSource.clip= shootingSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (attackFlag)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GetComponent<LineRenderer>().enabled = true;
                _camera.GetComponent<CameraScripts>().isShooting = true;
                startDragPos = Input.mousePosition/_multi;
            }

            if (Input.GetMouseButton(0))
            {

                


                endDragPos = Input.mousePosition / _multi;

                _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;
                
                Vector2[] trajectory = Plot(rb, (Vector2)transform.position, _velocity, 500);

                


                lr.positionCount = trajectory.Length;

                Vector3[] positions = new Vector3[trajectory.Length];
                for (int i = 0; i < trajectory.Length; ++i)
                {
                    positions[i] = trajectory[i];
                }

                _camera.GetComponent<CameraScripts>().cameraOffset = (positions[positions.Length -1] - positions[0]) / multi;
                _camera.GetComponent<CameraScripts>().cameraOffset.x = Mathf.Clamp(_camera.GetComponent<CameraScripts>().cameraOffset.x, _cameraXoffsetRange[0], _cameraXoffsetRange[1]);
                _camera.GetComponent<CameraScripts>().cameraOffset.y = Mathf.Clamp(_camera.GetComponent<CameraScripts>().cameraOffset.y, _cameraYoffsetRange[0], _cameraYoffsetRange[1]);

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
                    _audioSource.Play();
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.parent.GetComponent<aimBow>().AddNewArrow();
                    transform.parent = null;
                    Vector2 endDragPos = Input.mousePosition/ _multi;
                    Vector2 _velocity = Vector2.ClampMagnitude((endDragPos - startDragPos), AimDistance) * -power;
                    rb.bodyType = RigidbodyType2D.Dynamic;
                    rb.velocity = _velocity;
                    Destroy(gameObject, 15f);
                    //this.enabled = false;
                    attackFlag = false;
                }
                _camera.GetComponent<CameraScripts>().isShooting = false;
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
            _audioSource.clip = arrowHit;
            _audioSource.Play();
            transform.GetChild(0).gameObject.SetActive(false);
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
