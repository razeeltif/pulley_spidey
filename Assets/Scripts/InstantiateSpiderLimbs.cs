using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateSpiderLimbs : MonoBehaviour
{
    public GameObject patteAraigneePrefab;
    public GameObject[] patteInstances;
    public int nombreDePattes;

    public GameObject CorpDeLaraignee;

    [Header("Configuration CableComponent")]
    // Configuration CableComponent
    public Material CableMaterial;
    public Vector3 CableOffset;
    public float CableLength;
    public int TotalSegments;
    public int SegmentsPerUnit;
    public int SolverIterations;

    [Header("Configuration Spring Join")]
    public float distance;
    public float dampingRatio;
    public float frequency;


    // Start is called before the first frame update
    void Start()
    {

        if(patteInstances.Length == 0)
        {
            instantiatePatte();
        }

        initialyzePatte();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void instantiatePatte()
    {
        GameObject instance;

        patteInstances = new GameObject[nombreDePattes];

        for (int i = 0; i < nombreDePattes; i++)
        {
            instance = GameObject.Instantiate(patteAraigneePrefab);
            CableComponent cb = instance.AddComponent<CableComponent>();
            cb.cableEnd = CorpDeLaraignee.transform;

            cb.cableMaterial = CableMaterial;
            cb.cableEndOffset = CableOffset;
            cb.cableLength = CableLength;
            cb.totalSegments = TotalSegments;
            cb.segmentsPerUnit = SegmentsPerUnit;
            cb.solverIterations = SolverIterations;

            patteInstances[i] = instance;

        }
    }

    private void initialyzePatte()
    {
        for (int i = 0; i < patteInstances.Length; i++)
        {
            for (int j = 0; j < patteInstances.Length; j++)
            {
                if (patteInstances[i] != patteInstances[j])
                {
                    SpringJoint2D sj = patteInstances[i].AddComponent<SpringJoint2D>();
                    sj.autoConfigureConnectedAnchor = false;
                    sj.autoConfigureDistance = false;
                    sj.distance = distance;
                    sj.dampingRatio = dampingRatio;
                    sj.frequency = frequency;

                    sj.connectedBody = patteInstances[j].GetComponent<Rigidbody2D>();
                }
            }
        }
    }
}
