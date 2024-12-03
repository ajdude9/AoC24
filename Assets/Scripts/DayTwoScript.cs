using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;

public class DayTwoScript : MonoBehaviour
{
    StreamReader reader = new StreamReader("Inputs/input2.txt");
    List<List<int>> hashList = new List<List<int>>();
    List<bool> safetyList = new List<bool>();
    public void run1()
    {
        readLists();

        bool safe = true;
        for(int i = 0; i < hashList.Count; i++)//Go through each entry in the list, which is a list in itself
        {
            for(int j = 0; j < hashList[i].Count - 1; j++)//Go through each entry in the nested list except the last, which contains the numbers
            {
                if(hashList[i][j] > hashList[i][j+1])//If the next number is less than the current number
                {
                    if(hashList[i][j] - hashList[i][j+1] > 3 || hashList[i][j] - hashList[i][j+1] == 0)//If the difference is greater than 3 OR zero
                    {
                        safe = false;
                    }
                }
                else//If the next number is greater than the current number
                {
                    if(hashList[i][j+1] - hashList[i][j] > 3 || hashList[i][j+1] - hashList[i][j] == 0)//If the difference is greater than 3 OR zero
                    {
                        safe = false;
                    }
                }
            }
            safe = checkDirection(hashList[i], hashList[i][0], hashList[i][1]);
            safetyList.Add(safe);
        }
        int finalInt = checkSafety(safetyList);
        if(finalInt >= 628)
        {
            Debug.Log("ERROR: Calculated value too high; value: " + finalInt);
            if(finalInt == 628)
            {
                Debug.Log("Incorrect value is the same as known incorrect value");
            }
            
        }
        else
        {
            Debug.Log(finalInt);
        }
        //Debug.Log(hashList[0][0]);//Print out the item contained in the first list object's first position
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

    bool checkDirection (List<int> readableList, int first, int second)
    {
        bool increasing;
        bool safety = true;
        if(first < second)//If the first number is less than the second, meaning that the values are increasing
        {
            increasing = true;
        }
        else//Otherwise the values are decreasing
        {
            increasing = false;
        }

        for(int i = 0; i < readableList.Count - 1; i++)
        {
            if(increasing)//If the values are increasing
            {
                if(readableList[i] > readableList[i+1])//If the values are decreasing, when they're meant to be increasing
                {
                    safety = false;
                }
            }
            else//If the values are decreasing
            {
                if(readableList[i] < readableList[i+1])//If the values are increasing, when they're meant to be decreasing
                {
                    safety = false;
                }
            }
        }
        return safety;
    }

    int checkSafety(List<bool> safetyList)
    {
        int count = 0;
        for(int i = 0; i < safetyList.Count; i++)
        {
            if(safetyList[i])
            {
                count = count + 1;
            }
        }
        return count;
    }
}
