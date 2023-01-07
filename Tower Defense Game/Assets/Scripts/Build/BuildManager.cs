using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Random = System.Random;

public class BuildManager : MonoBehaviour
{
    public TurretData Turret0;
    public TurretData Merge;
    public TurretData[] turrets = new TurretData[3];
    


    //The current chosen turret which is going to build
    private TurretData selectedTurretData;
    //The current chosen turret by left button of the mouse which has been built
    public MapCube selectedMapCube;
    //Random number
    public int random;


    public Text moneyText;
    public Animator moneyAnimator;
    public int money = 500;
    public int sellprice;
    
    

    public void UpdateMoney(int change)//Update the money when the enemy died
    {
        money += change;
        moneyText.text = "$" + money;
    }
    private void Update()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject()==false)
            {
                //the build of turret
                //Create a random integer to controll the type of turret
                
                Ray ray = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 2000, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    //get the mapcube which is click by mouse
                    MapCube mapCube = hit.collider.GetComponent<MapCube>();
                    if (selectedTurretData == Merge)//to get the data for merge
                    {
                        selectedMapCube = mapCube;
                        print(selectedMapCube);
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
                            if (money >= selectedTurretData.cost)
                            {
                                UpdateMoney(-selectedTurretData.cost);
                                mapCube.BuildTurret(selectedTurretData);
                                Random rnd = new Random();
                                random = rnd.Next(0, turrets.Length);
                                selectedTurretData = turrets[random];
                                print(random);
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

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)
            {       
                Ray ray2 = Camera.main.ScreenPointToRay(mousePosition);
                RaycastHit hit2;
                bool isCollider2 = Physics.Raycast(ray2, out hit2, 2000, LayerMask.GetMask("MapCube"));
                if (isCollider2)
                {
                    //get the mapcube which is click right button of mouse
                    MapCube mapCube2 = hit2.collider.GetComponent<MapCube>();
                    if (selectedTurretData == Merge)
                    {
                        if(mapCube2 != selectedMapCube)
                        {
                            if (mapCube2.currentTurret == selectedMapCube.currentTurret)
                            {//do the merge
                                print("merge1 + " + mapCube2.turretGo);
                                selectedMapCube.MergeTurret();
                                mapCube2.SellTurret();
                            }
                        }
                        else
                        {
                            print("same");
                        }
                        
                    }
                }
            }
        }
    }

    public void OnTurretRSelected(bool ison)//the build of turret button selected
    {
        Random rnd = new Random();
        int random = rnd.Next(0, turrets.Length);
        if(ison)
        {
            //The first turret we choose is also random
            selectedTurretData = turrets[random];
        }
    }


    public void OnSellSelected(bool ison)//the sell button selected
    {
        if (ison)
        {
            selectedTurretData = Turret0;
        }
    }

    public void OnMergeSelected(bool ison)//the merge button selected
    {
        if (ison)
        {
            selectedTurretData = Merge;
        }
    }
}
