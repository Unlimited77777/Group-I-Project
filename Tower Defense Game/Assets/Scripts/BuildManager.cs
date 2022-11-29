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
    

    //The current chosen turret which is going to build
    private TurretData selectedTurretData;
    //The current chosen turret which has been built
    public MapCube selectedMapCube;

    
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
                    if (selectedTurretData != Turret0)
                    {
                        if (mapCube.turretGo == null)
                        {
                            //can create
                            //According to the random integer t to choose the turret randomly
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

}
