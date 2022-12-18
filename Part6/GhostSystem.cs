using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEditor.Rendering.Universal;


public class GhostSystem : MonoBehaviour
{
    public Volume volume;
    private GhostRecorder[] recorders;
    private GhostActor[] ghostActors;

   

    public Transform playerControlled;
    public Transform playerGhost;

    public float recordDuration = 10;

    private void Start()
    {
        recorders = FindObjectsOfType<GhostRecorder>();
        ghostActors = FindObjectsOfType<GhostActor>();
      
    }

    public void StartRecording()
    {  
            recorders[0].StartRecording(recordDuration);
            OnRecordingStart();
    }

    public void StopRecording()
    {    
        recorders[0].StopRecording();
       
        OnRecordingEnd();
    }

    public void StartReplay()
    {      
        ghostActors[0].StartReplay();
        OnReplayStart();
    }

    public void StopReplay()
    {      
            ghostActors[0].StopReplay();    
            OnReplayEnd();
    }

    #region Event Handlers
    public event EventHandler RecordingStarted;
    public event EventHandler RecordingEnded;
    public event EventHandler ReplayStarted;
    public event EventHandler ReplayEnded;
    #endregion

    #region Event Invokers
    protected virtual void OnRecordingStart()
    {
        if (RecordingStarted != null)
        {
            RecordingStarted.Invoke(this, EventArgs.Empty);
        }
    }

    protected virtual void OnRecordingEnd()
    {
        if (RecordingEnded != null)
        {
            RecordingEnded.Invoke(this, EventArgs.Empty);
        }
    }


    protected virtual void OnReplayStart()
    {
        if (ReplayStarted != null)
        {
            ReplayStarted.Invoke(this, EventArgs.Empty);
        }
    }

    protected virtual void OnReplayEnd()
    {
        if (ReplayEnded != null)
        {
            ReplayEnded.Invoke(this, EventArgs.Empty);
        }
    }
    #endregion
    #region
    private bool switch1 = true;
    private bool switch2 = true;
    private void Update()
    {
       
        


        if (Input.GetKeyDown(KeyCode.P))
        {
            if (switch1)
            {
                StartRecording();
                
            }
            else
            {
                StopRecording();
               
            }
            switch1 = !switch1;
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            if (switch2)
            {
                StartReplay();
            }
            else
            {
                StopReplay();
            }
            switch2 = !switch2;
        }
    }
    #endregion
}