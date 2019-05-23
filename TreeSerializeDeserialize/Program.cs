using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeSerializeDeserialize.classes;

namespace TreeSerializeDeserialize
{
    class Program
    {
        static void Main(string[] args)
        {
            ListNode node_head = new ListNode();
            node_head.data = "HEAD";
            ListNode node_tail = new ListNode();
            node_tail.data = "TAIL";

            ListNode node_1 = new ListNode();
            node_1.data = "FIRST";          
            ListNode node_2 = new ListNode();
            node_2.data = "SECOND";
            ListNode node_3 = new ListNode();
            node_3.data = "THIRD";
            ListNode node_4 = new ListNode();
            node_4.data = "FOURTH";
            ListNode node_5 = new ListNode();
            node_5.data = "FIFTH";
            ListNode node_6 = new ListNode();
            node_6.data = "SIXTH";

            node_head.next = node_1;
            node_head.rand = node_3;

            node_1.prev = node_head;
            node_1.next = node_2;
            node_1.rand = node_3;

            node_2.prev = node_1;
            node_2.next = node_3;
            node_2.rand = node_1;

            node_3.prev = node_2;
            node_3.next = node_5;
            node_3.rand = node_4;

            node_4.prev = node_3;
            node_4.next = node_5;
            node_4.rand = node_6;

            node_5.prev = node_3;
            node_5.next = node_tail;
            node_5.rand = node_3;

            node_6.prev = node_4;
            node_6.next = node_tail;
            node_6.rand = node_2;

            node_tail.prev = node_5;


            ListRand listRand = new ListRand();
            listRand.Head = node_head;
            listRand.Tail = node_tail;

            FileStream fileStream = new FileStream("output.txt", FileMode.Truncate, FileAccess.ReadWrite);
            fileStream.Close();
            fileStream = new FileStream("output.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);            
            listRand.Serialize(fileStream);
            fileStream.Close();

            fileStream = new FileStream("output.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            ListRand listRandDes = new ListRand();
            listRandDes.Deserialize(fileStream);
            fileStream.Close();
            FileStream fileStreamTest = new FileStream("output_1.txt", FileMode.Truncate, FileAccess.ReadWrite);
            fileStreamTest.Close();
            fileStreamTest = new FileStream("output_1.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            listRandDes.Serialize(fileStreamTest);
            fileStreamTest.Close();
        }
    }
}
