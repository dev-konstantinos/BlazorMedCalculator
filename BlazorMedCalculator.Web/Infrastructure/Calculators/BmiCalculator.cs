namespace BlazorMedCalculator.Domain.Calculators
{
    public static class BmiCalculator
    {
        public static double Calculate(double weightKg, double heightCm)
        {
            var heightM = heightCm / 100.0;
            return weightKg / (heightM * heightM);
        }
    }
}
