using System;
using System.Collections.Generic;

class Program
{
    // Клас для Кафедри
    class Kafedra
    {
        public int ID { get; set; }
        public string Name { get; set; }
    }

    // Клас для Викладачів
    class Vikladach
    {
        public int ID { get; set; }
        public string Prizvysche { get; set; }
        public string Imya { get; set; }
        public int KafedraID { get; set; }
        public string Posada { get; set; }
    }

    // Клас для Екзаменів
    class Ekzamen
    {
        public int ID { get; set; }
        public string Predmet { get; set; }
        public int VikladachID { get; set; }
    }

    // Клас для Сесій
    class Sesia
    {
        public int EkzamenID { get; set; }
        public int StudentID { get; set; }
        public int Otsinka { get; set; }
    }

    static void Main()
    {
        // Дані про кафедри
        List<Kafedra> kafedry = new List<Kafedra>
        {
            new Kafedra { ID = 1, Name = "Кафедра математики" },
            new Kafedra { ID = 2, Name = "Кафедра інформатики" }
        };

        // Дані про викладачів
        List<Vikladach> vikladachi = new List<Vikladach>
        {
            new Vikladach { ID = 1, Prizvysche = "Іваненко", Imya = "Іван", KafedraID = 1, Posada = "Доцент" },
            new Vikladach { ID = 2, Prizvysche = "Петренко", Imya = "Петро", KafedraID = 2, Posada = "Професор" }
        };

        // Дані про екзамени
        List<Ekzamen> ekzameny = new List<Ekzamen>
        {
            new Ekzamen { ID = 1, Predmet = "Математика", VikladachID = 1 },
            new Ekzamen { ID = 2, Predmet = "Програмування", VikladachID = 2 }
        };

        // Дані про сесію (оцінки студентів)
        List<Sesia> sesiia = new List<Sesia>
        {
            new Sesia { EkzamenID = 1, StudentID = 101, Otsinka = 2 },
            new Sesia { EkzamenID = 1, StudentID = 102, Otsinka = 5 },
            new Sesia { EkzamenID = 2, StudentID = 103, Otsinka = 2 },
            new Sesia { EkzamenID = 2, StudentID = 104, Otsinka = 2 }
        };

        // Підрахунок кількості талонів з кожного предмету
        Console.WriteLine("Кількість талонів з кожного предмету:");
        Dictionary<string, int> talonyZaPredmet = new Dictionary<string, int>();

        foreach (var ekz in ekzameny)
        {
            int talonyCount = 0;
            foreach (var ses in sesiia)
            {
                if (ses.EkzamenID == ekz.ID && ses.Otsinka == 2)
                {
                    talonyCount++;
                }
            }

            if (!talonyZaPredmet.ContainsKey(ekz.Predmet))
            {
                talonyZaPredmet[ekz.Predmet] = talonyCount;
            }
        }

        foreach (var item in talonyZaPredmet)
        {
            Console.WriteLine($"Предмет: {item.Key}, Талонів: {item.Value}");
        }

        // Підрахунок кількості талонів на кожній кафедрі
        Console.WriteLine("\nКафедра з максимальною кількістю талонів:");
        Dictionary<int, int> talonyZaKafedru = new Dictionary<int, int>();

        foreach (var vikl in vikladachi)
        {
            int talonyCount = 0;

            foreach (var ekz in ekzameny)
            {
                if (ekz.VikladachID == vikl.ID)
                {
                    foreach (var ses in sesiia)
                    {
                        if (ses.EkzamenID == ekz.ID && ses.Otsinka == 2)
                        {
                            talonyCount++;
                        }
                    }
                }
            }

            if (!talonyZaKafedru.ContainsKey(vikl.KafedraID))
            {
                talonyZaKafedru[vikl.KafedraID] = talonyCount;
            }
            else
            {
                talonyZaKafedru[vikl.KafedraID] += talonyCount;
            }
        }

        int maxTalony = 0;
        int maxKafedraID = -1;

        foreach (var item in talonyZaKafedru)
        {
            if (item.Value > maxTalony)
            {
                maxTalony = item.Value;
                maxKafedraID = item.Key;
            }
        }

        if (maxKafedraID != -1)
        {
            string kafedraName = kafedry.Find(k => k.ID == maxKafedraID).Name;
            Console.WriteLine($"Кафедра: {kafedraName}, Талонів: {maxTalony}");
        }
        else
        {
            Console.WriteLine("Талонів не знайдено.");
        }
    }
}
