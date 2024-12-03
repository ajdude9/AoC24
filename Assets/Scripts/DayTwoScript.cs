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

        
        for(int i = 0; i < hashList.Count; i++)//Go through each entry in the list, which is a list in itself
        {
            for(int j = 0; i < hashList[i].Count - 1; j++)//Go through each entry in the nested list except the last, which contains the numbers
            {
                if(hashList[i][j] > hashList[i][j+1])
                {

                }
            }
        }
        
        Debug.Log(hashList[0][0]);//Print out the item contained in the first list object's first position
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
