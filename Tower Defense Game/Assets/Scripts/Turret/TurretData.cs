using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    public GameObject turretPrefab;
    public GameObject turretUpgradePrefab;
    public GameObject turretUpgradePrefab_Lv2;
    public int cost;
    public int costUpgrade;
    public TurretType type;
}

public enum TurretType
{
    Turret0,
    Turret1,
    Turret2,
    Turret3,
    Merge
}