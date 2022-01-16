using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSWeapon : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject hitParticle;

    public int damage = 30;
    public int range = 1000;

    public Animation anim;
    public AnimationClip shootAnim;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            fireShot();
        }
    }

    public void fireShot()
    {
        if (!anim.IsPlaying(shootAnim.name))
        {
            anim.CrossFade(shootAnim.name);

            RaycastHit hit;
            Ray ray = fpsCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

            if (Physics.Raycast(ray, out hit, range))
            {
                if (hit.transform.tag == "Player")
                {
                    hit.transform.GetComponent<PhotonView>().RPC("applyDamage", PhotonTargets.AllBuffered, damage);
                }
                GameObject particleClone;
                particleClone = PhotonNetwork.Instantiate(hitParticle.name, hit.point, Quaternion.LookRotation(hit.normal), 0) as GameObject;
                Destroy(particleClone, 2);
            }
        }        
    }
}
