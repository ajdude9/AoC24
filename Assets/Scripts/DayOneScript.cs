using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class DayOneScript : MonoBehaviour
{
    List<int> leftList = new List<int>();
    List<int> rightList = new List<int>();
    List<int> differenceList = new List<int>();
    List<int> distinctList = new List<int>();
    List<int> repeatedList = new List<int>();
    int total = 0;
    StreamReader reader = new StreamReader("Inputs/input1.txt");

    public void run1()
    {
        readLists();
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

    public void run2()
    {
        readLists();//Read in both lists from the input file
        leftList = sortLists(leftList);//Sort the left list to be least to greatest
        rightList = sortLists(rightList);//Sort the right list to be least to greatest
        distinctList = findDistinct(leftList);//Find every distinct number in the list, and include one of each
        /**
        for(int i = 0; i < distinctList.Count; i++)//Iterate through all distinct values
        {
            
            int count = 0;
            
            for(int j = 0; j < rightList.Count; j++)//Iterate through everything in the right list
            {
                //Debug.Log("Comparing " + distinctList[i] + " with " + rightList[j]);
                if(distinctList[i] == rightList[j])//If both values are the same
                {
                    count = count + 1;
                    Debug.Log(distinctList[i] + " and " + rightList[j] + " are the same");
                }
                else//Both values do not subtract to zero
                {
                    if(distinctList[i] - rightList[j] < 0)//If the right list value is smaller, and thus has been passed
                    {
                        //break;//We no longer need to keep iterating through the list as the matched values have been found
                        
                    }
                }
            }
            repeatedList.Add(count);
            Debug.Log("Found " + count + " repeats.");
        }
        */
        int tally = 0;//Set the total tally to 0
        foreach(int number in distinctList)//For each distinct number
        {
            foreach(int compared in rightList)//For each number in the right list
            {
                if(number == compared)//If the distinct number from the left list is the same as the one in the right
                {
                    tally = tally + 1;//Increase the tally by one
                }                
            }
            repeatedList.Add(tally);//Add how many instances there are of the current number in the right list
            tally = 0;//Reset the tally
        }
        for(int i = 0; i < distinctList.Count; i++)//Iterate through the list
        {
            total = total + (distinctList[i] * repeatedList[i]);//Sum the total as the number in the distinct list multiplied by the number of times it appears
        }
        if(total >= 24070620)//Known too-high value
        {
            Debug.Log("ERROR: Total Value is too high. Incorrect Value: " + total);
            if(total == 24070620)
            {
                Debug.Log("ERROR: Repeated Known Incorrect Value.");
            }
        }
        else
        {
            Debug.Log(total);
        }
        reader.Close();
    }

    void readLists()
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

    List<int> findDistinct(List<int> list)
    {
        HashSet<int> distinctNumbers = new();
        HashSet<int> duplicateNumbers = new();
      
        foreach (int number in list)
        {
            if (!distinctNumbers.Add(number))
                duplicateNumbers.Add(number);

        }

        return distinctNumbers.ToList();;
    }

}
