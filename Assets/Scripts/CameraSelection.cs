using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelection : MonoBehaviour
{
    public GameObject virtualCamera1;
    public GameObject virtualCamera2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cameraTransition();
    }

    void cameraTransition()
    {

        switch (Input.inputString)
        {
            case "e":
                virtualCamera1.SetActive(true);
                virtualCamera2.SetActive(false);
                break;
            case "r":
                virtualCamera1.SetActive(false);
                virtualCamera2.SetActive(true);
                break;
        }
    }
}
