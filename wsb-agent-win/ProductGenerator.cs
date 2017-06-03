using System;
using System.IO;

namespace wsb_agent_win
{
    public static class ProductGenerator
    {
        public static string[] candyBars = File.ReadAllLines("..\\..\\candybars.txt");
        public static string[] snacks = File.ReadAllLines("..\\..\\snacks.txt");
        public static string[] softdrinks = File.ReadAllLines("..\\..\\softdrinks.txt");
        public static string[] streets = File.ReadAllLines("..\\..\\ulice.txt");
        public static string[] barTastes = { "Czekoladowy", "Kokosowy", "Waniliowy", "Orzechowy" };
        public static string[] snackTastes = { "Solone", "Paprykowe", "Cebulowe", "Salsa" };
        public static string[] drinkTastes = { "Cytrynowa", "Pomarańczowa", "Cebulowa", "Jabłkowa", "Porzeczkowa" };

        //public static Product GetSnickers()
        //{
        //    var snickers = new CandyBar();
        //    snickers.Name = "Snickers";
        //    snickers.Price = 2.50f;
        //    snickers.Taste = "Klasyczny";
        //    snickers.Code = "SnickersRegular";
            
        //    return snickers;
        //}

        //public static Product GetCisowiankaStill()
        //{
        //    var cisowianka = new Drink()
        //    {
        //        Name = "Cisowianka",
        //        DrinkType = DrinkType.StillWater,
        //        Price = 2.0f,
        //        Code = "CisowiankaStill"
        //    };
        //    return cisowianka;
        //}

        //public static Product GetLajkonikSalted()
        //{
        //    var paluszki = new Snack()
        //    {
        //        Name = "Paluszki",
        //        SnackType = SnackType.Breadsticks,
        //        Price = 5.0f,
        //        Taste = "Solone",
        //        Code = "LajkonikSolone"
        //    };
        //    return paluszki;
        //}

        private static string GenerateCode()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[12];
            var random = new Random(Guid.NewGuid().GetHashCode());

            for ( int i = 0; i < stringChars.Length; i++ )
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new string(stringChars);
        }

        public static VendingMachine Generate()
        {
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            int slotCount = rnd.Next(15, 30);
            while ( slotCount % 3 != 0 )
                slotCount = rnd.Next(15, 30);

            VendingMachine tmp = new VendingMachine(slotCount);
            tmp.Code = GenerateCode();
            tmp.Adress = streets[rnd.Next(0, streets.Length)] + " " + rnd.Next(1, 100);
            tmp.Type = (VendingMachineType)( rnd.Next(0, 3) );
            tmp.SlotDepthness = rnd.Next(10, 20);

            for ( int i = 0; i < slotCount / 3; i++ )
            {
                CandyBar candy = new CandyBar();
                candy.Name = candyBars[rnd.Next(0, candyBars.Length)];
                candy.Price = (float)( ( rnd.NextDouble() + 0.5 ) * rnd.Next(2, 6) );
                candy.Price = (float)Math.Round(candy.Price, 2);
                candy.Taste = barTastes[rnd.Next(0, barTastes.Length)];
                candy.Code = GenerateCode();
                candy.Count = tmp.SlotDepthness;

                tmp.Slots.Add(candy);

                
            }

            for ( int i = slotCount / 3; i < ( slotCount / 3 ) * 2; i++ )
            {
                Snack snack = new Snack();
                snack.Name = snacks[rnd.Next(0, snacks.Length)];
                snack.Price = (float)( ( rnd.NextDouble() + 0.5 ) * rnd.Next(2, 6) );
                snack.Price = (float)Math.Round(snack.Price, 2);
                snack.Taste = snackTastes[rnd.Next(0, snackTastes.Length)];
                snack.SnackType = (SnackType)rnd.Next(0, 3);
                snack.Code = GenerateCode();
                snack.Count = tmp.SlotDepthness;

                tmp.Slots.Add(snack);
            }

            for ( int i = ( slotCount / 3 ) * 2; i < slotCount; i++ )
            {
                Drink drink = new Drink();
                drink.Name = softdrinks[rnd.Next(0, softdrinks.Length)];
                drink.Price = (float)( ( rnd.NextDouble() + 0.5 ) * rnd.Next(2, 6) );
                drink.Price = (float)Math.Round(drink.Price, 2);
                drink.DrinkType = (DrinkType)rnd.Next(0, 4);
                drink.DrinkTastes = drinkTastes[rnd.Next(0, drinkTastes.Length)];
                drink.Code = GenerateCode();
                drink.Count = tmp.SlotDepthness;

                tmp.Slots.Add(drink);
            }

            return tmp;
        }
    }
}