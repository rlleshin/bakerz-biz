using baker_biz;
using System;
using System.Collections.Generic;

namespace Interview_Refactor1
{
    class Program
    {
        static void Main()
        {
            do
            {
                Console.WriteLine("How many apples do you have?");
                var apples = Console.ReadLine();

                Console.WriteLine("How much sugar do you have?");
                var sugar = Console.ReadLine();

                Console.WriteLine("How many pounds of flour do you have?");
                var _PoundsOfflour = Console.ReadLine();

                IngredientStock stock;

                try
                {
                    stock = getStock(apples, sugar, _PoundsOfflour);
                }
                catch
                {
                    Console.WriteLine("Invalid input given, please try again.");
                    continue;
                }

                var applePieRecipe = new List<Ingredient>() {
                    new Ingredient(IngredientType.Apple, new Amount(3, Units.Whole)),
                    new Ingredient(IngredientType.Sugar, new Amount(2, Units.Pound)),
                    new Ingredient(IngredientType.Flour, new Amount(1, Units.Pound))
                };

                int piesMade = stock.CreateRecipe(applePieRecipe);

                displayResults(piesMade, stock);

            } while (!string.Equals(Console.ReadLine(), "Q"));
        }

        private static IngredientStock getStock(string? apples, string? sugar, string? flour)
        {
            try
            {
                var numApples = int.Parse(apples);
                var numPoundsSugar = decimal.Parse(sugar);
                var numPoundsFlour = decimal.Parse(flour);

                var initialStock = new Dictionary<IngredientType, Amount>()
                {
                    { IngredientType.Apple, new Amount(numApples, Units.Whole) },
                    { IngredientType.Sugar, new Amount(numPoundsSugar, Units.Pound) },
                    { IngredientType.Flour, new Amount(numPoundsFlour, Units.Pound) }
                };

                return new IngredientStock(initialStock);
            }
            catch
            {
                throw new Exception("Invalid Amount Values Given");
            }
        }

        private static void displayResults(int numRecipeItemsCreated, IngredientStock leftoverStock)
        {
            Console.WriteLine($"You can make: {numRecipeItemsCreated}.");
            Console.WriteLine($"Leftover Ingredients: ");

            foreach (Ingredient ingredient in leftoverStock.CheckStock())
            {
                Console.WriteLine($"{ingredient.Amount.Number} {ingredient.Amount.Unit}(s) of {ingredient.Type}(s)");
            }
        }
    }  
}



