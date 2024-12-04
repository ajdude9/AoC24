using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DayFourScript : MonoBehaviour
{
    StreamReader reader = new StreamReader("Inputs/input4.txt");
    List<List<char>> hashList = new List<List<char>>();

    public void run1()
    {
        int finalInt = 0;
        readLists();
        finalInt = wordSearch(hashList);
        Debug.Log(finalInt);
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
            string word = line;//Split words based on spaces
            hashList.Add(new List<char>());//Create a new nested list
            char[] letters = word.ToCharArray();
            for (int i = 0; i < letters.Length; i++)//For each word in the list
            {
                hashList[count].Add(letters[i]);//Extract the number and add it to the nested list

            }
            count = count + 1;//A line has been parsed; create a new nested list entry to store the next line
        }
        //  Debug.Log(hashList[0][0]);
    }

    int wordSearch(List<List<char>> map)
    {
        Debug.Log("Running Wordsearch");
        int totalXmas = 0;
        for (int i = 0; i < map.Count; i++)//For the size of the hashmap
        {
            Debug.Log("Current line depth: " + i + "/" + map.Count);
            for (int j = 0; j < map[i].Count; j++)//For the size of all the characters on one particular line
            {
                //Debug.Log("Reading Y: " + i + " and X: " + j);
                //Debug.Log("Char: " + map[i][j] + " |" + i + j);
                
                
                if (map[i][j].Equals('X'))//We only need to care about possible words starting with X
                {
                    Debug.Log("Char is X!");
                    //North ↑
                    if (i > 2)
                    {
                        Debug.Log("NORTH: Searching from " + i + " to (-) " + (i - 3));
                        char[] chars = { map[i][j], map[i - 1][j], map[i - 2][j], map[i - 3][j] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            Debug.Log("XMAS found!");
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //Northeast ↗
                    if (i > 2 && j < (map[i].Count - 3))
                    {
                        Debug.Log("NORTHEAST: Searching from Y: " + i + " and X: " + j + " to " + "(-) Y: " + (i - 3) + " and (+) X: " + (j + 3));
                        char[] chars = { map[i][j], map[i - 1][j + 1], map[i - 2][j + 2], map[i - 3][j + 3] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //East → 
                    if (j < map[i].Count - 3)
                    {
                        Debug.Log("EAST: Searching from " + j + " to (+) " + (j + 3));
                        char[] chars = { map[i][j], map[i][j + 1], map[i][j + 2], map[i][j + 3] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //Southeast ↘
                    if (i < map.Count - 3 && j < map[i].Count - 3)
                    {
                        Debug.Log("SOUTHEAST: Searching from Y: " + i + " and X: " + j + " to " + "(+) Y: " + (i + 3) + " and (+) X: " + (j + 3));
                        char[] chars = { map[i][j], map[i + 1][j + 1], map[i + 2][j + 2], map[i + 3][j + 3] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //South ↓
                    if (i < map.Count - 3)
                    {
                        Debug.Log("SOUTH: Searching from " + i + " to (+) " + (i + 3));
                        char[] chars = { map[i][j], map[i + 1][j], map[i + 2][j], map[i + 3][j] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //Southwest ↙
                    if (i < map.Count - 3 && j > 2)
                    {
                        Debug.Log("SOUTHWEST: Searching from Y: " + i + " and X: " + j + " to " + "(+) Y: " + (i + 3) + " and (-) X: " + (j - 3));
                        char[] chars = { map[i][j], map[i + 1][j - 1], map[i + 2][j - 2], map[i + 3][j - 3] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //West ←
                    if (j > 2)
                    {
                        Debug.Log("WEST: Searching from " + j + " to (-) " + (j - 3));
                        char[] chars = { map[i][j], map[i][j - 1], map[i][j - 2], map[i][j - 3] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                    //Northwest ↖
                    if (i > 2 && j > 2)
                    {
                        Debug.Log("NORTHWEST: Searching from Y: " + i + " and X: " + j + " to " + "(-) Y: " + (i - 3) + " and (-) X: " + (j - 3));
                        char[] chars = { map[i][j], map[i - 1][j - 1], map[i - 2][j - 2], map[i - 3][j - 3] };
                        string xmas = new string(chars);
                        if (xmas == "XMAS")
                        {
                            totalXmas = totalXmas + 1;
                        }
                    }
                }
            }
        }
        return totalXmas;
    }
}
