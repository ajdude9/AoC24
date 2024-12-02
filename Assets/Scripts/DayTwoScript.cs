using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DayTwoScript : MonoBehaviour
{
    StreamReader reader = new StreamReader("Inputs/input2.txt");
    List<List<int>> hashList = new List<List<int>>();
    public void run1()
    {
        readLists();
    }

    public void run2()
    {
        
    }

    void readLists()
    {
        string line;
        int count = 0;
        while ((line = reader.ReadLine()) != null)
        {
            string[] words = line.Split(char.Parse(" "));
            hashList.Add(new List<int>());
            for(int i = 0; i < words.Length; i++)
            {
                hashList[count].Add(int.Parse(words[i]));
            }
            count = count + 1;            
        }
    }       

}
