﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyScript : MonoBehaviour
{
    public int pathIndex = 0;
    private int wayPointIndex = 0;
    public int speed = 2;
    public int health = 6;
    public AudioSource hitSound;

    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isPaused != true){
        if (wayPointIndex < WayPointManager.Instance.MinionPaths[pathIndex].WayPoints.Count){
            UpdateMovement();
        }else {
            OnGotToLastWayPoint();
        }
        }
    }
    private void OnTriggerEnter(Collider other){
        if (other.CompareTag("Projectile")){
            if (health > 1) {
                health -= 1;
                hitSound.Play();
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
        GameManager.instance.allyMinionCount += 1;
        Destroy(gameObject);
    }
}

