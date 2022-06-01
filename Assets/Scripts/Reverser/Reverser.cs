using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reverser : MonoBehaviour
{
    public bool isRecording = true;

    public float time = 0f;
    public float counter = 5f;

    public List<PlaceInTime> placeInTimes;
    // Start is called before the first frame update
    void Start()
    {
        if (isRecording)
        {
            placeInTimes = new List<PlaceInTime>();
        }
        time = counter;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        time -= Time.fixedDeltaTime;
        if(time <= 0)
        {
            isRecording = false;
        }
        if (isRecording)
        {
            RecordPositons();
        }
        else
        {
            LoopBackward();
        }
    }

    void RecordPositons()
    {
        placeInTimes.Insert(0, new PlaceInTime(transform.position, transform.rotation));
    }

    void LoopBackward()
    {
        if (placeInTimes.Count > 0)
        {
            PlaceInTime temp = placeInTimes[0];
            transform.position = temp.position;
            transform.rotation = temp.rotation;
            placeInTimes.RemoveAt(0);
        }
        else
        {
            time = counter;
            isRecording = true;
        }
        
    }
}
