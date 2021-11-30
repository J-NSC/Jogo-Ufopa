using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Triangulo_cod : MonoBehaviour
{


    public MeshFilter meshF;
    public Mesh mesh;
    public Vector3[] vertices;
    public int[] triangulos;
    public Transform[] pos;



    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;

        vertices = new Vector3[]{
            pos[0].position,
            pos[1].position,
            pos[2].position,
            pos[3].position,
            pos[4].position,
            pos[5].position
        };



        triangulos = new int[(6-2) * 3];

        for (int i = 0; i < 6; i++)
        {
            if(i < 6 - 2){
                triangulos[i * 3] = 0;
                triangulos[i * 3 + 1] = i + 1;
                triangulos[i * 3 + 2] = i + 2;
            }
        }

        mesh.Clear();
        mesh.vertices= vertices;
        mesh.triangles = triangulos;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
