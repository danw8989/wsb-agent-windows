using System;
using System.Collections.Generic;
using System.IO;

namespace wsb
{
    [System.Serializable]
    public class VendingMachine
    {
        // unikatowy kod maszyny
        public string Code { get; set; }

        // adres miejsca gidze jest postawiona
        public string Adress { get; set; }

        public VendingMachineType Type { get; set; }

        // ilosc miejsc na produkty, np jak ma 10 to moze byc 5 batonikow i 5 roznych napoi
        public int NumberOfSlots { get; set; }
        public List<int> SlotsToRefill = new List<int>();

        // ile miesci sie produktow w jednym slocie
        public int SlotDepthness { get; set; }
        public bool IsWorking { get; set; } = true;

        public List<Product> Slots { get; set; }
        public List<Transaction> transactionList;

        public VendingMachine()
        {
            this.SlotDepthness = 20;
            this.NumberOfSlots = 60;

            InitializeSlots();
        }

        public VendingMachine(int numberOfSlots)
        {
            this.SlotDepthness = 20;
            this.NumberOfSlots = numberOfSlots;

            InitializeSlots();
        }

        private void InitializeSlots()
        {
            this.Slots = new List<Product>();
            transactionList = new List<Transaction>();
            System.Threading.Thread.Sleep(500);
        }

        public void FillProduct(int productCode)
        {
            Slots[productCode].Count = SlotDepthness;
        }

        public void RefillMissingProducts()
        {
            foreach ( int slot in SlotsToRefill )
            {
                FillProduct(slot);
                Console.WriteLine("Uzupełniono slot nr: " + slot);
            }
            
            SlotsToRefill.Clear();
        }


        public void BuyProduct(int productCode)
        {
            if ( IsWorking )
            {
                if ( Slots[productCode].Count != 0 )
                {
                    Console.WriteLine("Zakupiono " + Slots[productCode].Name + " za " + Slots[productCode].Price + " zł" + " Zostało " + (Slots[productCode].Count-1) + "/" + SlotDepthness + " Automat na ulicy: " + Adress);

                    Slots[productCode].Count--;
                    transactionList.Add(new Transaction(productCode, DateTime.Now));
                }

                CheckSlot(productCode);
            }
            // kod błedu: maszyna nie działa
            else SendErrorCode(-1);
        }


        void CheckSlot(int productCode)
        {
            if ( Slots[productCode].Count <= SlotDepthness / 10 && Slots[productCode].Count > 0 )
            {
                // kod błedu: mniej niz 10% produktu
                SendErrorCode(5);
                if ( !SlotsToRefill.Contains(productCode) )
                    SlotsToRefill.Add(productCode);
            }
            else if ( Slots[productCode].Count == 0 )
                // kod błedu: produtku nie ma
                SendErrorCode(10);

            if (SlotsToRefill.Count >= NumberOfSlots/3)
            {
                // wyslij do serwera prośbe o uzupełnienie
                RefillMissingProducts();
            }
            IsWorking = RandomBreakChance();   
        }

        bool RandomBreakChance()
        {
            Random rnd = new Random();
            if ( rnd.Next(1, 1000) == 2 )
                return false;
            return true;
        }

        public void SendErrorCode(int errCode)
        {
            //TODO: Wysłanie do serwera ostrzeżenia
            //Oproznienie cache transakcji, przy okazji
            Console.WriteLine("Wysłano Kod Błędu => " + errCode + " " + Adress + " " + Code);
            EmptyTransactionChache();
        }

        public void EmptyTransactionChache()
        {
            //wysylanie listy transakcji do serwera oraz usuwanie cache
            transactionList.Clear();
        }
        

        public void SendStatus()
        {
        }
    }
}