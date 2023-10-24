using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EzySlice;
using UnityEngine.InputSystem;

public class sliceObject : MonoBehaviour
{

    public Transform startSlicePoint;
    public Transform endSlicePoint;
    public LayerMask sliceableLayer;
    public VelocityEstimator velocityEstimator;
    public Material targetObjectMaterial;
    public float cutForce = 2000;
    [SerializeField] private string layerName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if(hasHit)
        {
            GameObject target = hit.transform.gameObject;
            cutObject(target);
        }
    }
    

    public void cutObject(GameObject target)
    {
        Vector3 velocity = velocityEstimator.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity);
        planeNormal.Normalize();
        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);
        if(hull != null)
        {
            int newLayer = LayerMask.NameToLayer(layerName);
            targetObjectMaterial = target.GetComponent<Renderer>().material;
            GameObject upperHull = hull.CreateUpperHull(target,targetObjectMaterial);
            
            setBrokenObjects(upperHull);
            
            GameObject lowerHull = hull.CreateLowerHull(target,targetObjectMaterial);
            //if (newLayer != -1)
            //{
            //    upperHull.layer = newLayer;
            //}
            setBrokenObjects(lowerHull);
            
            Destroy(target);
        }
    }
    public void setBrokenObjects(GameObject brokenObject)
    {
        Rigidbody rb = brokenObject.AddComponent<Rigidbody>();
        MeshCollider mc = brokenObject.AddComponent<MeshCollider>();
        mc.convex = true;
        
        rb.AddExplosionForce(cutForce, brokenObject.transform.position, 1);
    }
}
