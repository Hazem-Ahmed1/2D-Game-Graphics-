using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneNPC : NPC
{
    public Transform firePoint;
    public GameObject stonePrefap;
    public void shoot()
    {
        Instantiate(stonePrefap,firePoint.position,firePoint.rotation);
    }
}
