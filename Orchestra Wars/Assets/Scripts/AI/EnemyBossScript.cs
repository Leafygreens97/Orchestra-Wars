﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBossScript : MonoBehaviour
{
    public int pathIndex = 0;
    private int wayPointIndex = 0;
    public int speed = 1;
    public int health = 40;

    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if (wayPointIndex < WayPointManager.Instance.MinionPaths[pathIndex].WayPoints.Count){
            UpdateMovement();
        }else {
            OnGotToLastWayPoint();
        }
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Projectile")){
            if (health > 1) {
                health -= 1;
            } else{
                Destroy(gameObject);
            }
        }
    }
    public void UpdateMovement(){
        Vector3 targetPosition = WayPointManager.Instance.MinionPaths[pathIndex].WayPoints[wayPointIndex].position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        transform.LookAt(targetPosition);
        if (Vector3.Distance(transform.position, targetPosition) < .1f) {
            wayPointIndex++;
        }
    }
    private void OnGotToLastWayPoint(){
        
    }
}