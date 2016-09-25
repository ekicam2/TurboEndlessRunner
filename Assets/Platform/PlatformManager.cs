using UnityEngine;
using System.Collections.Generic;

public class PlatformManager : MonoBehaviour
{
    public Material[] materials;
    public PhysicMaterial[] physicsMaterials;
    public Transform prefab;
    public int numberOfObjects;
    public float recycleOffset;
    public Vector3 startPosition;
    public Vector3 minSize, maxSize, minGap, maxGap;
    public float minY, maxY;

    private Vector3 nextPosition;
    private Queue<Transform> objectQueue;

    void Start()
    {
        GameEventManager.GameStart += GameStart;

        objectQueue = new Queue<Transform>(numberOfObjects);

        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; ++i)
        {
            objectQueue.Enqueue(Instantiate<Transform>(prefab));
        }

        for (int i = 0; i < numberOfObjects; ++i)
        {
            Recycle();
        }
    }

    void Update()
    {
        if (objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled)
        {
            Recycle();
        }
    }

    void Recycle()
    {
        Vector3 scale = new Vector3(
                Random.Range(minSize.x, maxSize.x),
                Random.Range(minSize.y, maxSize.y),
                Random.Range(minSize.z, maxSize.z));

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        Transform obj = objectQueue.Dequeue();
        obj.localScale = scale;
        obj.localPosition = position;
        int matIndex = Random.Range(0, materials.Length);
        obj.GetComponent<Renderer>().material = materials[matIndex];
        obj.GetComponent<Collider>().material = physicsMaterials[matIndex];
        objectQueue.Enqueue(obj);

        nextPosition += new Vector3(
            Random.Range(minGap.x, maxGap.x) + scale.x,
            Random.Range(minGap.y, maxGap.y),
            Random.Range(minGap.z, maxGap.z));

        if(nextPosition.y < minY) {
            nextPosition.y = minY + maxGap.y;
        }
        else if(nextPosition.y > maxY) {
            nextPosition.y = maxY + maxGap.y;
        }
    }

    void GameStart()
    {
        nextPosition = startPosition;
        for(int i = 0; i < numberOfObjects; ++i)
        {
            Recycle();
        }
    }
}
