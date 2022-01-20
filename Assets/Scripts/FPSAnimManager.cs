using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSAnimManager : MonoBehaviour
{
    public Animation am;
    public AnimationClip walk;
    public AnimationClip idle;

    public bool isTP = false;

    public PhotonView graphicsPV;
    public Animation graphicsAM;
    public AnimationClip idleTPS;
    public AnimationClip fireTPS;
    public AnimationClip runTPS;

    public Rigidbody rb;

    private void Update()
    {
        if (rb.velocity.magnitude >= 0.1f)
        {
            if (!isTP)
            {
                playAnim(walk.name);
            }
            else
            {
                graphicsPV.RPC("playAnimPV", PhotonTargets.All, runTPS.name);
            }            
        } 
        else
        {
            if (!isTP)
            {
                playAnim(idle.name);
            }
            else
            {
                graphicsPV.RPC("playAnimPV", PhotonTargets.All, idleTPS.name);
            }           
        }
    }

    public void playAnim(string animName)
    {
        if (!isTP)
        {
            am.CrossFade(animName);
        }       
    }

    public void stopAnim()
    {
        am.Stop();
    }

    [PunRPC]
    public void playAnimPV(string animName)
    {
        if (isTP)
        {
            graphicsAM.CrossFade(animName);
        }       
    }
}
