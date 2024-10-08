using System;

class Item
{
	public string itemName;
	public int price;
}
class Player
{
	public Item[] inventory = new Item[10];
	public int money;
}
public class Program
{
	static Player player = new Player();
	static Item apple, orange, carrot;
	
	public static void Main()
	{
		int day = 0;
		bool gameOn = true;
		ItemInit();
		player.money = 15;
		Console.WriteLine("You are a trader, trade your way up top!\nCareful: if your balance reaches zero - you can die from disappointment");
		Console.WriteLine("Starting balance: " + player.money);
		while (gameOn)
		{
			Day(day);
			day++;
			if (player.money >= 200)
			{
				Console.WriteLine("You died of happiness");
				gameOn = false;
			}
			else if (player.money <= 0)
			{
				Console.WriteLine("You died because you had too little money..");
				gameOn = false;
			}
			else
				gameOn = true;
		}
		Console.WriteLine("Game Over \nYour results: \n Money: " + player.money);
	}
	static void Day(int day)
	{
		bool dayEnd = false;
		while (dayEnd == false)
			{
				Console.WriteLine("Day: " + day);
				Menu(day, ref dayEnd);
			}
	}
	static void Menu(int day, ref bool dayEnd)
	{
		Console.WriteLine("Your options are: 1. Check your inventory 2. Go to the market 3. End the day early");
		string answer = Console.ReadLine().ToLower().Trim();
		switch (answer)
		{
			case "1":
			InventoryCheck();
			break;
			case "2":
			MarketVisit(day);
			Console.WriteLine("You call it a day");
			dayEnd = true;
			break;
			case "3":
			dayEnd = true;
			break;
			default:
			break;
		}
		Console.WriteLine("To continue, press enter");
		Console.ReadLine();
	}
	static void InventoryCheck()
	{
		for (int i = 0; i < player.inventory.Length; i++)
		{
			if (player.inventory[i] != null)
				Console.WriteLine(player.inventory[i].itemName + " - " + player.inventory[i].price);
			else
				Console.WriteLine("Empty slot");
		}
		Console.WriteLine("Money: " + player.money);
	}
	static void MarketVisit(int day)
	{
		int rnd;
		Randomizer(day, out rnd);
		if (day % 2 == 0)
		{
		apple.price = Math.Abs(apple.price - rnd);
		orange.price = Math.Abs(orange.price + rnd);
		carrot.price = Math.Abs(carrot.price + rnd);	
		}
		else
		{
		apple.price = Math.Abs(apple.price + rnd);
		orange.price = Math.Abs(orange.price - rnd);
		carrot.price = Math.Abs(carrot.price - rnd);	
		}
		
		Console.WriteLine("Here are the prices for items for today:");
		Console.WriteLine(apple.itemName + " - " + apple.price);
        Console.WriteLine(orange.itemName + " - " + orange.price);
        Console.WriteLine(carrot.itemName + " - " + carrot.price);
		
		Console.WriteLine("You have: " + player.money + " money");
		while (true)
		{
			Console.WriteLine("Do you want to buy or sell anyhting? s - sell, b - buy, c - check inventory, n - no, skip");
			
			Answer();
			Console.WriteLine("Do you want to skip the day or continue? y/n");
			if (Console.ReadLine().ToLower().Trim() == "y")
				break;
			else
				continue;
		}
		
	}
	//because Random() doesn't work properly
	static void Randomizer(int day, out int rnd)
	{
		if (day == 0)
			rnd = 0;
		else if (day % 2 == 0)
			rnd = day * 2 / 3;
		else if (day % 3 == 0)
			rnd = day * 4 / 3;
		else
			rnd = day * 5 / 3;
	}
	public static void ItemInit()
	{
		apple = new Item();
		apple.itemName = "Apple";
		apple.price = 10;
		orange = new Item();
		orange.itemName = "Orange";
		orange.price = 15;
		carrot = new Item();
		carrot.itemName = "Carrot";
		carrot.price = 5;
		Console.WriteLine("initialized");
	}
	static void Answer()
	{
		string answer_ = Console.ReadLine().ToLower().Trim();
		if (answer_ == "b")
		{
			Buy();
		}
		else if (answer_ == "s")
		{
			Sell();
		}
		else if (answer_ == "c")
		{
			InventoryCheck();
		}
		else
			return;
	}
	static void Buy()
	{
		Console.WriteLine("Choose what to buy: apples: a, oranges: o, carrots: c");
		int id = 0; //item id
		while (true)
		{
			string answer = Console.ReadLine().ToLower().Trim();
			if (answer == "a")
			{
				id = 0;
				break;
			}
			else if (answer == "o")
			{
				id = 1;
				break;
			}
			else if (answer == "c")
			{
				id = 2;
				break;
			}
			else
			{
				Console.WriteLine("Try again");
				continue;
			}
		}
		//check avaialble space
		int num = 10;
		for (int i =0; i< player.inventory.Length; i++)
		{
			if (player.inventory[i] != null)
				num--;
		}
		//how many?
		int number = 0;		
		Console.WriteLine("how many? Your inventory has " + (num) + " empty slots left");
		while (true)
		{
			bool success = false;
			success = Int32.TryParse(Console.ReadLine().ToLower().Trim(), out number);
			if (success == true)
				break;
			else
				continue;
		}
		int num_ = 0;
		if (num != 0)
		{
			for (int i = 0; i < player.inventory.Length; i++)
			{
				if (player.inventory[i] == null && number > 0)
				{
					number--;
					if (id == 0)
					{
						if (player.money - apple.price < 0)
						{
							Console.WriteLine("Not enough money");
							break;
						}
						else
						{
							player.inventory[i] = apple;
							player.money -= apple.price;
							num_++;
						}
					}
					else if (id == 1)
					{
						if (player.money - orange.price < 0)
						{
							Console.WriteLine("Not enough money");
							break;
						}
						else
						{
							player.inventory[i] = orange;
							player.money -= orange.price;
							num_++;
						}
					}
					else if (id == 2)
					{
						if (player.money - carrot.price < 0)
						{
							Console.WriteLine("Not enough money");
							break;
						}
						else
						{
							player.inventory[i] = carrot;
							player.money -= carrot.price;
							num_++;
						}
					}
				}
			}
		}
		else
			Console.WriteLine("Your inventory is full!");
		Console.WriteLine("You've bought " + num_ + " items!\n Money left: " + player.money);
	}
	static void Sell()
	{
		Console.WriteLine("Choose what to sell: apples: a, oranges: o, carrots: c");
		int id = 0; //item id
		while (true)
		{
			string answer = Console.ReadLine().ToLower().Trim();
			if (answer == "a")
			{
				id = 0;
				break;
			}
			else if (answer == "o")
			{
				id = 1;
				break;
			}
			else if (answer == "c")
			{
				id = 2;
				break;
			}
			else
			{
				Console.WriteLine("Try again");
				continue;
			}
		}
		int number = 0;
		for (int i = 0; i< player.inventory.Length; i++)
		{
			if (id ==0)
			{
				if (player.inventory[i] == apple)
					number++;
			}
			else if (id ==1)
			{
				if (player.inventory[i] == orange)
					number++;
			}
			else if (id ==2)
			{
				if (player.inventory[i] == carrot)
					number++;
			}
		}
		if (id == 0)
			Console.WriteLine("how many? You have: " + number + " of apples");
		else if (id ==1)
			Console.WriteLine("how many? You have: " + number +" of orranges");
		else if (id ==2)
			Console.WriteLine("how many? You have: "+ number +" of carrots");
		
		while (true)
		{
			bool success = false;
			success = Int32.TryParse(Console.ReadLine().ToLower().Trim(), out number);
			if (success == true)
				break;
			else
			{
				Console.WriteLine("Not a valid number");
				continue;
			}
		}
		int num_ = 0;
		if (number != 0)
		{
			for (int i =0; i < player.inventory.Length; i++)
			{
				if (number > 0)
				{
					if (id == 0 && player.inventory[i] == apple)
					{
						player.inventory[i] = null;
						player.money += apple.price;
						number--;
					}
					else if (id == 1 && player.inventory[i] == orange)
					{
						player.inventory[i] = null;
						player.money += orange.price;
						number--;
					}
					else if (id == 2 && player.inventory[i] == carrot)
					{
						player.inventory[i] = null;
						player.money += carrot.price;
						number--;
					}
				num_++;
				}
			}
		Console.WriteLine("You have sold " + num_ + " items\n Money: " + player.money);
		}
	}
}
