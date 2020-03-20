using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] Timer spawnTimer;
    [SerializeField] Bonus[] bonusObjects;
    [SerializeField] Transform[] bonusPositions;
    private List<int> randomList = new List<int>();

    private int zero = 0;
    private int un = 1;
    private int deux = 2;
    private int trois = 3;
    private int quatre = 4;

    // Start is called before the first frame update
    void Start()
    {
        randomList.Add(zero);
        randomList.Add(un);
        randomList.Add(deux);
        randomList.Add(trois);
        randomList.Add(quatre);

        spawnTimer.SetDuration(Random.Range(2f, 5f), 1);

    }

    // Update is called once per frame
    void Update()
    {
        if (spawnTimer.Update())
        {
            int randomObject = Random.Range(0, bonusObjects.Length);
            int randomPosition = randomList[Random.Range(0, randomList.Count)];

            Bonus bonus = Instantiate(bonusObjects[randomObject], bonusPositions[randomPosition].position, Quaternion.identity);
            bonus.index = randomPosition;
            RemoveListNumber(randomPosition);
        }
    }

    private void RemoveListNumber(int index)
    {
        randomList.Remove(index);
    }

    public void AddNumberList(int index)
    {
        switch (index)
        {
            case 0:
                randomList.Insert(0, zero);
                break;

            case 1:
                randomList.Insert(1, un);
                break;

            case 2:
                randomList.Insert(2, deux);
                break;

            case 3:
                randomList.Insert(3, trois);
                break;

            case 4:
                randomList.Insert(4, quatre);
                break;
        }
        spawnTimer.SetDuration(Random.Range(2f, 5f), 1);
    }
}
