using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicManager : MonoBehaviour
{
    [SerializeField] AudioClip musicAudio;
    AudioSource _audipSource;
    // Start is called before the first frame update
    void Start()
    {
        _audipSource= GetComponent<AudioSource>();
        _audipSource.clip= musicAudio;
        _audipSource.Play();
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
