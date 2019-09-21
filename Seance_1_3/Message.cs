﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Seance_3_1
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

        public static void AddMessage(string message)
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
