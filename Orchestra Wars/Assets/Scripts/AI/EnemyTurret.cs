﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : MonoBehaviour
{
    public int damagePerShot = 1;
    public float timeBetweenShotsInSecs = 2f;
    public float aggroRadius = 20f;
    public GameObject projectile;
    private float attackCounter;
    public GameObject currentTarget;
    public AudioSource attackSound;

    private GameObject FindTarget() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Ally");
        GameObject closestTarget = null;
        float closestTargetDistance = aggroRadius;
        foreach (GameObject target in targets){
            if (Vector3.Distance(transform.position, target.transform.position) <= closestTargetDistance)
            closestTarget = target;
            closestTargetDistance = Vector3.Distance(transform.position, target.transform.position);
        }
        return closestTarget;
    }
    public void AttackTarget(GameObject Target) {
        GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity);
        bullet.GetComponent<Projectile>().target = Target;
        attackSound.Play();
    }
    public virtual void Update() {
        if (GameManager.instance.isPaused != true){
        attackCounter -= Time.deltaTime;
        GameObject nearestTarget = FindTarget();
        if (currentTarget != null){
            transform.LookAt(currentTarget.transform.position);
        }

        if (currentTarget == null){
            
            if (nearestTarget != null && Vector3.Distance(transform.position, nearestTarget.transform.position) <= aggroRadius) {
                currentTarget = nearestTarget;
            }
        } else {
            if (attackCounter <= 0f){
                AttackTarget(currentTarget);
                attackCounter = timeBetweenShotsInSecs;
            }

            if (Vector3.Distance(transform.position, currentTarget.transform.position) > aggroRadius)

            currentTarget = null;
        }
        }
    }
}
