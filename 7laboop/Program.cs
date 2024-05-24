using System;

namespace lab
{
    public class DoublyLinkedListNode
    {
        public long Data { get; set; }
        public DoublyLinkedListNode Next { get; set; }
        public DoublyLinkedListNode Prev { get; set; }

        public DoublyLinkedListNode(long data)
        {
            Data = data;
            Next = null;
            Prev = null;
        }
    }

    public class DoublyLinkedList
    {
        private DoublyLinkedListNode head;
        private DoublyLinkedListNode tail;
        private int count;


        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            count = 0;

        }

        public void AddToEnd(long data)
        {
            DoublyLinkedListNode newNode = new(data);

            if (head == null)
            {
                head = newNode;
                tail = newNode;
            }
            else
            {
                tail.Next = newNode;
                newNode.Prev = tail;
                tail = newNode;
            }

            count++;
        }

        private DoublyLinkedListNode GetNodeAt(int index)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(index);

            if (index >= count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            DoublyLinkedListNode current = head;
            int currentIndex = 0;

            while (current != null)
            {
                if (currentIndex == index)
                {
                    return current;
                }
                current = current.Next;
                currentIndex++;
            }

            throw new ArgumentOutOfRangeException(nameof(index));
        }

        // Індексатор
        public long this[int index]
        {
            get
            {
                var node = GetNodeAt(index);
                return node.Data;
            }
            set
            {
                var node = GetNodeAt(index);
                node.Data = value;
            }
        }

        public DoublyLinkedListNode FindFirstMultiple(long divisor)
        {
            DoublyLinkedListNode current = head;

            for (int i = 0; i < count; i++)
            {
                if (this[i] % divisor == 0)
                {
                    return GetNodeAt(i);
                }
            }

            return null;
        }

        public long SumOfEvenPosElements()
        {
            long sum = 0;

            for (int i = 0; i < count; i += 2)
            {
                sum += this[i];
            }

            return sum;
        }

        public DoublyLinkedList GetElementGreater(long value)
        {
            DoublyLinkedList result = new();

            for (int i = 0; i < count; i++)
            {
                if (this[i] > value)
                {
                    result.AddToEnd(this[i]);
                }
            }

            return result;
        }

        public void RemoveElementsGreaterThanAverage()
        {
            double average = CalculateAverage();

            DoublyLinkedListNode current = head;

            while (current != null)
            {
                DoublyLinkedListNode next = current.Next;

                if (current.Data > average)
                {
                    RemoveNode(current);
                }

                current = next;
            }
        }

        private double CalculateAverage()
        {
            if (count == 0)
                return 0;

            long sum = 0;

            for (int i = 0; i < count; i++)
            {
                sum += this[i];
            }

            return (double)sum / count;
        }

        private void RemoveNode(DoublyLinkedListNode node)
        {
            if (node == head)
            {
                head = head.Next;

                if (head != null)
                    head.Prev = null;
                else
                    tail = null;
            }
            else if (node == tail)
            {
                tail = tail.Prev;

                if (tail != null)
                    tail.Next = null;
                else
                    head = null;
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
            count--;
        }

        public void PrintList()
        {
            DoublyLinkedListNode current = head;

            while (current != null)
            {
                Console.WriteLine(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main()
        {
            DoublyLinkedList list = new();
            list.AddToEnd(11);
            list.AddToEnd(14);
            list.AddToEnd(21);
            list.AddToEnd(25);
            list.AddToEnd(30);
            list.AddToEnd(35);

            Console.WriteLine("Original List:");
            list.PrintList();

            Console.WriteLine("\nUsing indexer to access elements:");
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    Console.WriteLine($"Element at index {i}: {list[i]}");
                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine($"Index {i} is out of range.");
                }
            }

            // Приклад виконання завдань

            // Знайти перше входження елементу кратного 5
            var multipleOfFive = list.FindFirstMultiple(5);
            Console.WriteLine($"\nFirst multiple of 5: {multipleOfFive?.Data}");

            // Знайти суму елементів, що розташовані на парних позиціях списку
            long sumEvenPositions = list.SumOfEvenPosElements();
            Console.WriteLine($"Sum of elements at even positions: {sumEvenPositions}");

            // Отримати новий список зі значень елементів більших за задане
            long num = 21;
            DoublyLinkedList greaterThan = list.GetElementGreater(num);
            Console.WriteLine($"Elements greater than {num}:");
            greaterThan.PrintList();

            // Видалити елементи більші за середнє значення
            list.RemoveElementsGreaterThanAverage();
            Console.WriteLine("List after removing elements greater than average:");
            list.PrintList();
        }
    }
}
