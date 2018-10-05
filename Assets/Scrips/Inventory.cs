using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject box;

    public void BoxCreate()
    {
        Instantiate(box, transform.position, Quaternion.identity);
    }
}   
    