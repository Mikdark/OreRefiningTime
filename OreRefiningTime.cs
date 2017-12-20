void Main()        
{   
	// Ore Refining Time for vanilla server and Rebels mod
	// https://www.rebels-games.com/

	// This ist the pre pre pre Alpha version
	// Just need to fill in the information below to customize this to your liking
	// you need to build the same count of modules on all refineries,
	// this is very important!!! If you don't duit this, script working bed.
	// I was inspired by: Workdawg 
	// https://forum.keenswh.com/threads/enhanced-materials-counting-script.7348207/
	
	int ServerRafinerySpeed = 1; // Server refinery speed (1,2,3,...) , on Rebelsserver ServerRafinerySpeed = 1
	
	int RefineryCount = 1; // Refinery count on ship or base
	
	// Vanillia and Rebels mod
	int Pro = 0; //ProductivityModule 100% - Count of Productvity=speedModule on Refinery
	
	//Rebels mod
	int MK2 = 0; // MK2ProductivityModule 200% Count of Productvity Module on Refinery
	int MK3 = 0; // MK3ProductivityModule 400% Count of Productvity Module on Refinery
	int MK4 = 0; // MK4ProductivityModule 800% Count of Productvity Module on Refinery
	int MK5 = 0; // MK5ProductivityModule 1600% Count of Productvity Module on Refinery
	int Rebel = 0; // Rebel ProductivityModule 3200% Count of Productvity Module on Refinery
	
	int Arc = 0; //Count of Arc Furnace Vanilla and Rebels mod
				//- TODO list of arc
	
	//Count of Arc Furnace Rebels mod
	int MK2Arc = 0; // 200% Productvity
	int MK3Arc = 0; // 400% Productvity
	int MK4Arc = 0; // 800% Productvity
	int MK5Arc = 0; // 1600% Productvity
	int RebelArc = 0; // 32000% Productvity
	
	// If you use Arc Furnace only to refinery cobalt or iron or nickel
	// set false 
	// Blacklist for Refinery
	bool refineCobaltInRefinery = true; // refine Cobalt In Refinery
	bool refineIronInRefinery = true;
	bool refineNickelInRefinery = true;
	// TODO if fond Arc then refineCobaltInRefinery.Iron.Nickel falsee
	
    string LCD_refiningTime = "LCDTime"; // LCD custom name
	
	
 
	// Ore Refining Speed from http://www.spaceengineerswiki.com/Refinery 
	int ORSCobalt = 1170; 
	int ORSGold = 11700; 
	int ORSIron = 93600; 
	int ORSMagnesium = 4680; 
	int ORSNickel = 2340; 
	int ORSSilicon = 7800; 
	int ORSSilver = 4680; 
	int ORSStone = 46800; 
	int ORSPlatinum = 1170; 
	int ORSUranium = 1170;  
	
	// Rebels mod
	int ORSLateryt = 2742;
	int ORSMalachit = 4054;
	int ORSPalladium = 524;
	
	// http://www.spaceengineerswiki.com/Arc_Furnace#Refining_Rates_.28v1.080.29
	int ORSCobaltArc = 1440;
	int ORSIronArc = 115200;
	int ORSNickelArc = 2880;
	
	
	// Ore  
	float OreCobalt = 0; 
	float OreGold = 0; 
	float OreIron = 0; 
	float OreMagnesium = 0; 
	float OreNickel = 0;
	float OreSilicon = 0; 
	float OreSilver = 0; 
	float OreStone = 0; 
	float OrePlatinum = 0; 
	float OreUranium = 0; 
    // Rebels mod
	float OreLateryt = 0;
	float OreMalachit = 0;
	float OrePalladium = 0;
	
	float CobaltTime = 0;
	float GoldTime = 0; 
	float IronTime = 0; 
	float MagnesiumTime = 0; 
	float NickelTime = 0; 
	float SiliconTime = 0; 
	float SilverTime = 0; 
	float StoneTime = 0; 
	float PlatinumTime = 0; 
	float UraniumTime = 0; 
	float AllOreTime = 0;
	// Rebels mod
	float LaterytTime = 0;
	float MalachitTime = 0;
	float PalladiumTime = 0;
	
	float BaseSpeed = 0;
	
    string info = ""; 
 
	
	
	
	IMyTextPanel LCD; 
	LCD = GridTerminalSystem.GetBlockWithName(LCD_refiningTime) as IMyTextPanel; 
	
	//Created by: Workdawg
    //https://forum.keenswh.com/threads/enhanced-materials-counting-script.7348207/
	var allBlocks = new List<IMyTerminalBlock>();
	GridTerminalSystem.GetBlocks(allBlocks);
	List<IMyInventoryOwner> inventoryBlocks = new List<IMyInventoryOwner>(); // all blocks w/ inventories
    List<IMyInventory> inventories = new List<IMyInventory>(); //actual inventories
    // look for all blocks that have inventory
    for (int x = 0; x < allBlocks.Count; x++)
        {
            var InventoryOwner = allBlocks[x] as IMyInventoryOwner;
            if (InventoryOwner != null) //it's an inventory block
				{
                    inventoryBlocks.Add(InventoryOwner);
                }
        }
            // add all inventories to the list
            for (int i = 0; i < inventoryBlocks.Count; i++)
            {
                // all blocks in this group have inventories, so add the first          
                inventories.Add(inventoryBlocks[i].GetInventory(0));
                // if more than one inventory, add the 2nd
                if (inventoryBlocks[i].InventoryCount > 1)
                {
                    IMyInventory inventory = inventoryBlocks[i].GetInventory(1);
                    inventories.Add(inventory);
                }   		
			}  
	 	
	
	for (int j = 0; j < inventories.Count; j++)
	{							// get all items in the inventory item
		List<IMyInventoryItem> items = new List<IMyInventoryItem>(); // get all items in the inventory item	
		items = inventories[j].GetItems();        
			
		for(int i=0; i<items.Count; i++)        
		{    
	
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Cobalt") OreCobalt = OreCobalt + float.Parse(items[i].Amount.ToString()); 
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Gold") OreGold = OreGold + float.Parse(items[i].Amount.ToString());		 
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Iron") OreIron = OreIron + float.Parse(items[i].Amount.ToString());  
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Magnesium") OreMagnesium =OreMagnesium + float.Parse(items[i].Amount.ToString());  	     
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Nickel") OreNickel = OreNickel + float.Parse(items[i].Amount.ToString());      
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Silicon") OreSilicon = OreSilicon + float.Parse(items[i].Amount.ToString());  
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Silver") OreSilver = OreSilver + float.Parse(items[i].Amount.ToString());     
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Stone") OreStone = OreStone + float.Parse(items[i].Amount.ToString());      
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Platinum") OrePlatinum = OrePlatinum + float.Parse(items[i].Amount.ToString());       
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Uranium") OreUranium = OreUranium + float.Parse(items[i].Amount.ToString());
			//Rebels mod
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Lateryt") OreLateryt = OreLateryt + float.Parse(items[i].Amount.ToString());      
			if (items[i].Content.ToString().Contains("Oree") && items[i].Content.SubtypeName == "Malachit") OreMalachit = OreMalachit + float.Parse(items[i].Amount.ToString());
			if (items[i].Content.ToString().Contains("Ore") && items[i].Content.SubtypeName == "Pallad") OrePalladium = OrePalladium + float.Parse(items[i].Amount.ToString());
		}    	  
    } 
	// BaseSpeed = ServerRafinerySpeed * OreRefiningSpeed
	// OreRefiningTime = OreCount / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount)
	//  
	// BaseSpeedArc = ServerRafinerySpeed * OreRefiningSpeedArc - (i need testig if ServerRefinerySpeed multiply Arc speed to, i think yes)
	// OreRefiningTime = OreCount / ((Arc*BaseSpeedArc)+(MK2Arc*2*BaseSpeedArc)+(MK3Arc*4*BaseSpeedArc)+(MK4Arc*8*BaseSpeedArc)+(MK5Arc*16*BaseSpeedArc)+(RebelArc*32*BaseSpeedArc)) 
	

	if (refineCobaltInRefinery == true) 
	{
		BaseSpeed = ServerRafinerySpeed * ORSCobalt;
		CobaltTime = OreCobalt / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	}
	else // use algorithm for Arc Furnace refining time
	{
		BaseSpeed = ServerRafinerySpeed * ORSCobaltArc;
		CobaltTime = OreCobalt / ((Arc*BaseSpeed)+(MK2Arc*2*BaseSpeed)+(MK3Arc*4*BaseSpeed)+(MK4Arc*8*BaseSpeed)+(MK5Arc*16*BaseSpeed)+(RebelArc*32*BaseSpeed));
		Echo("Arc refining");
	}
	BaseSpeed = ServerRafinerySpeed * ORSGold;
	GoldTime = OreGold / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	if (refineIronInRefinery == true) 
	{
		BaseSpeed = ServerRafinerySpeed * ORSIron;
		IronTime = OreIron / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);	
	}
	else // use algorithm for Arc Furnace refining time
	{
		BaseSpeed = ServerRafinerySpeed * ORSIronArc;
		IronTime = OreIron / ((Arc*BaseSpeed)+(MK2Arc*2*BaseSpeed)+(MK3Arc*4*BaseSpeed)+(MK4Arc*8*BaseSpeed)+(MK5Arc*16*BaseSpeed)+(RebelArc*32*BaseSpeed));
	}
	BaseSpeed = ServerRafinerySpeed * ORSMagnesium;
	MagnesiumTime = OreMagnesium / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	if (refineNickelInRefinery == true) 
	{
		BaseSpeed = ServerRafinerySpeed * ORSNickel;
		NickelTime = OreNickel / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);	
	}
	else // use algorithm for Arc Furnace refining time
	{
		BaseSpeed = ServerRafinerySpeed * ORSNickelArc;
		NickelTime = OreNickel / ((Arc*BaseSpeed)+(MK2Arc*2*BaseSpeed)+(MK3Arc*4*BaseSpeed)+(MK4Arc*8*BaseSpeed)+(MK5Arc*16*BaseSpeed)+(RebelArc*32*BaseSpeed));
	}	
	BaseSpeed = ServerRafinerySpeed * ORSSilicon;
	SiliconTime = OreSilicon / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	BaseSpeed = ServerRafinerySpeed * ORSSilver;
	SilverTime = OreSilver / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	BaseSpeed = ServerRafinerySpeed * ORSStone;
	StoneTime = OreStone / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	BaseSpeed = ServerRafinerySpeed * ORSPlatinum;
	PlatinumTime = OrePlatinum / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	BaseSpeed = ServerRafinerySpeed * ORSUranium;
	UraniumTime = OreUranium / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	// Rebels mod
	BaseSpeed = ServerRafinerySpeed * ORSLateryt;
	LaterytTime = OreLateryt / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	BaseSpeed = ServerRafinerySpeed * ORSMalachit;
	MalachitTime = OreMalachit / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	BaseSpeed = ServerRafinerySpeed * ORSPalladium;
	PalladiumTime = OrePalladium / (((BaseSpeed)+(Pro*BaseSpeed)+(MK2*2*BaseSpeed)+(MK3*4*BaseSpeed)+(MK4*8*BaseSpeed)+(MK5*16*BaseSpeed)+(Rebel*32*BaseSpeed))*RefineryCount);
	
	
	AllOreTime = CobaltTime + GoldTime + IronTime + MagnesiumTime +NickelTime + SiliconTime + SilverTime + StoneTime + PlatinumTime + UraniumTime + LaterytTime + MalachitTime + PalladiumTime;
	
	
	
	
	info = "   Ore             amount          time    \n";
	if (OreCobalt > 0)		info = (info +            "Cobalt:           " + OreCobalt + "  " + GetTimeString(CobaltTime) + "\n"); 
	if (OreGold > 0)		info = (info +               "Gold:              " + OreGold + "  " + GetTimeString(GoldTime) + "\n"); 
	if (OreIron > 0)		info = (info +                "Iron:               " + OreIron + "  "+ GetTimeString(IronTime) + "\n"); 
	if (OreLateryt > 0) 	info = (info +          "Lateryt:          " + OreLateryt + "  "+ GetTimeString(LaterytTime) + "\n"); 
	if (OreMagnesium > 0) 	info = (info +  "Magnesium:  " + OreMagnesium + "  " + GetTimeString(MagnesiumTime) + "\n"); 
	if (OreMalachit > 0) 	info = (info +       "Malachit:       " + OreMalachit + "  "+ GetTimeString(MalachitTime) + "\n"); 
	if (OreNickel >0) 		info = (info +            "Nickel:          " + OreNickel + "  " + GetTimeString(NickelTime) +  "\n"); 
	if (OreSilicon >0)		info = (info +            "Silicon:         " + OreSilicon + "  " + GetTimeString(SiliconTime) + "\n"); 
	if (OreSilver >0)		info = (info +              "Silver:           " + OreSilver + "  " + GetTimeString(SilverTime) + "\n"); 
	if (OreStone >0)		info = (info +              "Stone:           " + OreStone + "  " + GetTimeString(StoneTime) + "\n"); 
	if (OrePalladium >0)	info = (info +       "Palladium:    " + OrePalladium + "  " + GetTimeString(PalladiumTime) + "\n"); 
	if (OrePlatinum >0)		info = (info +         "Platinum:      " + OrePlatinum + "  " + GetTimeString(PlatinumTime) + "\n"); 
	if (OreUranium >0)		info = (info +          "Uranium:       " + OreUranium + "  " + GetTimeString(UraniumTime) + "\n"); 
	info = (info + "\n\r");
	info = (info + "Time together : " + GetTimeString(AllOreTime));
	Echo(info); 
	
	LCD.WritePublicText(info); 
    LCD.ShowPublicTextOnScreen(); 
}

// This function Convert hours to DD:HH:MM
   
	public static string GetTimeString(float Hours)
	{
	DateTime dTime = new DateTime().AddHours(Hours);
    return dTime.ToString("dd")+"d " + dTime.ToString("HH")+"h "+ dTime.ToString("mm")+"m" ;    
	}
