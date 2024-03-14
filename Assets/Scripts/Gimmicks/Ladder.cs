using System;
using UnityEngine;

public class Ladder : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<FpsController>(out var controller))
        {
            controller.OnLadder = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<FpsController>(out var controller))
        {
            controller.OnLadder = false;
        }
    }
}
