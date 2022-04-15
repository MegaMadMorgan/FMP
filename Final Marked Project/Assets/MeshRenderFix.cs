using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRenderFix : MonoBehaviour
{
    MeshFilter yourMesh;
    public Mesh replacementmesh;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        yourMesh = this.GetComponent<MeshFilter>();
        if (yourMesh.sharedMesh != replacementmesh)
        {
            yourMesh.sharedMesh = replacementmesh;
        }
    }
}
