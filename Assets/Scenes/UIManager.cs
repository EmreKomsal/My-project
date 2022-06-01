using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UIManager : MonoBehaviour
{
    public Movement movement;

    private void Start()
    {
        Debug.Log(XRSettings.loadedDeviceName);
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
    }

    public void ForwardButton()
    {
        movement.isReverse = false;
        SceneProcess();
    }

    public void BackwardButton()
    {
        movement.isReverse = true;
        SceneProcess();
    }

    public void SceneProcess()
    {
        movement.StartScene();
        StartCoroutine(SwitchToVR());
    }

    IEnumerator SwitchToVR()
    {
        // Device names are lowercase, as returned by `XRSettings.supportedDevices`.
        string desiredDevice = "cardboard"; // Or "cardboard".

        Debug.Log("heelllo");

        // Some VR Devices do not support reloading when already active, see
        // https://docs.unity3d.com/ScriptReference/XR.XRSettings.LoadDeviceByName.html
        if (String.Compare(XRSettings.loadedDeviceName, desiredDevice, true) != 0)
        {
            XRSettings.LoadDeviceByName(desiredDevice);


            // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
            yield return null;

            
        }

        // Now it's ok to enable VR mode.
        XRSettings.enabled = true;
        gameObject.SetActive(false);
    }

}
