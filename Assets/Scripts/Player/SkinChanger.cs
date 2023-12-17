using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinChanger : MonoBehaviour
{
    [SerializeField] private string objName;
    [SerializeField] private int id;
    public void saveIndex()
    {
        PlayerPrefs.SetInt(objName, id);
    }
}
