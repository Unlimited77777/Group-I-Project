using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour
{

    public TurretData Turret1;
    public TurretData Turret2;
    public TurretData Turret3;

    //The current chosen turret which is going to build
    private TurretData selectedTurretData;

    public Text moneyText;
    public Animator moneyAnimator;
    private int money = 500;
    void UpdateMoney(int change)
    {
        money += change;
        moneyText.text = "$" + money;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //the build of turret
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 2000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //get the mapcube which is click by mouse
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (mapCube.turretGo == null)
                    {
                        //can create
                        if (money >= selectedTurretData.cost)
                        {   
                            UpdateMoney(-selectedTurretData.cost);
                            mapCube.BuildTurret(selectedTurretData.turretPrefab);
                            
                        }
                        else//do not have enough money
                        {
                            moneyAnimator.SetTrigger("Flicker");
                        }
                    }
                    else//to do level up
                    {

                    }
                }
            }
        }
    }
    public void OnTurret1Selected(bool ison)
    {
        if (ison)
        {
            selectedTurretData = Turret1;
        }
    }
    public void OnTurret2Selected(bool ison)
    {
        if (ison)
        {
            selectedTurretData = Turret2;
        }
    }

    public void OnTurret3Selected(bool ison)
    {
        if (ison)
        {
            selectedTurretData = Turret3;
        }
    }
}
