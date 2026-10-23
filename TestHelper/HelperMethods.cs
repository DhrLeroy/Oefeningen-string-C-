using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace TestHelper
{
    public static class HelperMethods
    {
        public static void AssertOutputs(string expected, params string[] input)
        {
            var inputString = new StringReader(string.Join("\n", input));
            Console.SetIn(inputString);

            var sw = new StringWriter();
            var originalOut = Console.Out;
            Console.SetOut(sw);

            try
            {
                var assemblyName = Assembly.GetCallingAssembly().GetName().Name.Substring(4);
                var assembly = Assembly.Load(assemblyName); 
                var entryPoint = assembly.EntryPoint;
                entryPoint.Invoke(null, entryPoint.GetParameters().Length == 0 ? null : new object[] { Array.Empty<string>() });

                // Assert
                var output = sw.ToString();
                StringAssert.Contains(output.Trim(), expected.Trim());
            }
            finally
            {
                Console.SetOut(originalOut);
            }
        }

        /*public static void AssertMethodOutput(string className, string methodName, object expectedOutput, params object[] methodParameters)
        {
        var assemblyName = Assembly.GetCallingAssembly().GetName().Name.Substring(4);
        var assembly = Assembly.Load(assemblyName);

            className = $"{assemblyName}.{className}";

        // 2. Zoek het type (de klasse) in de assembly
        // Let op: Als de klasse in een namespace staat, moet className "Namespace.Klassenaam" zijn
        Type type = assembly.GetType(className)
                    ?? throw new TypeLoadException($"Klasse '{className}' kon niet worden gevonden in assembly '{assemblyName}'.");

        // 3. Zoek de methode (zowel publiek, privaat, static als instantie)
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        MethodInfo method = type.GetMethod(methodName, flags)
                    ?? throw new MissingMethodException($"Methode '{methodName}' kon niet worden gevonden in klasse '{className}'.");

        object instance = null;

        // 4. Als het geen static methode is, moeten we een instantie van de klasse aanmaken
        if (!method.IsStatic)
        {
            instance = Activator.CreateInstance(type);
        }

            // 5. Voer de methode uit (Invoke)
            // Als de methode 'void' teruggeeft, is het resultaat 'null'

            try
            {
                object actualResult = method.Invoke(instance, methodParameters);
                Assert.AreEqual(expectedOutput, actualResult, $"De output van {className}.{methodName} kwam niet overeen met de verwachting.");
            }
            catch (TargetParameterCountException)
            {
                throw new TargetParameterCountException($"De methode {methodName} bevat niet {methodParameters.Length} parameters.");
            }
            catch (ArgumentException ex)
            {
                // Haal de parameters op die de methode DAADWERKELIJK verwacht
                ParameterInfo[] expectedParams = method.GetParameters();

                string errorFeedback = $"Fout bij het aanroepen van {className}.{methodName}():\n"+
                                       $"Verwachte parameters:\n";

                for (int i = 0; i < expectedParams.Length; i++)
                {
                    string expectedType = expectedParams[i].ParameterType.Name;
                    string paramName = expectedParams[i].Name;

                    // Controleer wat de test heeft meegestuurd voor deze positie
                    string actualType = (methodParameters != null && i < methodParameters.Length && methodParameters[i] != null)
                        ? methodParameters[i].GetType().Name
                        : "null of ontbrekend";

                    errorFeedback += $"{i+1}. Parameter {paramName} is van het type {expectedType} maar moet van het type {actualType} zijn.\n";
                }

                // Gooi een duidelijke AssertionException met de gedetailleerde feedback
                Assert.Fail($"{errorFeedback}\nTechnische foutmelding: {ex.Message}");
            }

            // 6. Assert de output
        }*/

    public static void AssertMethodOutput(string className, string methodName, object expectedOutput, params object[] methodParameters)
    {
        // 1. Haal de assembly op
        var assemblyName = Assembly.GetCallingAssembly().GetName().Name.Substring(4);
        var assembly = Assembly.Load(assemblyName);

            className = $"{assemblyName}.{className}";

        // 2. Zoek de klasse
        Type type = assembly.GetType(className)
                    ?? throw new TypeLoadException($"Klasse '{className}' kon niet worden gevonden.");

        // 3. Zoek de methode
        BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
        MethodInfo method = type.GetMethod(methodName, flags)
                    ?? throw new MissingMethodException($"Methode '{methodName}' kon niet worden gevonden in klasse '{className}'.");

        // 4. Maak een instantie aan indien nodig
        object instance = null;
        if (!method.IsStatic)
        {
            instance = Activator.CreateInstance(type);
        }

        // 5. Controleer of de methode void is
        bool isVoid = method.ReturnType == typeof(void);
            bool shouldBeVoid = expectedOutput.GetType() == typeof(string) && expectedOutput.ToString().StartsWith("#console#");
            if(isVoid && !shouldBeVoid)
            {
                Assert.Fail($"{className}.{methodName} is van het return-type void maar moet van het type {expectedOutput.GetType().Name} zijn.");
            }
            else if(!isVoid && shouldBeVoid)
            {
                Assert.Fail($"{className}.{methodName} is van het return-type {method.ReturnType.Name} maar moet van het type void zijn.");
            }
            if (shouldBeVoid)
                expectedOutput = expectedOutput.ToString().Substring("#console#".Length);

        StringWriter sw = null;
        TextWriter originalOut = null;

        if (isVoid)
        {
            // Bereid het opvangen van de Console voor als de methode void is
            sw = new StringWriter();
            originalOut = Console.Out;
            Console.SetOut(sw);
        }

        try
        {
            // 6. Voer de methode uit
            object actualResult = method.Invoke(instance, methodParameters);

            // 7. Assert de output op basis van het returntype
            if (isVoid)
            {
                // Herstel de console direct na uitvoeren van de code van de leerling
                Console.SetOut(originalOut);

                string consoleOutput = sw.ToString().Trim();
                string expectedString = expectedOutput?.ToString()?.Trim() ?? string.Empty;

                Assert.AreEqual(expectedString, consoleOutput,
                    $"De Console-output van {className}.{methodName} kwam niet overeen met de verwachting.");
            }
            else
            {
                // Vergelijk de daadwerkelijke returnwaarde
                Assert.AreEqual(expectedOutput, actualResult,
                    $"De returnwaarde van {className}.{methodName} kwam niet overeen met de verwachting.");
            }
            }
            catch (TargetParameterCountException)
            {
                throw new TargetParameterCountException($"De methode {methodName} bevat niet {methodParameters.Length} parameter{(methodParameters.Length > 1 ? "s" : "")}.");
            }
            catch (TargetInvocationException tex) when (tex.InnerException is ArgumentException ex)
        {
            // Belangrijk: Reflection verpakt parameter-fouten soms in een TargetInvocationException
            HandleArgumentException(method, methodParameters, ex, className, methodName);
        }
        catch (ArgumentException ex)
        {
            HandleArgumentException(method, methodParameters, ex, className, methodName);
        }
        finally
        {
            // Zorg dat de Console ALTIJD netjes wordt hersteld, ook bij crashes
            if (isVoid && originalOut != null)
            {
                Console.SetOut(originalOut);
            }
        }
    }

    // Hulpmethode voor de nette foutmeldingen naar de leerling (uit de vorige stap)
    private static void HandleArgumentException(MethodInfo method, object[] methodParameters, ArgumentException ex, string className, string methodName)
    {
        ParameterInfo[] expectedParams = method.GetParameters();
        string errorFeedback = $"Fout bij het aanroepen van {className}.{methodName}():\n"+
                               $"Verwachte parameters:\n";

        for (int i = 0; i < expectedParams.Length; i++)
        {
            string expectedType = expectedParams[i].ParameterType.Name;
            string paramName = expectedParams[i].Name;
            string actualType = (methodParameters != null && i < methodParameters.Length && methodParameters[i] != null)
                ? methodParameters[i].GetType().Name
                : "null of ontbrekend";

            errorFeedback += $"- Parameter {i + 1} ({paramName}): Verwacht type '{expectedType}', maar kreeg '{actualType}'.\n";
        }

        Assert.Fail($"{errorFeedback}\nTechnische foutmelding: {ex.Message}");
    }


    }
}
