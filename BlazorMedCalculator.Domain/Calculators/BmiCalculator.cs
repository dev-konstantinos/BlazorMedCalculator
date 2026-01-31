namespace BlazorMedCalculator.Domain.Calculators
{
    public static class BmiCalculator
    {
        public static double Calculate(double weightKg, double heightCm)
        {
            if (weightKg <= 0)
                throw new ArgumentOutOfRangeException(nameof(weightKg));

            if (heightCm <= 0)
                throw new ArgumentOutOfRangeException(nameof(heightCm));

            var heightM = heightCm / 100.0;
            return weightKg / (heightM * heightM);
        }
    }
}
