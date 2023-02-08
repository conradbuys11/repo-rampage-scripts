using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyNameBasket {

    static string[] firstNames = new string[] { "Meat ", "Barry ", "Tammy ", "Kesha ", "Julius ", "Richie ", "Butch ", "Gregg ", "Trahern ", "Conrad ", "Collin ", "Tim ", "Bryan ", "Eduardo ", "Freya ", "Dicky ", "John ", "Morgan ", "Chad ", "Sam ", "Jimmy ", "Timmy ", "Albert ", "Zac ", "George ", "Paul ", "Rohan ", "Gerald ", "Will ", "Dexter ", "Frankie ", "Sylas ", "Toby ", "Leonardo ", "Diego ", "Emmanuel " };
    static string[] lastNames = new string[] { "Anubis", "Damon", "Buys", "Moews", "McQuade", "Tyler", "Stiles", "Garcia", "Barker", "Matthews", "Smith", "Doe", "Brown", "Cobb", "McGee", "Wong", "Jackson", "Cunningham", "Morgan", "Kaiba", "Banks", "Lloyd", "Chambers", "Thomas ", "McCartney", "Harrison", "Lennon", "Cunningham", "Dixon", "Turner", "Fletcher", "Stone", "Ford" };

    public static string GenerateName()
    {
        string myFirstName = firstNames[Random.Range(0, firstNames.Length)];
        string myLastName = lastNames[Random.Range(0, lastNames.Length)];

        return myFirstName + myLastName;
    }
}
