﻿using System;
using Design_Patterns_project.Attributes;
using System.Collections.Generic;
using Design_Patterns_project;

namespace SingleTableInheritanceTest
{
    class SingleTableInheritanceTest
    {
        public static void Main(string[] args)
        {
            // remote -> "den1.mssql7.gear.host", "DPTest", "dptest", "Me3JyhRLOg-_"

            // local 
            // maciopelo -> "DESKTOP-HVUO0CP", "TestDB"
            // szymon -> "LAPTOP-BHF7G1P9", "SingleTableInheritanceTest"

            DataManager mythicalManager = new DataManager("LAPTOP-BHF7G1P9", "SingleTableInheritanceTest");
            //DataManager mythicalManager = new DataManager("DESKTOP-HVUO0CP", "SingleTableInheritanceTest");

            IceDragon iceDragon1 = new IceDragon(1, 220, "Winter", 15, 20, 35, 50);
            IceDragon iceDragon2 = new IceDragon(8, 210, "Whiter", 19, 22, 37, 51);
            IceDragon iceDragon3 = new IceDragon(9, 200, "Freezer", 14, 23, 31, 46);
            GoldDragon goldDragon1 = new GoldDragon(2, 220, "Smaug", 10, 20, 40, 40);
            GoldDragon goldDragon2 = new GoldDragon(10, 250, "Borch", 19, 25, 45, 55);
            Dragon dragon1 = new Dragon(3, 160, "Cracow Dragon", 12, 8);
            Dragon dragon2 = new Dragon(7, 150, "English Dragon", 5, 10);
            MythicalCreature mythicalCreature1 = new MythicalCreature(4, 50, "Dwarf");
            MythicalCreature mythicalCreature2 = new MythicalCreature(5, 15, "Goblin");
            MythicalCreature mythicalCreature3 = new MythicalCreature(6, 40, "Orc");
            List<Object> creatures = new List<Object>() { iceDragon1, goldDragon1 };

            //create and inherit
            mythicalManager.Inherit(creatures, 0);

            //insert
            mythicalManager.Insert(dragon1);
            mythicalManager.Insert(dragon2);
            mythicalManager.Insert(iceDragon1);
            mythicalManager.Insert(iceDragon2);
            mythicalManager.Insert(iceDragon3);
            mythicalManager.Insert(goldDragon1);
            mythicalManager.Insert(goldDragon2);
            mythicalManager.Insert(mythicalCreature1);
            mythicalManager.Insert(mythicalCreature2);
            mythicalManager.Insert(mythicalCreature3);

            //select
            List<SqlCondition> selectConditions = new List<SqlCondition> { SqlCondition.LowerThan("id", 11) };
            string select = mythicalManager.Select(typeof(MythicalCreature), selectConditions);
            Console.WriteLine('\n' + select + '\n');

            //delete
            List<SqlCondition> deleteConditions = new List<SqlCondition> { SqlCondition.LowerThan("blast_power", 11) };
            mythicalManager.Delete("MythicalCreature", deleteConditions);
            mythicalManager.Delete(dragon1);

            //select
            select = mythicalManager.Select(typeof(MythicalCreature), selectConditions);
            Console.WriteLine('\n' + select + '\n');

            //update
            List<Tuple<string, Object>> valuesToSet = new List<Tuple<string, Object>> { new Tuple<string, Object>("endurance", 10) };
            List<SqlCondition> updateConditions = new List<SqlCondition> { SqlCondition.LowerThan("health", 100) };
            mythicalManager.Update(typeof(MythicalCreature), valuesToSet, updateConditions);

            //select
            select = mythicalManager.Select(typeof(MythicalCreature), selectConditions);
            Console.WriteLine('\n' + select + '\n');

            mythicalCreature1 = (MythicalCreature)mythicalManager.SelectById(mythicalCreature1, 1);
            Console.WriteLine(mythicalCreature1.id);
            Console.WriteLine(mythicalCreature1.name);
            Console.WriteLine(mythicalCreature1.health);

            Console.WriteLine("Utter success");
        }
    }

    class MythicalCreature
    {
        [PKey()]
        [Column()]
        public int id { get; set; }

        [Column()]
        public int health { get; set; }

        [Column("mythical_name")]
        public string name { get; set; }

        public MythicalCreature(int id, int health, string name)
        {
            this.id = id;
            this.health = health;
            this.name = name;
        }
    }

    class Dragon : MythicalCreature
    {
        [Column("blast_power")]
        int blastPower { get; set; }

        [Column()]
        int endurance { get; set; }

        public Dragon(int id, int health, string name, int blastPower, int endurance) : base(id, health, name)
        {
            this.blastPower = blastPower;
            this.endurance = endurance;
        }
    }

    class GoldDragon : Dragon
    {
        [Column("mineral_hunger")]
        int mineralHunger { get; set; }

        [Column()]
        double preciousness { get; set; }

        public GoldDragon(int id, int health, string name, int fireCapacity, int endurance, int mineralHunger, double preciousness)
            : base(id, health, name, fireCapacity, endurance)
        {
            this.mineralHunger = mineralHunger;
            this.preciousness = preciousness;
        }
    }

    class IceDragon : Dragon
    {
        [Column("time_freeze")]
        int timeFreeze { get; set; }

        [Column("ice_fire")]
        double iceFire { get; set; }

        public IceDragon(int id, int health, string name, int fireCapacity, int endurance, int timeFreeze, double iceFire)
            : base(id, health, name, fireCapacity, endurance)
        {
            this.timeFreeze = timeFreeze;
            this.iceFire = iceFire;
        }
    }
}
