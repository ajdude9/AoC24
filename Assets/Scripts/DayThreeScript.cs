using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using UnityEngine;
using System.Globalization;
using Unity.VisualScripting;

public class DayThreeScript : MonoBehaviour
{
    static readonly string inputFile = "Inputs/input3.txt";
    string input;
    public void run1()
    {
        
        readFiles();
        string pattern = @"mul\([0-9]{1,3}\,[0-9]{1,3}\)";
        string numbers = @"[0-9]{1,3}\,[0-9]{1,3}";
        int counter = 0;
        int finalInt = 0;
        List<int> multiplyMap = new List<int>();
        MatchCollection matches = Regex.Matches(input, pattern);
        foreach (Match match in matches)
        {            
            string literal = match.ToString();
            MatchCollection numberMatches = Regex.Matches(literal, numbers);            
            foreach (Match numMatch in numberMatches)
            {
                string literalNumbers = numMatch.ToString();
                string[] nums = literalNumbers.Split(char.Parse(","));
                multiplyMap.Add(int.Parse(nums[0]) * int.Parse(nums[1]));
            }
            counter = counter + 1;
        }        
        for(int i = 0; i < multiplyMap.Count; i++)
        {
            finalInt = finalInt + multiplyMap[i];
        }
        Debug.Log(finalInt);
        
    }

    public void run2()
    {
        readFiles();
        string pattern = @"(mul\([0-9]{1,3}\,[0-9]{1,3}\))|(do\(\))|(don't\(\))";
        string numbers = @"[0-9]{1,3}\,[0-9]{1,3}";
        MatchCollection matches = Regex.Matches(input, pattern);
        int counter = 0;
        int finalInt = 0;
        bool allowMul = true;
        List<string> literals = new List<string>();
        List<int> multiplyMap = new List<int>();
        foreach (Match match in matches)
        {
            string literal = match.ToString();
            literals.Add(literal);//Add every found RegEx item into an ordered list

        }
        for (int i = 0; i < literals.Count; i++)//Parse through the list of every matched RegEx find - either mul, do or don't
        {
            string identifier = literals[i].Substring(0, 4);//Read the first four characters of the string, resulting in either 'mul(', 'do()' or "don'")
            switch (identifier)//Act based on what is found
            {
                case "mul("://If it's a multiply statement
                    if (allowMul)//If multiplication is currently allowed
                    {
                        MatchCollection numberMatches = Regex.Matches(literals[i], numbers);
                        foreach (Match numMatch in numberMatches)
                        {
                            string literalNumbers = numMatch.ToString();
                            string[] nums = literalNumbers.Split(char.Parse(","));
                            multiplyMap.Add(int.Parse(nums[0]) * int.Parse(nums[1]));
                        }
                    }
                    break;
                case "do()"://If it's a do statement
                    allowMul = true;//Enable multiplication
                    break;
                case "don'"://If it's a don't statement
                    allowMul = false;//Disable multiplication
                    break;

            }
        }
        for(int i = 0; i < multiplyMap.Count; i++)
        {
            finalInt = finalInt + multiplyMap[i];
        }
        Debug.Log(finalInt);

    }

    void readFiles()
    {
        input = File.ReadAllText(inputFile);
        //Debug.Log(input);
    }
}
