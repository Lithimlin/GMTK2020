using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    [Range(0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    [Range(0,1)]
    public float meshResolution;

    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    [HideInInspector]
    public List<Transform> visibleTargets = new List<Transform>();

    private void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
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

    private void LateUpdate()
    {
        DrawFOV();
    }

    void FindVisibleTarget()
    {
        visibleTargets.Clear();
        Collider2D[] targetInViewRadius = Physics2D.OverlapCircleAll(transform.position, viewRadius, targetMask);

        foreach(Collider2D collider in targetInViewRadius)
        {
            Transform target = collider.transform;
            Vector2 dirToTarget = target.position - transform.position;
            if (Vector2.Angle(transform.right, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector2.Distance(transform.position, target.position);
                if(!Physics2D.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                }
            }
        }
    }

    void DrawFOV()
    {
        int rays = Mathf.RoundToInt(viewAngle * meshResolution);
        float rayAngle = viewAngle / rays;
        List<Vector3> viewPoints = new List<Vector3>();

        for (int i = 0; i <= rays; i++)
        {
            float angle = transform.eulerAngles.z - viewAngle / 2 + rayAngle * i;
            ViewCastInfo vc = ViewCast(angle);
            viewPoints.Add(vc.point);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount - 2) * 3];

        vertices[0] = Vector3.zero;
        for(int i = 0; i < vertexCount-1; i++)
        {
            vertices[i + 1] = transform.InverseTransformPoint(viewPoints[i]);
            if (i < vertexCount - 2)
            {
                int l = triangles.Length-1;
                triangles[l-(i * 3)] = 0;
                triangles[l-(i * 3 + 1)] = i + 1;
                triangles[l-(i * 3 + 2)] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals();

    }

    ViewCastInfo ViewCast(float globalAngle)
    {
        Vector2 dir = DirFromAngle(globalAngle, true);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, viewRadius, obstacleMask);
        if (hit.collider != null)
        {
            return new ViewCastInfo(true, hit.point, hit.distance, globalAngle);
        } else
        {
            return new ViewCastInfo(false, (Vector2) transform.position + dir * viewRadius, viewRadius, globalAngle);
        }
    }

    public struct ViewCastInfo
    {
        public bool hit;
        public Vector2 point;
        public float distance;
        public float angle;
        public ViewCastInfo(bool _hit, Vector2 _point, float _distance, float _angle)
        {
            hit = _hit;
            point = _point;
            distance = _distance;
            angle = _angle;
        }
    }


    public Vector2 DirFromAngle(float angleInDegres, bool angleIsGlobal)
    {
        if(!angleIsGlobal)
        {
            angleInDegres += transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Cos(angleInDegres * Mathf.Deg2Rad), Mathf.Sin(angleInDegres * Mathf.Deg2Rad));
    } 
}
