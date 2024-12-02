using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class DayOneScript : MonoBehaviour
{
    List<int> leftList = new List<int>();
    List<int> rightList = new List<int>();
    List<int> differenceList = new List<int>();
    int total = 0;
    StreamReader reader = new StreamReader("Inputs/input1.txt");

    public void run()
    {
        string line;
        int count = 0;
        while ((line = reader.ReadLine()) != null)
        {
            string[] words = line.Split(char.Parse("-"));
            leftList.Add(int.Parse(words[0]));
            rightList.Add(int.Parse(words[1]));
            count = count + 1;
        }
        reader.Close();
        //Debug.Log(leftList[0]);
        //Debug.Log(rightList[0]);

        leftList = sortLists(leftList);
        rightList = sortLists(rightList);
        differenceList = findDifference(leftList, rightList);

        for(int i = 0; i < differenceList.Count; i++)
        {
            total = total + differenceList[i];
        }

        Debug.Log(total);
    }

    List<int> sortLists(List<int> list)
    {
        int n = list.Count;
        for (int i = 0; i < n - 1; i++)
        {
            for (int j = 0; j < n - i - 1; j++)
            {
                if (list[j] > list[j + 1])
                {
                    int temp = list[j];
                    list[j] = list[j + 1];
                    list[j + 1] = temp;
                }
            }
        }
        return list;
    }

    List<int> findDifference(List<int> left, List<int> right)
    {
        List<int> foundList = new List<int>();
        int n = left.Count;
        for(int i = 0; i < n; i++)
        {
            if(left[i] > right[i])
            {
                foundList.Add(left[i] - right[i]);
            }
            else
            {
                foundList.Add(right[i] - left[i]);
            }
        }
        return foundList;
    }
}
