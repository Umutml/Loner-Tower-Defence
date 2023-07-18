using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimulationController : MonoBehaviour
{
    private void Awake()
    {
        Physics.autoSimulation = false;
    }
}
