using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeSerializeDeserialize.classes
{
    class ListRand
    {
        public ListNode Head;
        public ListNode Tail;
        public int count;

        public void Serialize(FileStream s)
        {            
            List<ListNode> checkedListNode = new List<ListNode>();
            List<ListNodeIndex> dataListNode = new List<ListNodeIndex>();            

            readNode(Head, checkedListNode, dataListNode);
            readNode(Tail, checkedListNode, dataListNode);
            count = dataListNode.Count();
            using (StreamWriter writer = new StreamWriter(s))
            {
                writer.WriteLine(count);
                foreach (ListNodeIndex data in dataListNode)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(data.index).Append("-");
                    builder.Append(data.indexPrev).Append("-");
                    builder.Append(data.indexNext).Append("-");
                    builder.Append(data.indexRand).Append("-");
                    builder.Append(data.data);
                    writer.WriteLine(builder.ToString());
                }
            }
            
        }

        public void Deserialize(FileStream s)
        {
            List<ListNode> resultListNode = new List<ListNode>();
            using(StreamReader reader = new StreamReader(s))
            {
                int count = int.Parse(reader.ReadLine());
                this.count = count;
                for (int i = 0; i < count; i++)
                {
                    resultListNode.Add(new ListNode());
                }
                for (int i = 0; i < count; i++)
                {
                    String readData = reader.ReadLine();
                    String[] splitData = readData.Split('-');
                    if (splitData[1] != "#")
                        resultListNode[i].prev = resultListNode[int.Parse(splitData[1])];
                    if (splitData[2] != "#")
                        resultListNode[i].next = resultListNode[int.Parse(splitData[2])];
                    if (splitData[3] != "#") resultListNode[i].rand = resultListNode[int.Parse(splitData[3])];
                    resultListNode[i].data = splitData[4];                    
                }
            }
            Head = resultListNode[0];
            Tail = resultListNode.Last();
            
        }      

        private int findAtList(ListNode node, List<ListNode> list)
        {
            ListNode findNode = list.Where(o => o == node).FirstOrDefault();
            if (findNode == null)
            {                
                list.Add(node);
                return list.IndexOf(node);
            }
            else
            {
                return list.IndexOf(findNode);
            }
        }

        private int readNode(ListNode node, List<ListNode> checkedList, List<ListNodeIndex> dataList)
        {
            Console.WriteLine("Node data: " + node.data);
            int index = findAtList(node, checkedList);
            ListNodeIndex data = dataList.Where(o => o.index == index.ToString()).FirstOrDefault();            
            if (data == null)
            {
                data = new ListNodeIndex();
                data.index = index.ToString();
                dataList.Add(data);
                if (node.prev != null)
                    data.indexPrev = readNode(node.prev, checkedList, dataList).ToString();                
                else
                    data.indexPrev = "#";
                if (node.next != null)
                    data.indexNext = readNode(node.next, checkedList, dataList).ToString();
                else
                    data.indexNext = "#";
                if (node.rand != null)
                    data.indexRand = readNode(node.rand, checkedList, dataList).ToString();
                else
                    data.indexRand = "#";
                data.data = node.data;               
            }                       
            return index;                                    
        }
    }
}
