using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class EnemyLockOn : MonoBehaviour
{
    public float range;
    public Transform emptyTarget;

    public CinemachineTargetGroup group;
    public int enemyCount;
    public List<Transform> enemiesToLock;
    public Transform closestEnemy;
    public Transform selectedEnemy;
    public Transform priorityEnemy;
    public bool foundPriorityEnemy;

    public float xScaleIncrement;
    public float yScaleIncrement;
    public float zScaleIncrement;
    public float xScaleMax;
    public float zScaleMax;
    public GameObject targetingCone;
    public GameObject targetingConePivot;
    public Transform coneHolder;
    private Vector3 selectorDirection;
    private bool parentChangeInitialisationPerformed;

    public bool LockOn;
    public bool temp;

    public TargetLockCamera cam;
    private PlayerMovement characterMovement;
    private TargetingConeTrigger trigger;

    private void Awake()
    {
        cam = GetComponent<TargetLockCamera>();
        characterMovement = GetComponent<PlayerMovement>();
        trigger = targetingCone.GetComponent<TargetingConeTrigger>();
    }

    private void Update()
    {
        if (BarelyInputManager.CameraButton()) temp = !temp;
        if (temp)
        {
            RunEnemySearchSphereCollider();
        }
        else
        {
            enemiesToLock.Clear();
            cam.targetLockCam = false;
        }

        enemyCount = enemiesToLock.Count;

        if (enemyCount == 0 || priorityEnemy == null)
        {
            InitialiseTargetGroup();
            cam.targetLockCam = false;
            foundPriorityEnemy = false;
            closestEnemy = null;
            selectedEnemy = null;
            priorityEnemy = null;
            InitializeConeParent();
            ResetTargetingCone();
            LockOn = false;
        }

        //Main Chunk
        if (enemyCount != 0)
        {
            cam.targetLockCam = true;
            findClosestEnemy();
            if (closestEnemy != null && foundPriorityEnemy == false) SetPriorityEnemy(closestEnemy);
            SwitchTarget();
            if (selectedEnemy != null) SetPriorityEnemy(selectedEnemy);
            if (priorityEnemy != null) BuildTargetGroup();
            LockOn = true;
        }
    }

    //change sphere to capsule
    public void RunEnemySearchSphereCollider()
    {
        Collider[] enemyDetect = Physics.OverlapCapsule(transform.position, transform.position, range);
        enemiesToLock = new List<Transform>();
        foreach (Collider col in enemyDetect)
        {
            Targetable enemy = col.GetComponent<Targetable>();
            if (enemy != null) enemiesToLock.Add(col.transform);
        }
    }

    private void findClosestEnemy()
    {
        float closest = range;
        closestEnemy = null;
        for (int i = 0; i < enemyCount; i++)
        {
            float distanceToPlayer = Vector3.Distance(enemiesToLock[i].position, transform.position);
            if (distanceToPlayer < closest)
            {
                closest = distanceToPlayer;
                closestEnemy = enemiesToLock[i];
            }
        }
    }

    private void SetPriorityEnemy(Transform pEnemy)
    {
        priorityEnemy = pEnemy;
        foundPriorityEnemy = true;
        SetConeParentToTarget();
    }

    private void BuildTargetGroup()
    {
        CinemachineTargetGroup.Target enemy;
        enemy.target = priorityEnemy;
        enemy.weight = priorityEnemy.GetComponent<Targetable>().camWeight;
        enemy.radius = priorityEnemy.GetComponent<Targetable>().camRadius;

        group.m_Targets[1].target = enemy.target;
        group.m_Targets[1].weight = enemy.weight;
        group.m_Targets[1].radius = enemy.radius;
    }

    public void InitialiseTargetGroup()
    {
        CinemachineTargetGroup.Target defaultTarget;
        defaultTarget.target = emptyTarget;
        defaultTarget.weight = emptyTarget.GetComponent<EmptyTargetData>().camWeight;
        defaultTarget.radius = emptyTarget.GetComponent<EmptyTargetData>().camRadius;

        group.m_Targets[1].target = defaultTarget.target;
        group.m_Targets[1].weight = defaultTarget.weight;
        group.m_Targets[1].radius = defaultTarget.radius;
    }

    private void SwitchTarget()
    {
        if (BarelyInputManager.SubVertical() == 0 && BarelyInputManager.SubHorizontal() == 0) ResetTargetingCone();
        else
        {
            selectorDirection = ((characterMovement.camForward * -BarelyInputManager.SubVertical()) + (characterMovement.camRight * BarelyInputManager.SubHorizontal())).normalized;
            targetingConePivot.transform.rotation = Quaternion.LookRotation(selectorDirection);
            targetingCone.SetActive(true);

            if (trigger.selectedEnemy != null && trigger.selectedEnemy != priorityEnemy)
            {
                parentChangeInitialisationPerformed = false;
                selectedEnemy = trigger.selectedEnemy.transform;
            }
            else
            {
                if (targetingCone.transform.localScale.y <= range) targetingCone.transform.localScale += new Vector3(0, yScaleIncrement, 0);
                if (targetingCone.transform.localScale.x <= xScaleMax) targetingCone.transform.localScale += new Vector3(xScaleIncrement, 0, 0);
                if (targetingCone.transform.localScale.z <= zScaleMax) targetingCone.transform.localScale += new Vector3(0, 0, zScaleIncrement);

            }
        }
    }

    private void SetConeParentToTarget()
    {
        targetingConePivot.transform.SetParent(priorityEnemy);
        if (!parentChangeInitialisationPerformed)
        {
            targetingConePivot.transform.localPosition = Vector3.zero;
            targetingCone.transform.localScale = new Vector3(trigger.coneScaleX, trigger.coneScaleY, trigger.coneScaleZ);
            parentChangeInitialisationPerformed = true;
        }
    }

    public void InitializeConeParent()
    {
        targetingConePivot.transform.SetParent(coneHolder);
        targetingConePivot.transform.localPosition = Vector3.zero;
        targetingConePivot.transform.localRotation = Quaternion.identity;
        parentChangeInitialisationPerformed = false;
    }

    public void ResetTargetingCone()
    {
        trigger.selectedEnemy = null;
        targetingCone.SetActive(false);
        targetingConePivot.transform.rotation = transform.rotation;
        targetingCone.transform.localScale = new Vector3(trigger.coneScaleX, trigger.coneScaleY, trigger.coneScaleZ);
    }

}
