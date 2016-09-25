using UnityEngine;
using System.Collections.Generic;

public class SkylineManager : MonoBehaviour {

    public Transform prefab;
    public int numberOfObjects;
    public float recycleOffset;
    public Vector3 startPosition;
    public Vector3 minScale, maxScale;

    private Vector3 nextPosition;
    private Queue<Transform> objectQueue;

	void Start () {
        GameEventManager.GameStart += GameStart;

        objectQueue = new Queue<Transform>(numberOfObjects);
        nextPosition = startPosition;
        for(int i = 0; i < numberOfObjects; ++i)
        {
            objectQueue.Enqueue(Instantiate<Transform>(prefab));
        }	

        for(int i = 0; i < numberOfObjects; ++i)
        {
            Recycle();
        }
	}
	
	void Update () {
	    if(objectQueue.Peek().localPosition.x + recycleOffset < Runner.distanceTraveled) {
            Recycle();
        }
	}

    void Recycle()
    {
        Vector3 scale = new Vector3(
                Random.Range(minScale.x, maxScale.x),
                Random.Range(minScale.y, maxScale.y),
                Random.Range(minScale.z, maxScale.z));

        Vector3 position = nextPosition;
        position.x += scale.x * 0.5f;
        position.y += scale.y * 0.5f;

        Transform obj = objectQueue.Dequeue();
        obj.localScale = scale;
        obj.localPosition = position;
        nextPosition.x += scale.x;
        objectQueue.Enqueue(obj);
    }

    void GameStart()
    {
        nextPosition = startPosition;
        for (int i = 0; i < numberOfObjects; ++i)
        {
            Recycle();
        }
    }
}
