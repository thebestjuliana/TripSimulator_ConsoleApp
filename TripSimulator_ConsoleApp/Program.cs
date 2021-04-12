using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace TripSimulator_ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Car car1 = new Car("Betty Beep", 12, 12, 144);
            Car car2 = new Car("Batmobile", 30, 15, 450);
            List<Car> cars = new List<Car> { car1, car2 };
            
            string welcome = "Welcome to the Trip Simulator";
            Console.WriteLine(welcome);
            bool running = true;
            while (running)
            {
                Trip myTrip = new Trip();
                Console.WriteLine("Where are you planning on traveling?");
                myTrip.destination = Console.ReadLine();
                myTrip.distance = ValidInteger($"How far away is {myTrip.destination}?");
                myTrip.nightsThere = ValidInteger($"How many nights will you stay in a hotel at {myTrip.destination}?");
                myTrip.hotelCost = ValidDouble($"What the the current cost for a hotel?");
                myTrip.gasPrice = ValidDouble($"What is the current Price per gallon?");
                bool inRange = false;
                while (inRange == false)
                {
                    int i = 1;
                    string question = "Which car would you like to drive?: "; 
                        foreach (Car c in cars)
                    {
                        question += $"\nCar: {i}" +
                            $"\n{c.CarName}" +
                            $"\nMpg: {c.mpg}" +
                            $"\nTank size: {c.tankCapacity}\n";
                        i++;
                    }
                    question += $"\nCar: {i}" +
                        $"\nMy own car\n";
                    int carNumber = ValidInteger(question);
                    if (carNumber>cars.Count+1)
                    {
                        Console.WriteLine("Your selection is out of range. Try again.");
                        continue;
                    }
                    else
                    {
                        inRange = true;
                        switch (carNumber-1)
                        {
                            case 0:
                                myTrip.car = cars[0];
                                break;
                            case 1:
                                myTrip.car = cars[1];
                                break;
                            default:
                                myTrip.car = MyCar();
                                break;
                        }
                    }
                }
                Drive(myTrip, myTrip.destination);
                
                Day(myTrip);
                Console.WriteLine("Let's go home!");
                Drive(myTrip, "Home");

                double totalCost = myTrip.souvenierCost + myTrip.fuelCost + myTrip.foodCost + myTrip.hotelCost*myTrip.nightsThere + myTrip.businessCost + myTrip.souvenierCost;
                Console.WriteLine($"Your simulated trip to {myTrip.destination} has completed." +
                    $"On your drive, you stopped {myTrip.stops} time(s) " +
                    $"and spent {totalCost:c}.\nYour itemized breakdown is: " +
                    $"\nFuel: \t{myTrip.fuelCost:c}" +
                    $"\nFood: \t{myTrip.foodCost:c}" +
                    $"\nLodging: \t{myTrip.hotelCost*myTrip.nightsThere:c}" +
                    $"\nBusiness Expenses: \t{myTrip.businessCost:c}" +
                    $"\nSouvenirs: \t{myTrip.souvenierCost:c}");
                Console.WriteLine("Goodbye!");
                running = false;

            }

        }


        public static int ValidInteger(string question)
        {
            bool validInt = false;
            int convertedAnswer = 0;
            while (validInt == false)
            {
                Console.WriteLine(question);
                string answer = Console.ReadLine().Trim().Split()[0];
                validInt = int.TryParse(answer, out convertedAnswer);
                if (validInt == false)
                {
                    Console.WriteLine("I'm sorry, I didn't get that. Please enter numbers only");
                }
            }
            return convertedAnswer;
        }
        public static double ValidDouble(string question)
        {
            bool validDouble = false;
            double convertedAnswer = 0;
            while (validDouble == false)
            {
                Console.WriteLine(question);
                string answer = Console.ReadLine().Trim();
                if (answer.StartsWith('$'))
                {
                   answer= answer.Substring(1);
                }
                validDouble = double.TryParse(answer, out convertedAnswer);
                if (validDouble == false)
                {
                    Console.WriteLine("I'm sorry, I didn't get that. Please enter numbers only");
                }
            }
            return convertedAnswer;
        }
        public static bool YesOrNo(string question)
        {
            bool validAnswer = false;
            bool yesOrNo = true;
            while (validAnswer == false)
            {
                Console.WriteLine(question);

                string answer = Console.ReadLine();
                switch (answer.ToLower())
                {
                    case "yes":
                    case "y":
                    case "absolutely":
                    case "yep":
                        yesOrNo = true;
                        validAnswer = true;
                        break;
                    case "no":
                    case "n":
                        yesOrNo = false;
                        validAnswer = true;
                        break;
                    default:
                        Console.WriteLine("Sorry, I didn't quite get that.");
                        break;
                }


            }
            return yesOrNo;
        }
        public static void Drive(Trip myTrip, string destination)
        {
            Console.WriteLine("Ok! Let's drive!!");
            bool driving = true;
            int currentDistance = 0;

            while (driving)
            {
                currentDistance++;
                myTrip.car.milesUntilEmpty--;
                if (currentDistance % 75 == 0)
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("driving...");
                }
                if (myTrip.car.milesUntilEmpty < 1)
                {
                    Console.WriteLine("We have to stop for gas!");
                    Console.WriteLine($"We have driven {currentDistance} miles!");
                    Console.WriteLine($"We refilled the gas tank for {(myTrip.car.tankCapacity * myTrip.gasPrice):c}");
                    myTrip.fuelCost += (myTrip.car.tankCapacity) * myTrip.gasPrice;
                    myTrip.car.milesUntilEmpty = myTrip.car.tankCapacity * myTrip.car.mpg;
                    myTrip.stops++;

                    bool getSnacks = YesOrNo("Would you like to buy food or drinks during our stop?");

                    while (getSnacks == true)
                    {
                        Console.WriteLine("What do you want to buy?");
                        string snack = Console.ReadLine();
                        double snackCost = ValidDouble($"How much does {snack} cost?");
                        myTrip.foodCost += snackCost;
                        getSnacks = YesOrNo("Do you want another snack?");
                    }
                }

                if (currentDistance >= myTrip.distance)
                {
                    driving = false;
                    Console.WriteLine($"We arrived at {destination}!");
                }
            }
            
        }
        public static void Day(Trip trip)
        {

            for (int i = 1; i <= trip.nightsThere+1; i++)
            {
                Console.WriteLine($"Day {i}: ");
                bool businessExpense = YesOrNo("Are you going to make any business purchases today?");
                while (businessExpense)
                {
                    Console.WriteLine("What are you going to buy?");
                    string purchase = Console.ReadLine();

                    trip.businessCost += ValidDouble($"How much does the {purchase} cost?");
                    businessExpense = YesOrNo("Would you like to make another business purchase today?");

                }
                bool souvenirExpense = YesOrNo("Are you going to purchase any souvenirs today?");
                while (souvenirExpense)
                {
                    Console.WriteLine("What are you going to buy?");
                    string purchase = Console.ReadLine();

                    trip.souvenierCost += ValidDouble($"how much does the {purchase} cost?");
                    souvenirExpense = YesOrNo("Would you like to purchase another souvenir?");

                }
                trip.foodCost += ValidDouble("How much does breakfast cost?");
                trip.foodCost += ValidDouble("How much does lunch cost?");
                trip.foodCost += ValidDouble("How much does dinner cost?");
                bool snacks = YesOrNo("Did you have a snack?");
                if (snacks)
                {
                    trip.foodCost += ValidDouble("How much did you snack cost?");

                }

            }

        }
        public static Car MyCar()
        {

            Console.WriteLine("Now tell me about your car");

            Console.WriteLine("What's your car's name?");
            string CarName = Console.ReadLine();
            int tankCapacity = ValidInteger($"How big is {CarName}'s gas tank?");
            int mpg = ValidInteger($"What is the average mpg for {CarName}?");

            int milesUntilEmpty = tankCapacity * mpg;
            Car car = new Car(CarName, mpg, tankCapacity, milesUntilEmpty);
            return car;
        }
    }
}