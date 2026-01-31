namespace BlazorMedCalculator.Domain.Calculators
{
    public static class BmiClassifier
    {
        public static string Classify(double bmi) =>
            bmi switch
            {
                < 18.5 => "Underweight",
                < 25.0 => "Normal weight",
                < 30.0 => "Overweight",
                _ => "Obesity"
            };
    }
}
