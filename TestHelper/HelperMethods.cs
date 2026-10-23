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

        public static void AssertConstructor(string className, params object[] constructorArguments)
        {
            var assemblyName = Assembly.GetCallingAssembly().GetName().Name.Substring(4);
            var assembly = Assembly.Load(assemblyName);

            className = $"{assemblyName}.{className}";

            Type type = assembly.GetType(className)
                        ?? throw new TypeLoadException($"Klasse '{className}' kon niet worden gevonden.");

            Type[] argumentTypes = constructorArguments.Select(arg => arg?.GetType() ?? typeof(object)).ToArray();

            ConstructorInfo constructor = type.GetConstructor(
                BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                null,
                argumentTypes,
                null);

            if (constructor == null)
            {
                // Als er geen exacte match is, geven we duidelijke feedback over wat de leerling wél heeft
                var alleConstructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                string beschikbareConstructors = string.Join("\n", alleConstructors.Select(c => $"- {className}({string.Join(", ", c.GetParameters().Select(p => $"{p.Name} : {p.ParameterType.Name}"))})"));

                Assert.Fail($"Geen geschikte constructor gevonden in '{className}' voor de meegegeven types: ({string.Join(", ", argumentTypes.Select(t => t.Name))}).\n" +
                            $"Beschikbare constructors in de klasse {className}:\n{beschikbareConstructors}");
            }

            object instance;
            try
            {
                instance = constructor.Invoke(constructorArguments);
            }
            catch (TargetInvocationException tex)
            {
                Assert.Fail($"De constructor van '{className}' crashte tijdens het uitvoeren.\nTechnische fout: {tex.InnerException?.Message}");
                return;
            }

            ParameterInfo[] constructorParams = constructor.GetParameters();

            for (int i = 0; i < constructorParams.Length; i++)
            {
                Type paramType = constructorParams[i].ParameterType;
                string paramName = constructorParams[i].Name.Replace("_","");
                object expectedValue = constructorArguments[i];

                PropertyInfo property = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .FirstOrDefault(p => p.Name.Replace("_","").Equals(paramName, StringComparison.OrdinalIgnoreCase) && paramType == p.PropertyType);

                if(property == null)
                {
                    try
                    {
                        property = type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .SingleOrDefault(p => p.Name.Replace("_", "").StartsWith(paramName, StringComparison.OrdinalIgnoreCase)
                        && paramType == p.PropertyType);
                    }catch(InvalidOperationException e) when (e.Message.Contains("Sequence contains more than one matching element"))
                    {
                        Assert.Fail($"Er werden meerdere mogelijk properties gevonden voor de parameter {paramName} in de constructor {$"{className}({string.Join(", ", constructor.GetParameters().Select(p => $"{p.Name} : {p.ParameterType.Name}"))})"}.");
                    }
                }

                FieldInfo field = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                    .FirstOrDefault(f => f.Name.Replace("_", "").Equals(paramName, StringComparison.OrdinalIgnoreCase) && paramType == f.FieldType);



                if (field == null)
                {
                    try
                    {
                        field = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                        .SingleOrDefault(p => p.Name.Replace("_", "").StartsWith(paramName, StringComparison.OrdinalIgnoreCase)
                        && paramType == p.FieldType);
                    }
                    catch (InvalidOperationException e) when (e.Message.Contains("Sequence contains more than one matching element"))
                    {
                        Assert.Fail($"Er werden meerdere mogelijk velden gevonden voor de parameter {paramName} in de constructor {$"{className}({string.Join(", ", constructor.GetParameters().Select(p => $"{p.Name} : {p.ParameterType.Name}"))})"}.");
                    }
                }
                object actualValue = null;
                bool foundMatch = false;

                if (property != null)
                {
                    actualValue = property.GetValue(instance);
                    foundMatch = true;
                }
                else if (field != null)
                {
                    actualValue = field.GetValue(instance);
                    foundMatch = true;
                }

                if (!foundMatch)
                {
                    Assert.Fail($"De constructor verwacht de parameter '{paramName}', maar de klasse '{className}' heeft geen bijbehorende Property of Field (zoals '{paramName}' of '_{paramName}') om deze waarde in op te slaan.");
                }

                // Assert: Is de waarde correct overgenomen?
                Assert.AreEqual(expectedValue, actualValue,
                    $"De waarde voor '{paramName}' is niet correct ingesteld door de constructor van '{className}'.");
            }
        }

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
