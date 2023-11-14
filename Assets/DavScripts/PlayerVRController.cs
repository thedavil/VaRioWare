using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Avatar = Alteruna.Avatar;

using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using UnityEngine.XR.Interaction.Toolkit.UI;
using Unity.VRTemplate;

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
            //Disable control components on the controller (other person, not self)
            DisableComponentsOnController(leftController);
            DisableComponentsOnController(rightController);
            camera.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (!_avatar.IsOwner)
            return;

        head.localPosition = camera.transform.localPosition;
        head.rotation = camera.transform.rotation;
    }





     private void DisableComponentsOnController(GameObject controller)
    {
        // Replace these types with the actual component types you want to disable
        var actionBasedControllerManager = controller.GetComponent<ActionBasedControllerManager>();
        var xrController = controller.GetComponent<XRController>(); // Assuming this is the correct type
        var xrInteractionGroup = controller.GetComponent<XRInteractionGroup>();
        var calloutGazeController = controller.GetComponent<CalloutGazeController>();

        if (actionBasedControllerManager != null)
            actionBasedControllerManager.enabled = false;

        if (xrController != null)
            xrController.enabled = false;

        if (xrInteractionGroup != null)
            xrInteractionGroup.enabled = false;

        if (calloutGazeController != null)
            calloutGazeController.enabled = false;
    }
}