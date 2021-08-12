using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2Script : MonoBehaviour
{
    public GameObject spawnItem;
    public Transform target;
    private int timer;
    private int tick;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("MyEvent");
    }

    private IEnumerator MyEvent()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            GameObject spawn = GameObject.Instantiate(spawnItem, transform);
            spawn.transform.localPosition = Vector3.zero;

            Vector3 theVector = (transform.position - target.position).normalized;
            spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -60, theVector.y * -60, theVector.z * -60);
        }
    }

    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Click");
            GameObject spawn = GameObject.Instantiate(spawnItem, transform);
            spawn.transform.localPosition = Vector3.zero;

            Ray screenToPointer = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(screenToPointer, out hit, Mathf.Infinity))
            {
                Vector3 theVector = (transform.position - hit.point).normalized;
                spawn.GetComponent<Rigidbody>().velocity = new Vector3(theVector.x * -60, theVector.y * -60, theVector.z * -60);
            }
        }
    }

    //first-order intercept using absolute target position
    public static Vector3 FirstOrderIntercept
    (
        Vector3 shooterPosition,
        Vector3 shooterVelocity,
        float shotSpeed,
        Vector3 targetPosition,
        Vector3 targetVelocity
    )
    {
        Vector3 targetRelativePosition = targetPosition - shooterPosition;
        Vector3 targetRelativeVelocity = targetVelocity - shooterVelocity;
        float t = FirstOrderInterceptTime
        (
            shotSpeed,
            targetRelativePosition,
            targetRelativeVelocity
        );
        return targetPosition + t * (targetRelativeVelocity);
    }
    //first-order intercept using relative target position
    public static float FirstOrderInterceptTime
    (
        float shotSpeed,
        Vector3 targetRelativePosition,
        Vector3 targetRelativeVelocity
    )
    {
        float velocitySquared = targetRelativeVelocity.sqrMagnitude;
        if (velocitySquared < 0.001f)
            return 0f;

        float a = velocitySquared - shotSpeed * shotSpeed;

        //handle similar velocities
        if (Mathf.Abs(a) < 0.001f)
        {
            float t = -targetRelativePosition.sqrMagnitude /
            (
                2f * Vector3.Dot
                (
                    targetRelativeVelocity,
                    targetRelativePosition
                )
            );
            return Mathf.Max(t, 0f); //don't shoot back in time
        }

        float b = 2f * Vector3.Dot(targetRelativeVelocity, targetRelativePosition);
        float c = targetRelativePosition.sqrMagnitude;
        float determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        { //determinant > 0; two intercept paths (most common)
            float t1 = (-b + Mathf.Sqrt(determinant)) / (2f * a),
                    t2 = (-b - Mathf.Sqrt(determinant)) / (2f * a);
            if (t1 > 0f)
            {
                if (t2 > 0f)
                    return Mathf.Min(t1, t2); //both are positive
                else
                    return t1; //only t1 is positive
            }
            else
                return Mathf.Max(t2, 0f); //don't shoot back in time
        }
        else if (determinant < 0f) //determinant < 0; no intercept path
            return 0f;
        else //determinant = 0; one intercept path, pretty much never happens
            return Mathf.Max(-b / (2f * a), 0f); //don't shoot back in time
    }

    public static bool CalculateTrajectory(float TargetDistance, float ProjectileVelocity, out float CalculatedAngle)
    {
        CalculatedAngle = 0.5f * (Mathf.Asin((-Physics.gravity.y * TargetDistance) / (ProjectileVelocity * ProjectileVelocity)) * Mathf.Rad2Deg);
        if (float.IsNaN(CalculatedAngle))
        {
            CalculatedAngle = 0;
            return false;
        }
        return true;
    }

    Vector3 Fire()
    {

        float projectileVelocity = 300;
        float distance = Vector3.Distance(transform.position, target.position);
        float trajectoryAngle;

        Vector3 TargetCenter = FirstOrderIntercept(transform.position, Vector3.zero, projectileVelocity, target.position, target.GetComponent<Rigidbody>().velocity);


        /*if (CalculateTrajectory(distance, projectileVelocity, out trajectoryAngle))
        {
            float trajectoryHeight = Mathf.Tan(trajectoryAngle * Mathf.Deg2Rad) * distance;
            TargetCenter.y += trajectoryHeight;
        }*/

        //fire at TargetCenter
        return TargetCenter;
    }
}
