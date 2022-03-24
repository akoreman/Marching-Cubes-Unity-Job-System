using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script controls the creation of fluid balls to get the fluid stream effect.

public class FluidHandler : MonoBehaviour
{
    public GameObject fluidBall;
    public Vector3 fluidBallLaunchVelocity;
    public Vector3 fluidBallLaunchPosition;

    GameObject marchingCubes;

    void Awake()
    {
        marchingCubes = GameObject.Find("Marching Cubes");
    }

    void Start()
    {
        InvokeRepeating("LaunchFluidBall", 1.0f, .3f);
    }

    void LaunchFluidBall()
    {
        GameObject fluidBallInstance = Instantiate(fluidBall);

        Vector3 launchPosition = fluidBallLaunchPosition;

        float lowRange = -1f;
        float highRange = 1f;

        // Add some randomness to the initial position to gain a more natural effect.
        launchPosition.x += Random.Range(lowRange, highRange);
        launchPosition.y += Random.Range(lowRange, highRange);
        launchPosition.z += Random.Range(lowRange, highRange);

        fluidBallInstance.transform.position = launchPosition;
        fluidBallInstance.GetComponent<Rigidbody>().velocity = fluidBallLaunchVelocity;
    }
}
