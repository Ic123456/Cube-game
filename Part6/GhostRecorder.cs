using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class GhostRecorder : MonoBehaviour
{  // effect
    public Volume volume;

    public GhostShot[] frames;
    
    public bool isRecording;

    public int recordIndex = 0;
    public float recordTime = 0.0f;            // in milliseconds


    public void StartRecording(float duration)
    {
        if (!IsRecording())
        {
            frames = new GhostShot[(int)(60 * duration)];
            recordIndex = 0;
            recordTime = Time.time * 1000;

            isRecording = true;
            volume.weight = 1;
        }
    }

    public void StopRecording()
    {
        if (IsRecording())
        {
            frames[recordIndex - 1].isFinal = true;

            isRecording = false;
            volume.weight = 0;
        }
    }

    void Update()
    {
        if (IsRecording())
        {
            RecordFrame();
            
        }
     
    }

    private void RecordFrame()
    {
        if (recordIndex < frames.Length)
        {
            recordTime += Time.smoothDeltaTime * 1000;
            GhostShot newFrame = new GhostShot()
            {
                timeMark = recordTime,
                posMark = transform.position,
                rotMark = transform.rotation
            };

            frames[recordIndex] = newFrame;

            recordIndex++;
            
        }
        else
        {
            StopRecording();
        }
    }

    public bool IsRecording()
    {
        return isRecording;
    }

    public GhostShot[] GetFrames()
    {
        return frames;
    }
}