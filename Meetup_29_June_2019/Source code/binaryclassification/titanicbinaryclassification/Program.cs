using System;
using System.IO;
using Microsoft.ML;
using Microsoft.ML.Data;
using static Microsoft.ML.Transforms.MissingValueReplacingEstimator;
using titanicbinaryclassification.Schema;

namespace titanicbinaryclassification
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate MLContext
            var mlContext = new MLContext();

            // Load
            Console.WriteLine("Load...");
            var data = mlContext.Data.LoadFromTextFile<Passenger>("Data/titanic.csv", hasHeader: true, separatorChar: ',');
            var trainTestData = mlContext.Data.TrainTestSplit(data, 0.2); // Training/Test : 80/20  

            // Transform
            Console.WriteLine("Transform...");
            var dataProcessPipeline = mlContext.Transforms.Categorical.OneHotEncoding("Sex", "Sex")
                .Append(mlContext.Transforms.ReplaceMissingValues("Age", replacementMode: ReplacementMode.Mean))
                .Append(mlContext.Transforms.Concatenate("Features", "PClass", "Sex", "SiblingsAboard", "ParentsAboard"));

            // Train
            Console.WriteLine("Train...");
            var trainingPipeline = dataProcessPipeline.Append(mlContext.BinaryClassification.Trainers.LbfgsLogisticRegression("Survived"));
            var trainedModel = trainingPipeline.Fit(trainTestData.TrainSet);

            // Evaluate 
            Console.WriteLine("Evaluate...");
            var predictions = trainedModel.Transform(trainTestData.TrainSet);
            var metrics = mlContext.BinaryClassification.Evaluate(predictions, "Survived", "Score");
            var accuracy = Math.Round(metrics.Accuracy * 100, 2);
            Console.WriteLine($"Accuracy: {accuracy}%");

            // Save
            Console.WriteLine("Save...");
            var savedPath = Path.Combine(Directory.GetCurrentDirectory(), "model.zip");
            mlContext.Model.Save(trainedModel, trainTestData.TrainSet.Schema, savedPath);
            Console.WriteLine("The model is saved to {0}", savedPath);

            // Predict
            Console.WriteLine("*********** Predict...");
            var predictionEngine = mlContext.Model.CreatePredictionEngine<Passenger, PassengerPrediction>(trainedModel);
            var passenger = new Passenger()
            {
                PClass = 1,
                Name = "Mark Farragher",
                Sex = "male",
                Age = 48,
                SiblingsAboard = 0,
                ParentsAboard = 0
            };

            // make the prediction
            var prediction = predictionEngine.Predict(passenger);

            // report the results
            Console.WriteLine($"Passenger:   {passenger.Name} ");
            Console.WriteLine($"Prediction:  {(prediction.Prediction ? "survived" : "perished")} ");
        }
    }
}
