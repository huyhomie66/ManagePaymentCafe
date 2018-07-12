if (tbl.checkById(o.OrderTable.Table_Id) != null)
			{
				while (true)
				{
					Console.WriteLine(" Input Item Id: ");
					o.OrderItem.ItemId = Convert.ToInt32(Console.ReadLine());
					if (itbl.GetItemById(o.OrderItem.ItemId) != null)
					{
						o.OrderItem.ItemId = Convert.ToInt32(Console.ReadLine());
						break;
					}
					else
					{
						while (true)
						{
							Console.WriteLine("wrong item id,pls re-enter: ");
							o.OrderItem.ItemId = Convert.ToInt32(Console.ReadLine());
							if (itbl.GetItemById(o.OrderItem.ItemId) != null)
							{
								o.OrderItem.ItemId = Convert.ToInt32(Console.ReadLine());
								break;
							}
						}
					}
					Console.WriteLine("Input quantity item: ");
					int quantity = Convert.ToInt32(Console.ReadLine());
					if (itbl.CheckAmount(o.OrderItem.ItemId, quantity) != null)
					{
						quantity = Convert.ToInt32(Console.ReadLine());
						break;
					}
					else
					{
						Console.WriteLine("exceeds the number of items,pls re-enter:");
						quantity = Convert.ToInt32(Console.ReadLine());
						if (itbl.CheckAmount(o.OrderItem.ItemId, quantity) != null)
						{
							quantity = Convert.ToInt32(Console.ReadLine());
							break;
						}
					}
					Console.WriteLine("Do you want to continue input item ?");
					char c = Convert.ToChar(Console.ReadLine());
					switch (c)
					{
						case 'n':
							break;
						case 'N':
							break;
						case 'y':
							continue;
						case 'Y':
							continue;
					}
				}
				Console.WriteLine("Input date order: ");
				DateTime date = Convert.ToDateTime(Console.ReadLine());
				Console.WriteLine("Do you want to create new order???");
				char choice = Convert.ToChar(Console.ReadLine());
				switch (choice)
				{
					case 'n':
						break;
					case 'N':
						break;
					case 'y':

						// ob.CreateOrder(o);
						Console.WriteLine("Update complete, press any key to back the menu: ");
						Console.ReadKey();
						break;
				}
			}
			else
			{
				Console.WriteLine("wrong table id");
				Console.WriteLine("press any key to back the menu");
				Console.ReadKey();
			}