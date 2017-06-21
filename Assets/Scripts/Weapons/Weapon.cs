using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private float fireRate = .2f;

    private float lastTimeFired = 0;

    public void FireProjectile() {
        if(Time.time >= lastTimeFired + fireRate) {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
            lastTimeFired = Time.time;
        } 
    }
}
