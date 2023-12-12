using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
public class StageGene : MonoBehaviour
{
   [SerializeField] GameObject[] geneObjects;
   [SerializeField] GameObject obstacleObject;
    // Start is called before the first frame update
    async void Start()
    {
        await Gene1();
        await Gene2();
        await Gene3();
    }

    async  UniTask Gene1()
    { 
        for(int i = 0; i < 2000; i++)
        {
            int n = Random.Range(0, 4);
            Instantiate(geneObjects[n],new Vector3(SetCoordinate(-100, 100),SetCoordinate(50, 100),0), Quaternion.Euler(0,0, SetRotation()));
        }

        for (int i = 0; i < 1000; i++)
        {
            Instantiate(obstacleObject, new Vector3(SetCoordinate(-100, 100), SetCoordinate(50, 100), 0), Quaternion.Euler(0, 0, SetRotation()));
        }

    }
    async UniTask Gene2()
    {
        for (int i = 0; i < 2000; i++)
        {
            int n = Random.Range(0, 4);
            Instantiate(geneObjects[n], new Vector3(SetCoordinate(-100, 100), SetCoordinate(-50, 50), 0), Quaternion.Euler(0, 0, SetRotation()));
        }

        for (int i = 0; i < 1000; i++)
        {
            float  CoordinateX = SetCoordinate(-100, 100);
            float CoordinateY = SetCoordinate(-50, 50);
            if (System.Math.Abs(CoordinateX) > 5 && System.Math.Abs(CoordinateY) > 5)
            {
                Instantiate(obstacleObject, new Vector3(CoordinateX, CoordinateY,0), Quaternion.Euler(0, 0, SetRotation()));
            }
        }

    }

    async UniTask Gene3()
    {
        for (int i = 0; i < 2000; i++)
        {
            int n = Random.Range(0, 4);
            Instantiate(geneObjects[n], new Vector3(SetCoordinate(-100, 100), SetCoordinate(-100, -50), 0), Quaternion.Euler(0, 0, SetRotation()));
        }

        for (int i = 0; i < 1000; i++)
        {
            Instantiate(obstacleObject, new Vector3(SetCoordinate(-100, 100), SetCoordinate(-100, -50), 0), Quaternion.Euler(0, 0, SetRotation()));
        }

    }
    float SetCoordinate(float min, float max)
    {
        float geneVector = Random.Range(min, max);
        return geneVector;
    }

    float SetRotation()
    {
        float geneVector = Random.Range(0, 360);
        return geneVector;
    }
}
