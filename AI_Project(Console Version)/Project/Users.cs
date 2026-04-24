using System;
using System.Collections.Generic;
using System.Text;

namespace Project
{
    internal class Users
    {
        public Users(uint hearts)
        {
            this.hearts = hearts;

        }
        public Users()
        {

        }
        public uint hearts { get; set; }
        public string word { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static bool CheckScore(Users user)
        {
            if (user.hearts > 0)
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
