using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GooseSpawner : MonoBehaviour
{
    public GameObject goosePrefab;
    public Camera cam;
    public Transform player;

    public float delay = 10;

    public static int geeseInStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartTimeOffset(Random.Range(0, 3)));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator StartTimeOffset(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSecondsRealtime(delay);
            GameObject newGoose = Instantiate(goosePrefab, SpawnPosition(), Quaternion.identity, transform);
            newGoose.GetComponent<GooseAI>().player = player.gameObject;
            geeseInStage++;
            if (geeseInStage % 9 == 0)
                if (delay != 1)
                {
                    delay--;
                    SoundManager.intensity++;
                }
        }
    }

    public Vector3 SpawnPosition()
    {
        Vector3 pos = Vector3.zero;

        for (int i = 0; i < 1000; i++)
        {
            pos = player.position + (Vector3)Random.insideUnitCircle * (GooseAI.acceptableDistanceToPlayer * 2);
            if (ValidPosition(pos))
                break;
        }
        return pos;
    }

    public bool ValidPosition(Vector3 pos)
    {
        GraphNode node = AstarPath.active.GetNearest(pos).node;
        if ((pos - player.position).sqrMagnitude > GooseAI.acceptableDistanceToPlayer && node.Walkable)
            return true;
        else
            return false;
    }
}
