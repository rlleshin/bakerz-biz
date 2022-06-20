using System;

namespace baker_biz
{
    public class Amount
    {
        public readonly decimal Number;
        // TODO: Use unit lib?
        public readonly Units Unit;

        public Amount(decimal Number, Units Unit)
        {
            if (Number < 0)
            {
                throw new ArgumentException($"Cannot have a negative amount: {Number}");
            }

            this.Number = Number;
            this.Unit = Unit;
        }

        public static Amount RemoveAmount(Amount originalAmount, Amount amountToSubtract)
        {
            //don't need to remove nothing
            if (amountToSubtract.Number == 0)
            {
                return originalAmount;
            }

            //convert units
            if (originalAmount.Number == amountToSubtract.Number)
            {
                //return in original or subtraction units?
                return new(0, originalAmount.Unit);
            }

            //return 0 is subtraction entire amount or more
            if (originalAmount.Number > amountToSubtract.Number)
            {
                decimal updatedNumber = originalAmount.Number - amountToSubtract.Number;
                // TODO: Decide what unit to use. Many an ingrident has a preferred/default unit?
                return new Amount(updatedNumber, originalAmount.Unit);
                //return in original or subtraction units?
                
            }
            //throw error instead?
            else
            {
                return new(0, originalAmount.Unit);
            }
        }

        public static Amount AddAmount(Amount amount1, Amount amount2)
        {
            //take in paramter for prefered unit return?
            // TODO: Convert to same units
            decimal updatedNumber = amount1.Number + amount2.Number;
            // TODO: Decide what unit to use. Maybe an ingrident has a preferred/default unit?
            return new Amount(updatedNumber, amount1.Unit);
        }

        public readonly static Amount Default = new(0, Units.Whole);
    }
}
