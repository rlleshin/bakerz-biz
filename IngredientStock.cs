using System;
using System.Collections.Generic;
using System.Linq;

namespace baker_biz
{
    public class IngredientStock
    {
        private readonly IDictionary<IngredientType, Amount> stock;
        
        public IngredientStock()
        {
            stock = new Dictionary<IngredientType, Amount>();
        }

        public IngredientStock(Dictionary<IngredientType, Amount> initialStock)
        {
            stock = initialStock;
        }

        public Amount CheckStockAmount(IngredientType type)
        {
            if (stock.TryGetValue(type, out Amount? ingredient))
            {
                return ingredient;
            }
            //default unit per IngredientType type
            return Amount.Default;
        }

        public IReadOnlyList<Ingredient> CheckStock()
        {
            var ingredients = new List<Ingredient>();

            foreach (KeyValuePair<IngredientType, Amount> stockEntry in stock)
            {
                ingredients.Add(new Ingredient(stockEntry.Key, stockEntry.Value));
            }

            return ingredients;
        }

        public void AddIngredient(Ingredient ingredient)
        {
            stock[ingredient.Type] = Amount.AddAmount(stock[ingredient.Type], ingredient.Amount);
        }

        // TODO: Return acutal item
        public int CreateRecipe(IReadOnlyList<Ingredient> recipe)
        {
            int maxRecipeItems = CalculateMaxRecipeItems(recipe);
       
            //if any items were created, remove ingredients
            if (maxRecipeItems > 0)
            {
                //used ingredient list will be recipe amount multiplied by the number of items created 
                List<Ingredient> usedIngredients = recipe.Select(recipeItem => new Ingredient(recipeItem.Type,
                                                                                              new Amount(recipeItem.Amount.Number * maxRecipeItems,
                                                                                              recipeItem.Amount.Unit))).ToList();

                RemoveIngredients(usedIngredients);
            }

            return maxRecipeItems;
        }

        public int CalculateMaxRecipeItems(IReadOnlyList<Ingredient> recipe)
        {
            int maxRecipeItems = int.MaxValue;

            foreach (Ingredient recipeItem in recipe)
            {
                //check for each ingridient list in 
                if (stock.TryGetValue(recipeItem.Type, out Amount? stockIngredient))
                {
                    // TODO: Compare units. Probably need to add methods to ammounts
                    int numRecipeForStock = (int)(stockIngredient.Number / recipeItem.Amount.Number);

                    // not enough of one ingrident to make any recipe items
                    if (numRecipeForStock == 0)
                    {
                        return 0;
                    }

                    maxRecipeItems = Math.Min(maxRecipeItems, numRecipeForStock);
                }
                //missing ingredient from stock, cannot make any recipeItems
                else
                {
                    return 0;
                }
            }

            return maxRecipeItems;
        }

        private void RemoveIngredients(IReadOnlyList<Ingredient> ingredients)
        {
            foreach (Ingredient ingredient in ingredients)
            {
                //set default if not in stock
                Amount currentAmount;

                //ingridient in stock
                if (stock.ContainsKey(ingredient.Type))
                {
                    currentAmount = stock[ingredient.Type];        
                }
                else
                {
                    currentAmount = new Amount(0, ingredient.Amount.Unit);
                }

                stock[ingredient.Type] = Amount.RemoveAmount(currentAmount, ingredient.Amount);
            }
        }
    }
}