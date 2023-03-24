using System;
using System.IO;
using System.Collections.Generic;

namespace HangmanGameButSimpler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("galgenraten THE GAME");
            var randomGen = RandomWordAndCount();
            string word = randomGen.Item1;
            int count = randomGen.Item2;
            List<char> whatLettersAlready = new List<char>();
            int hp = 10; //10 versuche



            //Console.WriteLine(word + " " + count);


            string blanks = "";
            for(int i = 0; i < count; i++)
            {
                blanks += "_";
            }
            char[] blanks_char = blanks.ToCharArray();
            Console.WriteLine(blanks);            

            while (true)
            {

                try
                {
                    Console.Write("\n\nEnter a letter: ");
                    char input = char.ToUpper(char.Parse(Console.ReadLine()));
                    whatLettersAlready = AddInputToList(whatLettersAlready, input);
                    var isGuessWrite = IsInTheWordAndWhere(input, word);

                    if (isGuessWrite.Item1) //if its true
                    {
                        
                        Console.WriteLine("\nIt's in there!");
                        //on those indexes:

                        foreach(int elem2 in isGuessWrite.Item2){
                            blanks_char[elem2] = input;
                            
                        }
                        foreach(char elem in blanks_char){

                            Console.Write(elem);

                        }

                        //if you won
                        string checkIfWordIsGuessed = "";
                        //convert char array to string because the .ToString method doesnt work for some reason lol

                        foreach(char elem5 in blanks_char){
                            checkIfWordIsGuessed += elem5;
                        }
                        if(checkIfWordIsGuessed == word){
                            Console.WriteLine("\n\n---CONGRATS YOU WON!---\n");

                            break;
                        }

                    }
                    else{
                        hp--;
                        Console.WriteLine("\nNope not in there... Remaining trys: " + hp);
                        foreach(char elem in blanks_char){
                            Console.Write(elem);
                        }
                        Console.WriteLine("\nLetters already used: ");
                        foreach(char elem3 in whatLettersAlready){
                            Console.Write(elem3 + ";");
                        }
                        if(hp <= 0){
                            Console.WriteLine("\n---GAME OVER---\nThe word was: " + word);
                            break;
                        }

                    }

                }
                catch (Exception)
                {
                    Console.WriteLine("One valid letter at the time");
                    

                }

            }


            
        }

        static List<char> AddInputToList(List<char> list, char input){
            if(list.Contains(input)){
                return list;
            }
            else{
                list.Add(input);
                return list;
            }
        }

        static (string,int) RandomWordAndCount()
        {
            Random r = new Random();


            string[] arr = File.ReadAllLines("./words.txt");
            int r_word = r.Next(0, arr.Length);

            string genword = arr[r_word];

            int count = genword.Length;
            return (genword, count);

        }
        static (bool,List<int>) IsInTheWordAndWhere(char input, string word)
        {
            List<int> indexOfLetters = new List<int>();

            if (word.Contains(input))
            {
                //wenn ja wo genau
                for(int i = 0; i < word.Length; i++)
                {
                    if(word[i] == input){
                        indexOfLetters.Add(i);
                    }
                    
                }
                return (true, indexOfLetters);
            }
            return (false,indexOfLetters);


        }
    }
}
