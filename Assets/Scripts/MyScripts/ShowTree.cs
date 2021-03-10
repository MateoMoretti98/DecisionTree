using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowTree : MonoBehaviour
{
    public GameObject nightOrDay;
    public GameObject stamina;
    public GameObject raining;

    public GameObject sleep;
    public GameObject harvestFood;
    public GameObject getWood;

    public GameObject sleepRight;
    public GameObject haveFood;
    public GameObject harvestFoodRight;
    public GameObject haveWood;
    public GameObject getWoodRight;
    public GameObject buildHouse;

    void Start()
    {
        Debug.DrawLine(nightOrDay.transform.position, stamina.transform.position, Color.green, 100f);
        Debug.DrawLine(nightOrDay.transform.position, raining.transform.position, Color.green, 100f);

        Debug.DrawLine(stamina.transform.position, sleep.transform.position, Color.green, 100f);
        Debug.DrawLine(stamina.transform.position, harvestFood.transform.position, Color.green, 100f);
        Debug.DrawLine(stamina.transform.position, getWood.transform.position, Color.green, 100f);
        Debug.DrawLine(stamina.transform.position, sleepRight.transform.position, Color.green, 100f);

        Debug.DrawLine(raining.transform.position, sleepRight.transform.position, Color.green, 100f);
        Debug.DrawLine(raining.transform.position, haveFood.transform.position, Color.green, 100f);

        Debug.DrawLine(haveFood.transform.position, harvestFoodRight.transform.position, Color.green, 100f);
        Debug.DrawLine(haveFood.transform.position, haveWood.transform.position, Color.green, 100f);

        Debug.DrawLine(haveWood.transform.position, getWoodRight.transform.position, Color.green, 100f);
        Debug.DrawLine(haveWood.transform.position, buildHouse.transform.position, Color.green, 100f);
    }

}
