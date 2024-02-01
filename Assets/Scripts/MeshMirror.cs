using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshMirror : MonoBehaviour
{
    public Transform _objectToMirror;

    public bool hasHover;
    public Material hoverMaterial;
    public MeshRenderer[] hoverMeshes;

    protected Material[] originalMaterials;

    public void Awake()
    {
        int meshCount = hoverMeshes.Length;
        originalMaterials = new Material[meshCount];

        if (_objectToMirror == null)
        {
            _objectToMirror = transform;
        }
    }

    public virtual void MirrorPosition(Vector3 positionToMirror)
    {
        _objectToMirror.localPosition = positionToMirror;
    }

    public virtual void MirrorRotation(Quaternion rotationToMirror)
    {
        _objectToMirror.localRotation = rotationToMirror;
    }

    public virtual void ActivateHover()
    {
        for(int i = 0;i < hoverMeshes.Length; i++)
        {
            originalMaterials[i] = hoverMeshes[i].material;
            hoverMeshes[i].material = hoverMaterial;
        }
    }

    public virtual void DeactivateHover()
    {
        for (int i = 0; i < hoverMeshes.Length; i++)
        {
            hoverMeshes[i].material = originalMaterials[i];
        }
    }
}
