using System;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Resources.Scripts.World
{
    public class WorldGenerator : MonoBehaviour
    {
        [Header("Road")] 
        [SerializeField] private MeshRenderer road1Mesh;

        [Header("Barrier")] 
        [SerializeField] private GameObject barrierPrefab;
        [SerializeField] private GameObject barrierStartPrefab;
        [SerializeField] private GameObject barrierEndPrefab;
        [SerializeField] private Transform barrierInstantiationStartPoint;
        [SerializeField] private int numberOfBarriers;
        [SerializeField] private float barrierMinSize;
        [SerializeField] private float barrierMaxSize;
        [SerializeField] private float gapMinSize;
        [SerializeField] private float gapMaxSize;
        [SerializeField] private int currentBarrierIndex;
        [SerializeField] private int previousBarrierIndex;
        [SerializeField] private float lastBarrierEndPositionZ;
        [SerializeField] private GameObject[] barrierPool;
        [SerializeField] private GameObject[] barrierStartPool;
        [SerializeField] private GameObject[] barrierEndPool;
        
        [Header("Decorations")]
        [SerializeField] private GameObject[] decorationPool;
        [SerializeField] private GameObject inaccessibleDecoration;
        private int arrayLength;

        private void Start()
        {
            arrayLength = decorationPool.Length;
            PrepareBarriers();
            EstablishBarrier(85f);
        }

        public void Generate(Transform startPoint, float endPoint)
        {
            var tempDecoration = inaccessibleDecoration;
            int i = Random.Range(0, arrayLength);
            decorationPool[i].transform.position = startPoint.position;
            inaccessibleDecoration = decorationPool[i];
            decorationPool[i] = tempDecoration;
            EstablishBarrier(endPoint);
        }

        private void PrepareBarriers()
        {
            var roadLength = road1Mesh.bounds.size.z;
            barrierMaxSize = roadLength / 3;
            gapMaxSize = roadLength / 3;
            numberOfBarriers = (int)((roadLength * 2) / (barrierMinSize + gapMinSize));

            barrierPool = new GameObject[numberOfBarriers];
            barrierStartPool = new GameObject[numberOfBarriers];
            barrierEndPool = new GameObject[numberOfBarriers];
            
            for (int i = 0; i < numberOfBarriers; i++)
            {
                var startPosition = barrierInstantiationStartPoint.position;
                var startRotation = barrierInstantiationStartPoint.rotation;
                barrierPool[i] = Instantiate(barrierPrefab, startPosition, startRotation);
                barrierStartPool[i] = Instantiate(barrierStartPrefab, startPosition, startRotation);
                barrierEndPool[i] = Instantiate(barrierEndPrefab, startPosition, startRotation);
            }
        }

        private void GenerateBarriers()
        {
            
        }

        private void EstablishBarrier(float endOfRoad)
        {
            var gap = Random.Range(gapMinSize, gapMaxSize);
            var length = Random.Range(barrierMinSize, barrierMaxSize);
            var currentBarrierPosition = new Vector3(0, 0.34f, lastBarrierEndPositionZ + gap + (length / 2));
            var currentBarrierStartPosition = new Vector3(0, 0.34f, lastBarrierEndPositionZ + gap);
            var currentBarrierEndPosition = new Vector3(0, 0.34f, lastBarrierEndPositionZ + gap + length);
            
            if (currentBarrierEndPosition.z > endOfRoad)
            {
                return;
            }

            barrierPool[currentBarrierIndex].transform.position = currentBarrierPosition;
            barrierPool[currentBarrierIndex].transform.localScale = new Vector3(0.15f, 0.14f, length);
            barrierStartPool[currentBarrierIndex].transform.position = currentBarrierStartPosition;
            barrierEndPool[currentBarrierIndex].transform.position = currentBarrierEndPosition;
            lastBarrierEndPositionZ = currentBarrierEndPosition.z;
            previousBarrierIndex = currentBarrierIndex;
            currentBarrierIndex++;
            if (currentBarrierIndex == numberOfBarriers)
            {
                currentBarrierIndex = 0;
            }
            EstablishBarrier(endOfRoad);
        }
    }
}
