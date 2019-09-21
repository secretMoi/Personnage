﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RPG
{
    static class Message
    {
        private static List<string> messageList = new List<string>();
        public static void AfficheInfo()
        {
            foreach (string element in messageList)
            {
                Console.WriteLine(element);
            }

            messageList.Clear();
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
            return messageList.Count != 0;
        }
    }
}
