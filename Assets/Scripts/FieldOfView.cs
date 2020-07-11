using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        StartCoroutine("FindTargetsWithDelay", .2F);
    }

    IEnumerator FindTargetsWithDelay(float delay)
    {
        while(true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleTarget();
        }
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider[] targetInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        for(int i = 0; i < targetInViewRadius.Length; i++)
        {
            Transform target = targetInViewRadius [i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward , dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.position);
                if(!Physics.Raycast (transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add (target);
                }
            }
        }
    }


    public Vector2 DirFromAngle(float angleInDegres, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegres += transform.eulerAngles.y;
        }
        return new Vector2(Mathf.Cos(angleInDegres * Mathf.Deg2Rad), Mathf.Sin(angleInDegres * Mathf.Deg2Rad));
    } 
}
