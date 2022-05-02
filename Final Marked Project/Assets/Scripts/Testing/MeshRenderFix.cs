using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshRenderFix : MonoBehaviour
{
    // initialise variables
    MeshFilter yourMesh;
    public Mesh replacementmesh;

    
    void FixedUpdate()
    {
        // set mesh to render
        yourMesh = this.GetComponent<MeshFilter>();
        if (yourMesh.sharedMesh != replacementmesh)
        {
            yourMesh.sharedMesh = replacementmesh;
        }
    }
}
