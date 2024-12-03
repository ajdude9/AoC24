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
        readLists();//Read in all the numbers

        int finalInt = 0;//The final number of 'safe' entries
        bool safe = true;//Whether or not an entry is 'safe'
        for(int i = 0; i < hashList.Count; i++)//Go through each entry in the list, which is a list in itself
        {
            safe = true;//Assume that the value is initially safe
            for(int j = 0; j < hashList[i].Count - 1; j++)//Go through each entry in the nested list (except the last) which contains the numbers
            {
                if(hashList[i][j] > hashList[i][j+1])//If the next number is less than the current number
                {
                    if(hashList[i][j] - hashList[i][j+1] > 3 || hashList[i][j] - hashList[i][j+1] < -3 || hashList[i][j] - hashList[i][j+1] == 0)//If the difference is greater than 3 OR zero
                    {
                        safe = false;//It's unsafe
                    }
                }
                else//If the next number is greater than the current number
                {
                    if(hashList[i][j+1] - hashList[i][j] > 3 || hashList[i][j+1] - hashList[i][j] < -3 || hashList[i][j+1] - hashList[i][j] == 0)//If the difference is greater than 3 OR zero
                    {
                        safe = false;//It's unsafe
                    }
                }
            }
            safe = checkDirection(hashList[i], hashList[i][0], hashList[i][1]);//Ensure all numbers are either ascending or descending          
            safetyList.Add(safe);//Add whether or not this list is safe
        }
        finalInt = checkSafety(safetyList);//Find all safe lists
        if(finalInt >= 475)//Known incorrect 'too high' value
        {
            Debug.Log("ERROR: Calculated value too high; value: " + finalInt);
            if(finalInt == 628 || finalInt == 475)
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

    void readLists()//Read in the lists
    {
        string line;//The text contained on a particular line
        int count = 0;//How many lines have been read
        while ((line = reader.ReadLine()) != null)//While there are still lines to be read
        {
            string[] words = line.Split(char.Parse(" "));//Split entries based on spaces
            hashList.Add(new List<int>());//Create a new nested list
            for(int i = 0; i < words.Length; i++)//For each word in the list
            {
                hashList[count].Add(int.Parse(words[i]));//Extract the number and add it to the nested list
            }
            count = count + 1;//A line has been parsed; create a new nested list entry to store the next line
        }
    }       
    /**
    * Check the direction numbers in a list are moving
    * @readableList - The list of numbers to check
    * @first - The first item in the list; either the lower or higher number
    * @second - The second item in the list; depending on if it's higher or lower than the first, decides direction
    */
    bool checkDirection (List<int> readableList, int first, int second)
    {
        bool increasing;//If the list is increasing
        bool safety = true;//If the list only moves in one direction
        if(first < second)//If the first number is less than the second, meaning that the values are increasing
        {
            increasing = true;//The values are increasing
        }
        else//Otherwise the values are decreasing
        {
            increasing = false;//The values are not increasing
        }

        for(int i = 0; i < readableList.Count - 1; i++)//Read each value in the list, except the last
        {
            if(increasing)//If the values are increasing
            {
                if(readableList[i] >= readableList[i+1])//If the next value isn't bigger than the last, or is equal to it
                {
                    safety = false;
                }
            }
            else//If the values are decreasing
            {
                if(readableList[i] <= readableList[i+1])//If the next value isn't smaller than the last, or is equal to it
                {
                    safety = false;
                }
            }
        }
        return safety;//Return whether or not the provided list is 'Safe'
    }

    int checkSafety(List<bool> safetyList)//Read the number of safe values from a list
    {
        int count = 0;//Set the total number of safe values to 0 initially
        for(int i = 0; i < safetyList.Count; i++)//For each entry in the list
        {
            if(safetyList[i])//If the entry reads 'true'
            {
                count = count + 1;//Increase the number of known safe entries by one
            }
        }
        return count;//Return how many entries are safe
    }
}
