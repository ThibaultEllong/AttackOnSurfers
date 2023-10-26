using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevel : MonoBehaviour
{
    public GameObject[] section;
    public int zPos = 50;
    public int offset = 50;
    public bool creatingSection = false;
    public int secNum;
    public int maxSections;
    private int count = 0;


    // Update is called once per frame
    void Update()
    {
        if (creatingSection == false && count <maxSections)
        {
            creatingSection = true;
            StartCoroutine(GenerateSection());
            count ++;
        }
    }

    IEnumerator GenerateSection()
    {
        secNum = Random.Range(0, 4);
        Instantiate(section[secNum], new Vector3(0,0,zPos), Quaternion.identity);
        zPos += offset;
        yield return new WaitForSeconds(2);
        creatingSection = false;

    }
}
