using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject cameraSplitted;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("space");
            Debug.Log("space");
            Debug.Log("space");
            camera.SetActive(false);
            cameraSplitted.SetActive(true);
        }
    }
}
