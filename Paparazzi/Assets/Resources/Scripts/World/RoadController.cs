using System;
using UnityEngine;

namespace Resources.Scripts.World
{
    public class RoadController : MonoBehaviour
    {
        [SerializeField] private Transform otherRoadEndPoint;
        [SerializeField] private Transform thisEndPoint;
        [SerializeField] private Transform mainTransform;
        [SerializeField] private string layerToTrigger;
        [SerializeField] private MeshRenderer meshRenderer;
        [SerializeField] private WorldGenerator worldGenerator;
        [SerializeField] private GameObject zabor;

        private void Start()
        {
            var mainTransformPosition = mainTransform.position;
            transform.position = new Vector3(mainTransformPosition.x, mainTransformPosition.y, 
                mainTransformPosition.z + meshRenderer.bounds.size.z / 2);
        }


        // private void Update()
        // {
        //     if (Input.GetKeyDown(KeyCode.H))
        //     {
        //         var hui = (zabor.transform.position.z + (_zaborRenderer.bounds.size.z / 2)) -
        //                   (zabor.transform.position.z - (_zaborRenderer.bounds.size.z / 2));
        //         Debug.Log(zabor.transform.localScale.z + "   " + hui);
        //     }
        // }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer(layerToTrigger))
            {
                return;
            }
            mainTransform.position = otherRoadEndPoint.position;
            worldGenerator.Generate(mainTransform, thisEndPoint.position.z);
        }
    }
}
