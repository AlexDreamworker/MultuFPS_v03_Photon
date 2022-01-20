using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSWeapon : MonoBehaviour
{
    public Camera fpsCam;
    public GameObject hitParticle;

    public int damage = 30;
    public int range = 1000;
    public int ammo = 10;
    public int clipSize = 10;
    public int clipCount = 5;
    public float recoilPower = 50f;

    public Animation anim;
    public AnimationClip shootAnim;
    public AnimationClip reloadAnim;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            fireShot();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public void fireShot()
    {
        if (!anim.IsPlaying(reloadAnim.name) && ammo >=1)
        {
            if (!anim.IsPlaying(shootAnim.name))
            {
                anim.CrossFade(shootAnim.name);
                ammo = ammo - 1;

                fpsCam.transform.Rotate(Vector3.right, -recoilPower * Time.deltaTime);

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

    public void Reload()
    {
        if (clipCount >= 1)
        {
            anim.CrossFade(reloadAnim.name);
            ammo = clipSize;
            clipCount = clipCount - 1;
        }       
    }

    private void OnGUI()
    {
        //GUI.Box(new Rect(100, 100, 150, 30), "Ammo: " + ammo + "/" + clipSize + "/" + clipCount);
        GUIStyle styleTime = new GUIStyle();
        styleTime.fontSize = 45;
        GUI.Box(new Rect(100, 100, 150, 30), "Ammo: " + ammo + "/" + clipSize + "/" + clipCount, styleTime);
    }
}
