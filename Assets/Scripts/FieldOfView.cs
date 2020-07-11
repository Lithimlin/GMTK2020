using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public int viewLength;

    void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        vertices[0] = Vector3.zero;
        vertices[1] = new Vector3(viewLength, 0);
        vertices[2] = new Vector3(0 ,-viewLength);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        uv[0] = vertices[0];
        uv[1] = vertices[1];
        uv[2] = vertices[1];

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GameObject view = new GameObject("View",typeof(MeshFilter), typeof(MeshRenderer));
        view.GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        
    }
}
