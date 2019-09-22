﻿using System;
using System.Collections.Generic;
 using System.Drawing;
 using System.Text;

namespace RPG
{
    static class Message
    {
        private static List<string> messageList = new List<string>();
        public static void AfficheInfo()
        {
            if (!IsEmpty())
            {
                ConsoleColor ancienneCouleur = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                foreach (string element in messageList)
                {
                    Console.WriteLine(element);
                }
            
                Console.ForegroundColor = ancienneCouleur;
                messageList.Clear();
            }
        }

        public static void Add(string message)
        {
            messageList.Add(message);
        }

        public static int GetCount()
        {
            return messageList.Count;
        }

        public static bool IsEmpty()
        {
            return messageList.Count == 0;
        }
    }
}
