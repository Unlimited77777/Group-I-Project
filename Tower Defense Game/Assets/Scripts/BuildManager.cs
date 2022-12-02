using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class BuildManager : MonoBehaviour
{
    public TurretData Turret0;
    public TurretData Turret1;
    public TurretData Turret2;
    public TurretData Turret3;
    public TurretData Merge;


    //The current chosen turret which is going to build
    private TurretData selectedTurretData;
    //The current chosen turret by left button of the mouse which has been built
    public MapCube selectedMapCube;
    //The current chosen turret by right button of the mouse which has been built
    public MapCube selectedMapCubeMerge;


    public Text moneyText;
    public Animator moneyAnimator;
    public int money = 500;
    public int sellprice;
    public void UpdateMoney(int change)
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
                //Create a random integer to controll the type of turret
                Random rnd = new Random();
                int t = rnd.Next(1, 4);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 2000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //get the mapcube which is click by mouse
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData == Merge)//to get the data for merge
                    {
                        selectedMapCube = mapCube;
                    }
                    else if (selectedTurretData == Turret0)//to do sell
                    {
                        selectedMapCube = mapCube;
                        if (mapCube.turretGo != null)
                        {
                            sellprice = (int)(0.7 * selectedMapCube.turretData.cost);
                            UpdateMoney(+sellprice);
                            selectedMapCube.SellTurret();
                        }
                    }
                    else
                    {
                        if (mapCube.turretGo == null)
                        {
                            //can create
                            //According to the random integer t to choose the turret randomly
                            if (t == 1)
                            {
                                selectedTurretData = Turret1;
                            }
                            else if (t == 2)
                            {
                                selectedTurretData = Turret2;
                            }
                            else
                            {
                                selectedTurretData = Turret3;
                            }
                            if (money >= selectedTurretData.cost)
                            {
                                UpdateMoney(-selectedTurretData.cost);
                                mapCube.BuildTurret(selectedTurretData);
                            }
                            else//do not have enough money
                            {
                                moneyAnimator.SetTrigger("Flicker");
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {       
                Ray ray2 = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit2;
                bool isCollider2 = Physics.Raycast(ray2, out hit2, 2000, LayerMask.GetMask("MapCube"));
                if (isCollider2)
                {
                    //get the mapcube which is click right button of mouse
                    MapCube mapCube2 = hit2.collider.GetComponent<MapCube>();
                    if (selectedTurretData == Merge)
                    {
                        selectedMapCubeMerge = mapCube2;
                        if (selectedMapCubeMerge.turretData.turretPrefab == selectedMapCube.turretData.turretPrefab)
                        {//do the merge
                            selectedMapCube.MergeTurret();
                            selectedMapCubeMerge.SellTurret();
                        }
                    }
                }
            }
        }
    }

    public void OnTurretRSelected(bool ison)
    {
        Random rnd = new Random();
        int t = rnd.Next(1, 4);
        if(ison)
        {
            //The first turret we choose is also random
            if(t == 1)
            {
                selectedTurretData = Turret1;
            }
            else if(t == 2)
            {
                selectedTurretData = Turret2;
            }
            else
            {
                selectedTurretData = Turret3;
            }
        }
    }


    public void OnSellSelected(bool ison)
    {
        if (ison)
        {
            selectedTurretData = Turret0;
        }
    }

    public void OnMergeSelected(bool ison)
    {
        if (ison)
        {
            selectedTurretData = Merge;
        }
    }
}
