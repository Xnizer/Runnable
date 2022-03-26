# Runnable

Note: this library is still under construction.

Test your new idea quickly, and avoid commenting out your previews tests, go with Runnable!

With Runnable you can just add the [Runnable] attribute to functions anywhere in your code, and with a single line of code, Runnable will let you run any of those functions from the console.

## Example
```csharp
static void main()
{
	Runnable.Runner.Run();
}

// somwhere in your code
[Runnable("My brilliant idea")]
public void TestBrilliantIdea()
{
	// test code here
}

// somewhere else in your code
[Runnable("My other brilliant idea")]
public void Test()
{
	// test code here
}
```
