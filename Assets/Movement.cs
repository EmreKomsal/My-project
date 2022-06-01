using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Movement : MonoBehaviour
{
    public bool isReverse = false;
    public bool isStart = false;
    public float speed = 5f;

    GameObject canvas;
    Vector3 startPoint;
    Vector3 endPoint;
    // Start is called before the first frame update
    void Start()
    {
        startPoint = GameObject.FindGameObjectWithTag("StartPoint").transform.position;
        endPoint = GameObject.FindGameObjectWithTag("EndPoint").transform.position;
        canvas = GameObject.FindGameObjectWithTag("Canvas");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Buna ihtiyaç olmayabilir
        /*
        if (Input.GetButton("Fire1"))
        {
            isReverse = !isReverse;
        }
        */

        if (Input.GetButton("Fire2") && !isStart)
        {
            isStart = true;
        }
        if (isStart){
            if(!isReverse){
                float remaining_dis = endPoint.z - transform.position.z;
                if(remaining_dis > 0){
                    transform.position += Vector3.forward * speed * Time.deltaTime;
                }
                else
                {
                    isStart = false;
                    StartCoroutine(SwitchTo2D());
                }
            }
            else if (isReverse)
            {
                float remaining_dis = startPoint.z - transform.position.z;
                if(remaining_dis < 0){
                transform.position += Vector3.back * speed * Time.deltaTime;
                }
                else
                {
                    isStart = false;
                    StartCoroutine(SwitchTo2D());
                }
            }
        }

    }

    public void StartScene()
    {
        if (isReverse)
        {
            endPoint.y = transform.position.y;
            transform.position = endPoint;
        }
        else
        {
            startPoint.y = transform.position.y;
            transform.position = startPoint;
        }
    }

    // Call via `StartCoroutine(SwitchTo2D())` from your code. Or, use
    // `yield SwitchTo2D()` if calling from inside another coroutine.
    IEnumerator SwitchTo2D()
    {
        // Empty string loads the "None" device.
        XRSettings.LoadDeviceByName("");

        // Must wait one frame after calling `XRSettings.LoadDeviceByName()`.
        yield return null;

        // Not needed, since loading the None (`""`) device takes care of this.
        // XRSettings.enabled = false;


        canvas.SetActive(true);
        // Restore 2D camera settings.
        ResetCameras();
    }

    // Resets camera transform and settings on all enabled eye cameras.
    void ResetCameras()
    {
        // Camera looping logic copied from GvrEditorEmulator.cs
        for (int i = 0; i < Camera.allCameras.Length; i++)
        {
            Camera cam = Camera.allCameras[i];
            if (cam.enabled && cam.stereoTargetEye != StereoTargetEyeMask.None)
            {

                // Reset local position.
                // Only required if you change the camera's local position while in 2D mode.
                cam.transform.localPosition = Vector3.zero;

                // Reset local rotation.
                // Only required if you change the camera's local rotation while in 2D mode.
                cam.transform.localRotation = Quaternion.identity;

                // No longer needed, see issue github.com/googlevr/gvr-unity-sdk/issues/628.
                // cam.ResetAspect();

                // No need to reset `fieldOfView`, since it's reset automatically.
            }
        }
    }

}
