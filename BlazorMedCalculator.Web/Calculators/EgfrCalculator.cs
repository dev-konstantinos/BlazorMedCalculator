namespace BlazorMedCalculator.Web.Calculators
{
    public static class EgfrCalculator
    {
        public static double Calculate(
            double creatinineMgDl,
            int age,
            bool isFemale)
        {
            var k = isFemale ? 0.7 : 0.9;
            var alpha = isFemale ? -0.329 : -0.411;
            var sexFactor = isFemale ? 1.018 : 1.0;

            var min = Math.Min(creatinineMgDl / k, 1);
            var max = Math.Max(creatinineMgDl / k, 1);

            return 141
                * Math.Pow(min, alpha)
                * Math.Pow(max, -1.209)
                * Math.Pow(0.993, age)
                * sexFactor;
        }
    }
}
