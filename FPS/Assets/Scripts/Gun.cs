using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string name;
    public float fireRate;
    public int damage;
    public float spread;
    public float recoil;
    public float kickBack;
    public float aimSpeed;
    public GameObject prefab;
}
