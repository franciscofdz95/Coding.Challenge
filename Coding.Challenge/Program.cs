using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Coding.Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                //Read and store input from user
                Console.WriteLine("Type the word: ");
                string word = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(word))
                {
                    ConvertString(word);
                }
                else
                {
                    Console.WriteLine("--------------------------------");
                    Console.WriteLine("Error. You need to type a word.");
                }


                //Try again or exit the program
                Console.WriteLine("Want to try again? Press any key to continue, N or n to exit:");
                if (Console.ReadKey().Key == ConsoleKey.N)
                {
                    break;
                }
            }

            //test();
        }

        static void ConvertString(string wordToConvert)
        {
            //Get all words separately
            var stringArray = wordToConvert.Split(' ');
            List<string> newWord = new List<string>();

            //Search ASCII Chars
            string pattern = "[a-z]+";

            //Loop through splitted words
            foreach (string word in stringArray)
            {
                //If a word has more than 2 characters program will find how many chars are between the first and last character
                if (word.Length > 2)
                {
                    //Split the word to find non-alphabetic characters
                    var result = Regex.Split(word, pattern, RegexOptions.IgnoreCase).ToList().Where(c => c.Length > 0).ToArray();

                    // The word doesnt contain any special characters
                    if (!result.Any())
                    {
                        newWord.Add(GetFormattedStringAndCount(word));
                    }
                    else
                    {
                        //Split word in other word without special characters
                        var separatedStrings = word.Split(result, StringSplitOptions.None).Where(s => !string.IsNullOrEmpty(s)).ToArray();
                        string currentWord = string.Empty;

                        // The word is all special characters
                        if (!separatedStrings.Any())
                        {
                            currentWord = word;
                        }
                        else
                        {
                            bool separatorFirst = string.IsNullOrEmpty(Regex.Match(word, "^[a-z]", RegexOptions.IgnoreCase).Value);

                            //For to send the separated strings
                            for (int i = 0; i < separatedStrings.Length; i++)
                            {
                                if (i < result.Length)
                                {
                                    currentWord += GetFormattedStringAndCount(separatedStrings[i], result[i], separatorFirst);
                                }
                                else
                                {
                                    currentWord += GetFormattedStringAndCount(separatedStrings[i]);
                                }
                            }
                        }

                        newWord.Add(currentWord);
                    }
                }
                //Keep the word as original
                else
                {
                    newWord.Add(word);
                }
            }
            //Get a string with all the splitted word formatted together
            var output = String.Join(" ", newWord.ToArray());

            //Print result
            Console.WriteLine("--------------------------------");
            Console.WriteLine("Output: " + output);
        }

        static string GetFormattedStringAndCount(string input = null, string separator = null, bool separatorFirst = false)
        {
            if (input?.Length >= 2)
            {
                //Get First and last char
                string firstChar = input.First().ToString();
                string lastChar = input.Last().ToString();

                var innerString = input.Substring(1, input.Length - 2);
                //Get count of repeated chars
                var repeatedChars = innerString.GroupBy(x => x).Count();

                //Join the first and last character with repeated chars count
                string numberingString = firstChar + repeatedChars + lastChar;

                return separatorFirst ? separator + numberingString : numberingString + separator;
            }

            return separatorFirst ? separator + input : input + separator;
        }

    }


}
