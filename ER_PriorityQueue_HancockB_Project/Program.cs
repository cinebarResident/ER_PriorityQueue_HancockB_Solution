// Brittany Hancock
// IT113
// Notes:  None
// Behaviors not implemented and why: n/a

using System.IO;

namespace ER_Priority_Queue_Hancock_Project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "Patients.csv";
            PriorityQueue<Patient, (int,DateTime)> priorityQueue = new PriorityQueue<Patient, (int,DateTime)>();
            priorityQueue = ReadPatientListFromCsv(path);


            Console.Write(Menu());
            bool go = true;
            while (go)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                char keyChar = char.ToUpper(keyInfo.KeyChar);

                if (keyChar == 'A')
                {
                    Console.WriteLine("Add action triggered");
                    int patientCount = AddPatient(priorityQueue);
                    Console.WriteLine(patientCount + " Patients in Queue");
                    Console.WriteLine();
                    Console.Write(Menu());
                }
                else if (keyChar == 'P')
                {
                    Console.Clear();
                    Console.WriteLine("Process action triggered");
                    ProcessPatient(priorityQueue);
                    Console.WriteLine();
                    Console.Write(Menu());
                }
                else if (keyChar == 'L')
                {
                    Console.WriteLine("List action triggered");
                    Census(priorityQueue);
                    Console.WriteLine();
                    Console.Write(Menu());
                }
                else if (keyChar == 'Q')
                {
                    Console.WriteLine("Exit action triggered.  Exiting program.");
                    go = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid key.  Please use 'A', 'P', 'L', or 'E'." +
                        "\nPlease select an option:  ");
                }
            }
        }

        static void ProcessPatient(PriorityQueue<Patient, (int, DateTime)> priorityQueue)
        {
            if (priorityQueue.Count > 0)
            {
                Patient patient = priorityQueue.Dequeue();
                Console.WriteLine("\n" + String.Format("{0,-15} {1,-20} {2,-17} {3,8}", "FIRST NAME", "LAST NAME", "DOB", "PRIORITY"));
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("The queue is empty.  No patients to process.");
            }
        }

        static void Census(PriorityQueue<Patient, (int, DateTime)> priorityQueue)
        {
            int count = priorityQueue.Count;

            List<Patient> tempPatientList = new();
            Console.WriteLine("\n" + String.Format("{0,-15} {1,-20} {2,-17} {3,8}", "FIRST NAME", "LAST NAME", "DOB", "PRIORITY"));
            for (int i = 0; i < count; i++)
            {
                if (priorityQueue.Count != 0)
                {
                    Patient tempPatient = priorityQueue.Dequeue();
                    Console.WriteLine(tempPatient.ToString());
                    tempPatientList.Add(tempPatient);
                }
            }
            foreach (Patient patient in tempPatientList)
            {
                priorityQueue.Enqueue(patient, (patient.Priority, patient.ArrivalTime));
            }
        }

        static PriorityQueue<Patient, (int, DateTime)> ReadPatientListFromCsv(string path)
        {
            PriorityQueue<Patient, (int,DateTime)> priorityQueue = new PriorityQueue<Patient, (int, DateTime)>();

            string[] lines = File.ReadAllLines(path);

            foreach (string line in lines)
            {

                string[] values = line.Split(',');

                string firstName = values[0];
                string lastName = values[1];
                bool tryDate = DateTime.TryParse(values[2], out DateTime dateOfBirth);
                bool tryPriority = int.TryParse(values[3], out int priority);
                if (tryDate && tryPriority)
                {
                    DateTime arrivalTime = DateTime.Now;
                    Patient patient = new Patient(firstName, lastName, dateOfBirth, priority, arrivalTime);
                    priorityQueue.Enqueue(patient, (patient.Priority, patient.ArrivalTime));
                }
            }
            return priorityQueue;
        }

        static string Menu()
        {
            return "Menu Options:\n   - Press 'A' to Add a New Patient" +
                "\n   - Press 'P' to Process the Current Patient" +
                "\n   - Press 'L' to List All Patients in Queue" +
                "\n   - Press 'Q' to Quit the Application" +
                "\n\nPlease select an option:  ";
        }

        static int AddPatient(PriorityQueue<Patient, (int, DateTime)> priorityQueue)
        {
            Console.WriteLine("Please enter the following details:");
            Console.Write("Enter First Name:  ");
            string firstName = Console.ReadLine();
            Console.Write("Enter Last Name:  ");
            string lastName = Console.ReadLine();

            DateTime dateOfBirth;
            while (true)
            {
                Console.Write("Enter Date of Birth (mm/dd/yyyy):  ");
                string dobInput = Console.ReadLine();
                if (DateTime.TryParse(dobInput, out dateOfBirth))
                {
                    break;
                }
                Console.WriteLine("Invalid date format.  Please try again.");
            }

            int priority;
            while (true)
            {
                Console.Write("Enter Priority (1-5, 1 being lowest and 5 highest):  ");
                if (int.TryParse(Console.ReadLine(), out priority))
                {
                    break;
                }
                Console.WriteLine("Invalid priority.  Please enter a number between 1 and 5.");
            }
            DateTime arrivalTime = DateTime.Now;
            Patient patient = new Patient(firstName, lastName, dateOfBirth, priority, arrivalTime);
            priorityQueue.Enqueue(patient, (patient.Priority, patient.ArrivalTime));

            return priorityQueue.Count;
        }
    }
}