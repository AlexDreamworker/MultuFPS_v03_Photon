using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSAnimManager : MonoBehaviour
{
    public Animation am;
    public AnimationClip walk;
    public AnimationClip idle;

    public Rigidbody rb;

    private void Update()
    {
        if (rb.velocity.magnitude >= 0.1f)
        {
            playAnim(walk.name);
        } 
        else
        {
            playAnim(idle.name);
        }
    }

    public void playAnim(string animName)
    {
        am.CrossFade(animName);
    }

    public void stopAnim()
    {
        am.Stop();
    }
}
