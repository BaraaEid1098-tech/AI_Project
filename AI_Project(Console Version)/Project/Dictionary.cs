using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal  static class Dictionary
    {
        public static List<string> dictionary =  File.ReadAllLines("E:\\Dictionary.txt").ToList();


        public static bool CheckDictionary(Users users)
        {
            if (users.word == null)
            {
               return false;
            }
            else if(dictionary.Contains(users.word))
            { 
                    return true;
            }
            else
            {
                    return false;
            }
            
        }

    }
}
