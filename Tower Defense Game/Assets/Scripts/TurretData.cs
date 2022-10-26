using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretData
{
    public GameObject turretPrefab;
    public GameObject turretUpgradePrefab;
    public int cost;
    public int costUpgrade;
    public TurretType type;
}

public enum TurretType
{
    Turret1,
    Turret2,
    Turret3
}