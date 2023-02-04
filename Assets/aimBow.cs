using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimBow : MonoBehaviour
{
    Vector2 mousePos;
    private Rigidbody2D rb2;
    [SerializeField] private Camera cam;
    private GameObject player;
    private float _playerSpeed;
    [SerializeField] GameObject arrowPrefab;
    public float[] bowPower;
    public int bowLevel;


    // Start is called before the first frame update
    void Start()
    {
        rb2 = GetComponent<Rigidbody2D>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = transform.parent.gameObject;

        _playerSpeed = player.GetComponent<playerMovement>().movementSpeed;
    }
    private void Update()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButton(0))
        {
            Vector2 lookDir = transform.GetChild(1).GetComponent<ArrowShooting>()._velocity;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            player.GetComponent<playerMovement>().movementSpeed = 0;
            rb2.rotation = angle;

        }

        if (Input.GetMouseButtonUp(0))
        {
            player.GetComponent<playerMovement>().movementSpeed = _playerSpeed;

        }
    }
    public void AddNewArrow()
    {
        
        GameObject _prefab = Instantiate(arrowPrefab, transform.GetChild(0).transform.position, transform.rotation);
        _prefab.transform.parent = transform;
    }

    void FixedUpdate()
    {
        
        
    }
}
