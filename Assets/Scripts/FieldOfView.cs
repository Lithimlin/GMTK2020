using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public int viewLength;

    void Start()
    {
        Mesh mesh = new Mesh();

        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3 (0, viewLength);
        vertices[1] = new Vector3(viewLength, 0);
        vertices[2] = new Vector3(viewLength, viewLength);
        vertices[3] = new Vector3(0 , viewLength);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        uv[0] = vertices[0];
        uv[1] = vertices[1];
        uv[2] = vertices[2];
        uv[3] = vertices[2];

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
