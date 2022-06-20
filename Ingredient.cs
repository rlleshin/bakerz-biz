namespace baker_biz
{
    public class Ingredient
    {
        public readonly IngredientType Type;
        //do we also want this readonly?
        public readonly Amount Amount;

        public Ingredient(IngredientType Type, Amount Amount)
        {
            this.Type = Type;
            this.Amount = Amount;
        }
    }

    public enum IngredientType
    {
        Apple,
        Sugar,
        Flour,
    }

    // TODO: Should be on class with name, abbreviate, etc. Look for libraary
    public enum Units
    {
        Pound,
        Whole,
    }
}
