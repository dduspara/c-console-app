using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seminarski_rad
{
    internal class Program
    {
        static void Main(string[] args)
        {
                while (true)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("+----------EVIDENCIJA VOZNOG PARKA-----------+");
                    Console.WriteLine("1. Dodavanje novog vozila");
                    Console.WriteLine("2. Ažuriranje postojećeg vozila");
                    Console.WriteLine("3. Ispis svih vozila");
                    Console.WriteLine("4. Ispis vozila sa važećom registracijom");
                    Console.WriteLine("5. Ispis vozila sa isteklom registracijom");
                    Console.WriteLine("6. Prekid rada programa");
                    Console.WriteLine("+--------------------------------------------+");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("Izaberite opciju: ");
                    Console.ResetColor();

                    try
                    {
                        int opcija = int.Parse(Console.ReadLine());

                        switch (opcija)
                        {
                            case 1:
                                DodavanjeVozila();
                                break;
                            case 2:
                                AzuriranjeVozila();
                                break;
                            case 3:
                                IspisSvihVozila();
                                break;
                            case 4:
                                IspisVozilaSaVazecomRegistracijom();
                                break;
                            case 5:
                                IspisVozilaSaIsteklomRegistracijom();
                                break;
                            case 6:
                                return;
                            default:
                                Console.WriteLine("Nepostojeća opcija.");
                                break;
                        }
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Nepostojeća opcija.");
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("Nepostojeća opcija.");
                    }

                }
            
        }
        static List<Vozilo> vozila = new List<Vozilo>();
        class Vozilo
        {
            public string marka { get; set; }
            public string model { get; set; }
            public string registracija { get; set; }
            public DateTime datum_izdavanja { get; set; }
            public DateTime datum_isteka { get; set; }




            public Vozilo(string marka, string model)
            {
                this.marka = marka;
                this.model = model;
            }

            public Vozilo(string marka, string model, string registracija, DateTime datum_izdavanja, DateTime datum_isteka) : this(marka, model)
            {
                this.registracija = registracija;
                this.datum_izdavanja = datum_izdavanja;
                this.datum_isteka = datum_isteka;
            }
        }

        static void DodavanjeVozila()
        {
            Console.WriteLine("Dodavanje novog vozila.");
            Console.Write("Unesite marku vozila: ");
            string marka = Console.ReadLine();
            Console.Write("Unesite model vozila: ");
            string model = Console.ReadLine();
            Console.Write("Unesite registarsku oznaku vozila: ");
            string registracija = Console.ReadLine();
            Console.Write("Unesite datum izdavanja registarske oznake: ");
            DateTime datum_izdavanja = DateTime.Parse(Console.ReadLine());
            DateTime datum_isteka = datum_izdavanja.AddYears(1);

            Vozilo novoVozilo = new Vozilo(marka, model, registracija, datum_izdavanja, datum_isteka);
            vozila.Add(novoVozilo);
            Console.WriteLine("Vozilo uspješno dodano.");
        }
        static void AzuriranjeVozila()
        {
            Console.WriteLine("Unesite registraciju vozila kojeg želite ažurirati:");
            string registracija = Console.ReadLine();
            Vozilo vozilo = vozila.Find(x => x.registracija == registracija);
            if (vozilo == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Vozilo sa unesenom registracijom ne postoji.");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.WriteLine("Ažuriranje postojećeg vozila.");
                Console.Write("Unesite novu marku vozila: ");
                vozilo.marka = Console.ReadLine();
                Console.Write("Unesite novi model vozila: ");
                vozilo.model = Console.ReadLine();
                Console.Write("Unesite novu registarsku oznaku vozila: ");
                vozilo.registracija = Console.ReadLine();
                Console.Write("Unesite novi datum izdavanja registarske oznake: ");
                vozilo.datum_izdavanja = DateTime.Parse(Console.ReadLine());
                vozilo.datum_isteka = vozilo.datum_izdavanja.AddYears(1);
                Console.WriteLine("Vozilo uspješno ažurirano.");
            }

        }
        static void IspisSvihVozila()
        {
            if (vozila.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nema unesenih vozila.");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Ispis svih vozila:");
                Console.ResetColor();

                foreach (Vozilo vozilo in vozila)
                {
                    Console.WriteLine("\n Marka: {0}\n Model: {1}\n Registarska oznaka: {2}\n Datum izdavanja: {3}\n Datum isteka: {4}",
                       vozilo.marka, vozilo.model, vozilo.registracija, vozilo.datum_izdavanja.ToShortDateString(), vozilo.datum_isteka.ToShortDateString());
                }
            }
        }
        static void IspisVozilaSaVazecomRegistracijom()
        {
            List<Vozilo> vozilaSaVazecomRegom = vozila.FindAll(x => x.datum_isteka > DateTime.Now);
            if (vozilaSaVazecomRegom.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nema vozila sa važećom registracijom.");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Popis vozila sa važećom registracijom:");
                Console.ResetColor();

                foreach (Vozilo vozilo in vozilaSaVazecomRegom)
                {
                    Console.WriteLine("\n Marka: {0}\n Model: {1}\n Registarska oznaka: {2}\n Datum izdavanja: {3}\n Datum isteka: {4}",
                        vozilo.marka, vozilo.model, vozilo.registracija, vozilo.datum_izdavanja.ToShortDateString(), vozilo.datum_isteka.ToShortDateString());
                }
            }
        }
        static void IspisVozilaSaIsteklomRegistracijom()
        {
            List<Vozilo> vozilaSaIsteklomRegom = vozila.FindAll(x => x.datum_isteka < DateTime.Now);
            if (vozilaSaIsteklomRegom.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Nema vozila sa isteklom registracijom.");
                Console.ResetColor();
                return;
            }
            else
            {
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Popis vozila sa isteklom registracijom.");
                Console.ResetColor();

                foreach (Vozilo vozilo in vozilaSaIsteklomRegom)
                {
                    Console.WriteLine("\n Marka: {0}\n Model: {1}\n Registarska oznaka: {2}\n Datum izdavanja: {3}\n Datum isteka: {4}",
                        vozilo.marka, vozilo.model, vozilo.registracija, vozilo.datum_izdavanja.ToShortDateString(), vozilo.datum_isteka.ToShortDateString());
                }
            }
        }
    }
}
