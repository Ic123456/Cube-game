using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostActor : MonoBehaviour
{
    public GhostRecorder recorder;

    public float replayTimescale = 1;

    public GhostShot[] frames;

    public bool isReplaying;

    public int replayIndex = 0;
    public float replayTime = 0.0f;            // in milliseconds

    private Renderer render;




    private void Awake()
    {
        render = GetComponent<Renderer>();
    }

    public void StartReplay()
    {
        SetFrames(recorder.GetFrames());

        if (!IsReplaying())
        {
            replayIndex = 0;
            replayTime = 0;

            transform.position = frames[0].posMark;
            transform.rotation = frames[0].rotMark;

            render.enabled = true;

            isReplaying = true;

           
        }
    }

    public void StopReplay()
    {
        if (IsReplaying())
        {
            replayIndex = 0;
            replayTime = 0.0f;

            render.enabled = false;
            isReplaying = false;

           
        }
    }

    private void Update()
    {
        if (IsReplaying())
        {
            if (replayIndex < frames.Length)
            {
                GhostShot frame = frames[replayIndex];

                if (!frame.isFinal)
                {
                    if (replayTime < frame.timeMark)
                    {
                        if (replayIndex == 0)
                        {
                            replayTime = frame.timeMark;
                        }
                        else
                        {
                            DoLerp(frames[replayIndex - 1], frame);
                            replayTime += Time.smoothDeltaTime * 1000 * replayTimescale;
                        }
                    }
                    else
                    {
                        replayIndex++;
                    }
                    
                }
                else
                {
                    StopReplay();
                }
                
            }
            else
            {
                StopReplay();
            }
        }

    }

    private void DoLerp(GhostShot a, GhostShot b)
    {
        transform.position = Vector3.Lerp(a.posMark, b.posMark, Mathf.Clamp(replayTime, a.timeMark, b.timeMark));
        transform.rotation = Quaternion.Lerp(a.rotMark, b.rotMark, Mathf.Clamp(replayTime, a.timeMark, b.timeMark));
    }

    public bool IsReplaying()
    {
        return isReplaying;
    }

    public void SetFrames(GhostShot[] frames)
    {
        this.frames = frames;
    }
}