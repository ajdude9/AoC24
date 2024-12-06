using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class DayTwoScript : MonoBehaviour
{
    StreamReader reader = new StreamReader("Inputs/input2Test.txt");
    List<List<int>> hashList = new List<List<int>>();//A list containing the input from the provided input file
    List<bool> safetyList = new List<bool>();//A list containing either 'true' or 'false' for whether an entry is safe
    List<int> unsafeList = new List<int>();//The locations of unsafe entries
    int totalViolations = 0;

    public void run1()
    {
        readLists();//Read in all the numbers

        int finalInt = 0;//The final number of 'safe' entries
        bool safe;//Whether or not an entry is 'safe'
        for (int i = 0; i < hashList.Count; i++)//Go through each entry in the list, which is a list in itself
        {
            safe = true;//Assume that the value is initially safe
            for (int j = 0; j < hashList[i].Count - 1; j++)//Go through each entry in the nested list (except the last) which contains the numbers
            {
                //Debug.Log("Checking " + hashList[i][j] + " and " + hashList[i][j+1] + " |" + j);
                if (hashList[i][j] > hashList[i][j + 1])//If the next number is less than the current number
                {
                    if (hashList[i][j] - hashList[i][j + 1] > 3 || hashList[i][j] - hashList[i][j + 1] < -3 || hashList[i][j] - hashList[i][j + 1] == 0)//If the difference is greater than 3 OR zero
                    {
                        //Debug.Log(hashList[i][j] + " and " + hashList[i][j+1] + " are unsafe");
                        safe = false;//It's unsafe
                    }
                }
                else//If the next number is greater than the current number
                {
                    if (hashList[i][j + 1] - hashList[i][j] > 3 || hashList[i][j + 1] - hashList[i][j] < -3 || hashList[i][j + 1] - hashList[i][j] == 0)//If the difference is greater than 3 OR zero
                    {
                        //Debug.Log(hashList[i][j] + " and " + hashList[i][j+1] + " are unsafe");
                        safe = false;//It's unsafe
                    }
                }

            }
            safe = checkDirection(hashList[i], safe);//Ensure all numbers are either ascending or descending  

            //Debug.Log("Final Verdict: " + safe + " |" + i);
            safetyList.Add(safe);//Add whether or not this list is safe
        }
        finalInt = checkSafety(safetyList);//Find all safe lists
        if (finalInt >= 475)//Known incorrect 'too high' value
        {
            //Debug.Log("ERROR: Calculated value too high; value: " + finalInt);
            if (finalInt == 628 || finalInt == 475 || finalInt == 525)
            {
                //Debug.Log("Incorrect value is the same as known incorrect value");
            }

        }
        else
        {
            Debug.Log("run1 Final Int: " + finalInt);
        }
        //Debug.Log(hashList[0][0]);//Print out the item contained in the first list object's first position
    }

    public void run2()
    {
        readLists();//Read in all the numbers
        int finalInt;
        runLogic(hashList);
        finalInt = checkSafety(safetyList);//Find all safe lists
        for (int i = 0; i < unsafeList.Count; i++)//Parse through all the known false entries in the unsafeList
        {                                   
            for (int j = 0; j < hashList[unsafeList[i]].Count; j++)//Parse through every list entry in a single false list
            {
                List<int> modifiedList = new List<int>();//Create a new list to store the values from this entry
                for(int k = 0; k < hashList[unsafeList[i]].Count; k++)//Duplicate the list we're testing to remove values from
                {
                    modifiedList.Add(hashList[unsafeList[i]][k]);
                }
                modifiedList.RemoveAt(j);//Remove a single entry from the modified list
                if(runSingleCheck(modifiedList))//Run a single check of a list known to be false, with one of the entries ignored
                {
                    safetyList[unsafeList[i]] = true;//If it comes back true, we know a single value can be removed
                }
                
            }
        }
        finalInt = checkSafety(safetyList);
        if (finalInt >= 668)
        {
            Debug.Log("ERROR: Final Value too high. Final Value: " + finalInt);
            if (finalInt == 668)
            {
                Debug.Log("Final Value same as known incorrect final value.");
            }
        }
        else
        {
            Debug.Log("run2 Final Int: " + finalInt);
        }

    }

    void readLists()//Read in the lists
    {
        string line;//The text contained on a particular line
        int count = 0;//How many lines have been read
        while ((line = reader.ReadLine()) != null)//While there are still lines to be read
        {
            string[] words = line.Split(char.Parse(" "));//Split words based on spaces
            hashList.Add(new List<int>());//Create a new nested list
            for (int i = 0; i < words.Length; i++)//For each word in the list
            {
                hashList[count].Add(int.Parse(words[i]));//Extract the number and add it to the nested list

            }


            count = count + 1;//A line has been parsed; create a new nested list entry to store the next line
        }
    }

    void runLogic(List<List<int>> provList)
    {
        bool safe;
        for (int i = 0; i < hashList.Count; i++)//Go through each entry in the list, which is a list in itself
        {
            safe = true;//Assume that the value is initially safe
            for (int j = 0; j < hashList[i].Count - 1; j++)//Go through each entry in the nested list (except the last) which contains the numbers
            {
                //Debug.Log("Checking " + hashList[i][j] + " and " + hashList[i][j+1] + " |" + j);
                if (hashList[i][j] > hashList[i][j + 1])//If the next number is less than the current number
                {
                    if (hashList[i][j] - hashList[i][j + 1] > 3 || hashList[i][j] - hashList[i][j + 1] < -3 || hashList[i][j] - hashList[i][j + 1] == 0)//If the difference is greater than 3 OR zero
                    {
                        //Debug.Log(hashList[i][j] + " and " + hashList[i][j+1] + " are unsafe");
                        safe = false;//It's unsafe
                    }
                }
                else//If the next number is greater than the current number
                {
                    if (hashList[i][j + 1] - hashList[i][j] > 3 || hashList[i][j + 1] - hashList[i][j] < -3 || hashList[i][j + 1] - hashList[i][j] == 0)//If the difference is greater than 3 OR zero
                    {
                        //Debug.Log(hashList[i][j] + " and " + hashList[i][j+1] + " are unsafe");
                        safe = false;//It's unsafe
                    }
                }

            }
            safe = checkDirection(hashList[i], safe);//Ensure all numbers are either ascending or descending  

            Debug.Log("Final Verdict: " + safe + " |" + i);
            if (safe)
            {
                safetyList.Add(safe);//Add whether or not this list is safe
            }
        }
    }

    bool runSingleCheck(List<int> singleList)
    {
        bool safe = true;
        bool increasing;//If the list is increasing
        if (singleList[0] < singleList[1])//If the first number is less than the second, meaning that the values are increasing
        {
            increasing = true;//The values are increasing
        }
        else//Otherwise the values are decreasing
        {
            increasing = false;//The values are not increasing
        }
        for (int i = 0; i < singleList.Count - 1; i++)
        {

            if (singleList[i] > singleList[i + 1])
            {
                if (singleList[i] - singleList[i + 1] > 3 || singleList[i] - singleList[i + 1] < -3 || singleList[i] - singleList[i + 1] == 0)//If the difference is greater than 3 OR zero
                {
                    safe = false;
                }
            }
            else
            {
                if (singleList[i + 1] - singleList[i] > 3 || singleList[i + 1] - singleList[i] < -3 || singleList[i + 1] - singleList[i] == 0)//If the difference is greater than 3 OR zero
                {
                    safe = false;
                }
            }
            if (increasing)//If the values are increasing
            {
                if (singleList[i] >= singleList[i + 1])//If the next value isn't bigger than the last, or is equal to it
                {
                    //Debug.Log(readableList[i] + " is decreasing to " + readableList[i+1]);
                    safe = false;


                }
            }
            else//If the values are decreasing
            {
                if (singleList[i] <= singleList[i + 1])//If the next value isn't smaller than the last, or is equal to it
                {
                    //Debug.Log(readableList[i] + " is increasing to " + readableList[i+1]);
                    safe = false;
                }
            }

        }
        return safe;
    }
    /**
    * Check the direction numbers in a list are moving
    * @readableList - The list of numbers to check
    */
    bool checkDirection(List<int> readableList, bool safety)
    {
        bool increasing;//If the list is increasing
        if (readableList[0] < readableList[1])//If the first number is less than the second, meaning that the values are increasing
        {
            increasing = true;//The values are increasing
        }
        else//Otherwise the values are decreasing
        {
            increasing = false;//The values are not increasing
        }

        for (int i = 0; i < readableList.Count - 1; i++)//Read each value in the list, except the last
        {
            if (increasing)//If the values are increasing
            {
                if (readableList[i] >= readableList[i + 1])//If the next value isn't bigger than the last, or is equal to it
                {
                    //Debug.Log(readableList[i] + " is decreasing to " + readableList[i+1]);
                    safety = false;
                    totalViolations = totalViolations + 1;//Increase the total number of violations by one

                }
            }
            else//If the values are decreasing
            {
                if (readableList[i] <= readableList[i + 1])//If the next value isn't smaller than the last, or is equal to it
                {
                    //Debug.Log(readableList[i] + " is increasing to " + readableList[i+1]);
                    safety = false;
                    totalViolations = totalViolations + 1;//Increase the total number of violations by one
                }
            }
        }
        return safety;//Return whether or not the provided list is 'Safe'
    }

    int checkSafety(List<bool> safetyList)//Read the number of safe values from a list
    {
        int count = 0;//Set the total number of safe values to 0 initially
        for (int i = 0; i < safetyList.Count; i++)//For each entry in the list
        {
            if (safetyList[i])//If the entry reads 'true'
            {
                count = count + 1;//Increase the number of known safe entries by one
            }
            else
            {
                unsafeList.Add(i);
            }
        }
        return count;//Return how many entries are safe
    }
}
