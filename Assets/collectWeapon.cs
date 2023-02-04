using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class collectWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponPrefab;
    [SerializeField] GameObject _player;
    [SerializeField] bool isCollectible;
    [SerializeField] string textToShow;
    [SerializeField] string textInPanel;
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] GameObject infoPanel;
    float textTimer;
    [SerializeField] float writingSpeed;
    int charCounter;
    // Start is called before the first frame update
    void Start()
    {
        infoText = GameObject.FindGameObjectWithTag("informationText").GetComponent<TextMeshProUGUI>();
        infoPanel = GameObject.FindGameObjectWithTag("infoPanel");
        infoPanel.SetActive(false);
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //infoText.text = textToShow;
            infoPanel.SetActive(true);
            _player = collision.gameObject;
            isCollectible = true;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isCollectible = false;
            textInPanel = null;
            charCounter = 0;
            infoText.text = textInPanel;
            infoPanel.SetActive(false);

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isCollectible)
        {
            textTimer += Time.deltaTime;
            if (textTimer >= writingSpeed && charCounter < textToShow.Length)
            {
                textInPanel += textToShow[charCounter];
                charCounter++;
                infoText.text = textInPanel;
                textTimer = 0;

            }
        }

        if (isCollectible && Input.GetKeyDown(KeyCode.R))
        {
            
            GameObject _addedPrefab = Instantiate(weaponPrefab, _player.transform.Find("el").transform.Find("weapons").transform.position, transform.rotation);
            _addedPrefab.transform.parent = GameObject.FindGameObjectWithTag("weaponHolder").transform;
            
            for (int i = 0; i < GameObject.FindGameObjectWithTag("weaponHolder").transform.childCount; i++)
            {
                if (GameObject.FindGameObjectWithTag("weaponHolder").transform.GetChild(i).gameObject.activeSelf)
                {
                    GameObject.FindGameObjectWithTag("weaponHolder").transform.GetChild(i).gameObject.SetActive(false);
                }
            }

            
            _addedPrefab.SetActive(true);
            infoPanel.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}
