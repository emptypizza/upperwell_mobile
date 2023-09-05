using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    [SerializeField] Transform followTarget = null;
    private ballrumble player_ballrumble = null;
    [SerializeField] Vector3 followVelocity;
    [SerializeField] float followSmoothMaxTime = 0.35f;
    [SerializeField] Camera cam = null;

    void Start()
    {
      //  GameManager.Instance.cam = this;
        followVelocity = new Vector3(1, 1, 1);
        if (followTarget == null)
        {
            followTarget = GameObject.FindWithTag("Player").transform;
        }

        // Automatically find the player_ballrumble component
        player_ballrumble = followTarget != null ? followTarget.GetComponent<ballrumble>() : null;
    }

    void LateUpdate()
    {
        if (followTarget != null && player_ballrumble != null)
        {
            var followTargetPosition = new Vector3(followTarget.position.x, followTarget.position.y + 3, transform.position.z);
            transform.position = Vector3.SmoothDamp(transform.position, followTargetPosition, ref followVelocity, 0.5f);
        }
    }

    public static float GetFovXDegFromFovYDeg(float fovYDeg, float aspect)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fovYDeg * Mathf.Deg2Rad / 2) * aspect) * Mathf.Rad2Deg;
    }

    public static float GetFovYDegFromFovXDeg(float fovXDeg, float aspect)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fovXDeg * Mathf.Deg2Rad / 2) / aspect) * Mathf.Rad2Deg;
    }
}

/*

public class MainCamera : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] Transform followTarget = null;
    [SerializeField] ballrumble player_ballrumble = null;
    [SerializeField] Vector3 followVelocity;
    [SerializeField] float followSmoothMaxTime = 0.35f;
    [SerializeField] Camera cam = null;


    void Start()
    {
        // Screen.SetResolution(720 , 1280, true);
        followVelocity = new Vector3(1, 1, 1);
    }


    void LateUpdate()
    {



        if (followTarget != null && player_ballrumble != null)
        {
            var followTargetPosition = new Vector3(followTarget.position.x, followTarget.position.y + 3, transform.position.z);
            // var followSmoothTime = player_ballrumble.turnSpeed > 0 ? player_ballrumble.maxSpeed * followSmoothMaxTime : 0;
            transform.position = Vector3.SmoothDamp(transform.position, followTargetPosition, ref followVelocity, 0.5f);
            //  transform.position = transform.position - player_ballrumble.transform.position ;
        }

        var fovYDeg = cam.fieldOfView;
        var fovXDeg = GetFovXDegFromFovYDeg(fovYDeg, cam.aspect);


    }

    public static float GetFovXDegFromFovYDeg(float fovYDeg, float aspect)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fovYDeg * Mathf.Deg2Rad / 2) * aspect) * Mathf.Rad2Deg;
    }

    public static float GetFovYDegFromFovXDeg(float fovXDeg, float aspect)
    {
        return 2 * Mathf.Atan(Mathf.Tan(fovXDeg * Mathf.Deg2Rad / 2) / aspect) * Mathf.Rad2Deg;
    }
}
*/