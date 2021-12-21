using System.Reflection;
using NickUnit;

Type paramsUnitTestAttr = typeof(ParametrizedUnitTestAttribute);
Type inlineDataAttr = typeof(InlineDataAttribute);
Assembly assembly = typeof(Program).Assembly;

// settings
bool randomOrder = false;

int totalTests = 0;
int passedTests = 0;
int failedTests = 0;

List<MethodInfo> unitTests = new();

// for each class in this assembly (except this one)
IEnumerable<TypeInfo> classes = assembly.DefinedTypes
    .Where(t => t.IsClass && t.Name != typeof(Program).Name);
foreach (TypeInfo theClass in classes)
{
    // if it has the special name my framework tells users to use for settings...
    if (theClass.Name == "TestSettings")
    {
        // look for the special property names the users expect to be able to use for settings.
        // these are non-static properties, so we need an instance of the class to get the value from,
        // so we'll use the zero-argument constructor to get an instance.
        ConstructorInfo? constructor = theClass.GetConstructor(Array.Empty<Type>());
        object settingsInstance = constructor.Invoke(Array.Empty<object>());
        // here: get the first item in the sequence named RandomOrder, or if there isn't any, then don't run the block of code
        // (this code uses the "is" operator with pattern matching to do a null check. FirstOrDefault returns null if there are no matches.)
        if (theClass.DeclaredProperties.FirstOrDefault(p => p.Name == "RandomOrder") is PropertyInfo randomOrderProp)
        {
            // if there isn't one, give up
            if (constructor != null)
            {
                // ! operator to assert that the value is not null (get rid of possibly-null warning when you know it can't ever be null)
                randomOrder = (bool)randomOrderProp.GetValue(settingsInstance)!;
            }
        }
    }

    // add all non-static public methods having a UnitTestAttribute
    unitTests.AddRange(theClass
        .GetMethods(BindingFlags.Instance | BindingFlags.Public)
        .Where(m => m.GetCustomAttributes().Any(a => a.GetType() == typeof(UnitTestAttribute))));
}

foreach (MethodInfo unitTestMethod in unitTests)
{
    // the zero-arg constructor of the class containing this unit test method
    var constructor = unitTestMethod.DeclaringType!.GetConstructor(Array.Empty<Type>());
    if (constructor is null) continue;

    // instantiate the test class (a new instance for each test method is usual)
    object testClassInstance = constructor.Invoke(Array.Empty<object>());
    // invoke the test method on that instance
    try
    {
        // if you have a non-static MethodInfo, you call the method like: method.Invoke(instance, parameters)
        // here there are no parameters, so i use an empty array
        unitTestMethod.Invoke(testClassInstance, Array.Empty<object>());
        passedTests++;
    }
    catch (TargetInvocationException ex) when (ex.InnerException is not null)
    {
        // apparently if an exception is thrown from MethodInfo.Invoke, it
        // gets wrapped in a TargetInvocationException, so we unwrap it
        Console.WriteLine(ex.InnerException);
        Console.WriteLine(ex.InnerException.StackTrace);
        failedTests++;
    }
    totalTests++;
}

if (randomOrder)
{
    //Console.WriteLine("order is randomized");
    Random random = new();
    unitTests = unitTests.OrderBy(a => random.Next()).ToList();
}

Console.WriteLine($"Tests passed: {passedTests}/{totalTests}");
Console.WriteLine($"Tests failed: {failedTests}/{totalTests}");

// a lot of frameworks have some amount of "convention" instead of "configuration"
// that's made possible by reflection like this

// reflection - it's very slow compared to other things
