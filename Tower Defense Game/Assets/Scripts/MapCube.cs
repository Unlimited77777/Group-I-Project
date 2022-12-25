using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour
{
    [HideInInspector]
    public GameObject turretGo;//store the turret on the current cube
    public TurretData turretData;

    public void BuildTurret(TurretData turretData)
    {
        this.turretData = turretData;
        turretGo = GameObject.Instantiate(turretData.turretPrefab, transform.position, Quaternion.identity);
    }

    public void MergeTurret()
    {   
        Destroy(turretGo);
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab, transform.position, Quaternion.identity);
    }

    public void MergeTurretLv2()
    {   
        Destroy(turretGo);
        turretGo = GameObject.Instantiate(turretData.turretUpgradePrefab_Lv2, transform.position, Quaternion.identity);
    }
     
    public void SellTurret()
    {
        Destroy(turretGo);
        turretGo = null;
        turretData = null;
    }


}
