using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class diaologManager : MonoBehaviour
{
    [Header("Dialog Settings")]
    [SerializeField] private string[] dialogs;
    [SerializeField] float writingSpeed;
    [SerializeField] string[] dialogPersonList;
    int dialogsCount = 0;

    [Header("activation")]
    [SerializeField] bool dialogActive = false;
    [SerializeField] GameObject dialogPanel;
    [SerializeField] private TextMeshProUGUI dialogText;
    [SerializeField] bool startWriting = false;

    string textToWrite;
    string currentWriting;
    float textTimer;
    int charCounter;


    void Update()
    {
        if (dialogActive)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                dialogPanel.SetActive(true);
                startWriting = true;
                currentWriting = null;
                charCounter = 0;
                if (dialogsCount == dialogs.Length)
                {
                    dialogPanel.SetActive(false);
                }
            }
        }

        if (startWriting)
        {
            textToWrite = dialogPersonList[dialogsCount] +": " +  dialogs[dialogsCount];


            textTimer += Time.deltaTime;
            if (textTimer >= writingSpeed && charCounter < textToWrite.Length)
            {
                currentWriting += textToWrite[charCounter];
                charCounter++;
                dialogText.text = currentWriting;
                textTimer = 0;

                if (charCounter == textToWrite.Length)
                {
                    dialogsCount++;
                    startWriting = false;
                }
            }
        }

        
    }
}
