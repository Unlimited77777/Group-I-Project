using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo;//store the turret on the current cube
    public TurretData turretData;
    public GameObject currentTurret;

    //use for build turret
    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
        currentTurret = turretData.turretPrefab;
    }

    //merge for turret
    public void MergeTurret()
    {
        if(currentTurret != turretData.turretPrefab)//for level2 turret
        {
            MergeTurretLv2();
        }
        else//for level 1 turret
        {
            Destroy(turretGo);
            turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
            currentTurret = turretData.turretUpgradePrefab;
        }
        
    }

    //use for merge the lv2 turret
    public void MergeTurretLv2()
    {   
        Destroy(turretGo);
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab_Lv2, transform.position, Quaternion.identity);
        currentTurret = turretData.turretUpgradePrefab_Lv2;
    }
     
    //destory the turret
    public void SellTurret()
    {
        Destroy(turretGo);
        turretGo = null;
        turretData = null;
    }


}
