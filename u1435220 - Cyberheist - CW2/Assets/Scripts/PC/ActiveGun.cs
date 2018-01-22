﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class ActiveGun : NetworkBehaviour
{

    public Transform gunPosition;
    public Weapon weapon;
    public Weapon pickup;
    public Weapon fallback;

    public GameObject missilePrefab;
    public GameObject missileEject;

    public bool triggerPulled = false;

    float coolDown = 0;

    public int currentAmmo;
    public int currentClipSize;

    // UI Params
    public static TextMeshProUGUI nameText;
    public static TextMeshProUGUI clipText;
    public static TextMeshProUGUI currentClipText;
    public static TextMeshProUGUI ammoText;

    // Use this for initialization
    void Start()
    {

        if (!isLocalPlayer)
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (!isLocalPlayer)
        {
            return;
        }

        if (weapon == null)
        {
            if (pickup != null)
            {
                weapon = pickup;
                pickup = null;
            }
            else if (fallback != null) weapon = fallback;
            else return;

            weapon = (Weapon)Instantiate(weapon, gunPosition.position, gunPosition.rotation);
            weapon.transform.parent = transform;
        }

        //if (GO_currentGun == null)
        //{
        //    GO_currentGun = GO_defaultGun;
        //    currentGun = GO_currentGun.GetComponent<Weapon>();
        //}
        //else if (GO_currentGun.GetComponent<Weapon>().empty)
        //{
        //    Destroy(GO_currentGun);
        //}

        //GO_currentGun.transform.position = gunPosition.position;

        //currentGun.CheckAmmo();

        //if (triggerPulled == true && currentGun.clipEmpty == false)
        //{
        //    if (coolDown < Time.time)
        //    {
        //        Cmd_FireBullet();
        //        coolDown = currentGun.ApplyCooldown(coolDown);
        //    }
        //}

        //UpdateUI();
    }

    //void AcquireNewGun(GameObject newGun)
    //{
    //    GO_currentGun = newGun;
    //    currentGun = GO_currentGun.GetComponent<Weapon>();
    //}

    //void UpdateUI()
    //{
    //    nameText.text = currentGun.gun.name;
    //    clipText.text = string.Format("" + currentGun.gun.clipSize);
    //    currentClipText.text = string.Format("" + currentGun.currentClipSize);
    //    ammoText.text = string.Format("" + currentGun.currentAmmo);
    //}

    public void Shoot(KeyCode shoot)
    {
        if (Input.GetKeyDown(shoot)) weapon.shoot = true;
        else weapon.shoot = false;
    }

    public void Missile()
    {
        Cmd_FireMissile();
    }

    [Command]
    void Cmd_FireMissile()
    {
        var _bullet = (GameObject)Instantiate(missilePrefab,
            missileEject.transform.position,
            missileEject.transform.rotation);

        NetworkServer.Spawn(_bullet);
    }
}