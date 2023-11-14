using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Avatar = Alteruna.Avatar;
using TrackedPoseDriver = UnityEngine.SpatialTracking.TrackedPoseDriver;

public class PlayerVRController : MonoBehaviour
{
    private Avatar _avatar;
    [SerializeField] private Transform head;
    [SerializeField] private Camera camera;
    [SerializeField] private int playerSelfLayer;

    // [SerializeField] private TrackedPoseDriver rightHandDriver, leftHandDriver;
    [SerializeField] private GameObject leftController, rightController;

    private void Awake()
    {
        _avatar = GetComponent<Avatar>();
    }

    private void Start()
    {
        if (_avatar.IsMe)
        {
            head.gameObject.layer = playerSelfLayer;
            foreach (Transform child in head)
                child.gameObject.layer = playerSelfLayer;
        }
        else
        {
            // leftController.enabled = false;
            // rightController.enabled = false;
            camera.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!_avatar.IsMe)
            return;

        head.localPosition = camera.transform.localPosition;
        head.rotation = camera.transform.rotation;
    }
}