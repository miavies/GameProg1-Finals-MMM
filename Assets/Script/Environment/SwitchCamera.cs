using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public GameObject groceryCamera;
    public GameObject mainCamera;
    public int Manager;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Manager == 0)
        {
            gCam();
            Manager = 1;
        }
        else
        {
            mCam();
            Manager = 0;
        }
    }

    void gCam()
    {
        groceryCamera.SetActive(true);
        mainCamera.SetActive(false);
    }

    void mCam()
    {
        groceryCamera.SetActive(false);
        mainCamera.SetActive(true);
    }
}
